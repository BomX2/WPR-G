using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.Huur;
using WebProjectG.Server.domain.GebruikerFiles.Dtos;
using WebProjectG.Server.domain.BedrijfFiles;
using WebProjectG.Server.domain.Voertuig;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebProjectG.Server.domain.GebruikerFiles.Controllers
{
    [ApiController]
    [Route("api/gebruikers")]
    public class GebruikerController : ControllerBase
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly SignInManager<Gebruiker> _signInManager;
        private readonly GebruikerDbContext _dbContext;
        private readonly HuurContext _huurContext;

        public GebruikerController(
            GebruikerDbContext dbContext,
            HuurContext huurContext,
            UserManager<Gebruiker> userManager,
            SignInManager<Gebruiker> signInManager)
        {
            _dbContext = dbContext;
            _huurContext = huurContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] GebruikerDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided." });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new { message = "Passwords do not match." });
            }

            if (string.IsNullOrEmpty(model.Role))
            {
                return BadRequest(new { message = "Role is required." });
            }

            if (model.Role != "ZakelijkeHuurder" && model.Role != "WagenparkBeheerder" && model.Role != "Particulier")
            {
                return BadRequest(new { message = "Invalid role specified." });
            }

            var user = new Gebruiker
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Adres = model.Adres,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                return Ok(new { message = "Registration successful" });
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(new { message = errors });
        }

        //Login a user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid data provided." });
                }
                
                var user = model.EmailOrUsername.Contains("@")
                    ? await _userManager.FindByEmailAsync(model.EmailOrUsername)
                    : await _userManager.FindByNameAsync(model.EmailOrUsername);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email/username or password." });
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "User"),

                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };


            };
                    foreach (var claim in User.Claims)
                    {
                        Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                    }
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return Ok(new { message = "Login successful" });
                }

                return Unauthorized(new { message = "Invalid email/username or password." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred during login.",
                    details = ex.Message 
                });
            }
        }


        //Logout a user
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }

        //Get current user with claims
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var username = User.Identity.Name;

            return Ok(new
            {
                id = userId,
                email = email,
                role = role,
                name = username
            });
        }

        //Fetch user details
        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.PhoneNumber,
                user.Adres
            });
        }

        //Update user details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserDetails(string id, [FromBody] UpdateGebruikerDto model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Adres = model.Adres;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { message = "User details updated successfully." });
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(new { message = errors });
        }

        //Change password
        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { message = "Password changed successfully." });
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(new { message = errors });
        }

        //Delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { message = "User deleted successfully." });
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(new { message = errors });
        }
            [HttpGet("pingauth")]
        [Authorize] // Ensure only authenticated users can access this endpoint
        public async Task<IActionResult> GetAuthenticatedUserRole()
        {
            // Extract the logged-in user's email from the claims
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return Unauthorized(new { message = "User is not logged in." });
            }

            // Fetch the user by email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // Retrieve the roles of the user
            var roles = await _userManager.GetRolesAsync(user);

            // Return the email and roles
            return Ok(new
            {
                Email = email,
                Role = roles.FirstOrDefault() // Adjust for multiple roles if needed
            });
        }

        [HttpPost("postAanvraag")]
        public async Task<ActionResult> PostAanvraag(Aanvraag aanvraag)
        {
            _huurContext.Aanvragen.Add(aanvraag);
            await _huurContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("getAanvragen")]
        public async Task<ActionResult> GetAanvragen()
        {
            var aanvragen = await _huurContext.Aanvragen
                .Where(aanv => aanv.Goedgekeurd == null)
                .Include(aanv => aanv.Auto)
                .Select(aanv => new { aanv.Id, aanv.StartDatum, aanv.EindDatum, aanv.Gebruiker.Email, aanv.Gebruiker.PhoneNumber, AutoType = aanv.Auto.Type, AutoMerk = aanv.Auto.Merk })
                .ToListAsync();

            if (!aanvragen.Any()) return NotFound();
            return Ok(aanvragen);
        }

        [HttpGet("getAanvragenFront")]
        public async Task<ActionResult> GetAanvragenFront()
        {
            var aanvragen = await _huurContext.Aanvragen
                .Where(aanv => aanv.Goedgekeurd == true)
                .Include(aanv => aanv.Auto)
                .Select(aanv => new { aanv.Id, aanv.StartDatum, aanv.EindDatum, aanv.Gebruiker.Email, aanv.Gebruiker.PhoneNumber, aanv.Status, AutoType = aanv.Auto.Type, AutoMerk = aanv.Auto.Merk })
                .ToListAsync();

            if (!aanvragen.Any()) return NotFound();
            return Ok(aanvragen);
        }

        [HttpPut("KeurAanvraagGoed/{id}")]
        public async Task<IActionResult> KeurAanvraagGoed(int id, [FromBody] AanvraagDto aanvraagDto)
        {
            if (id != aanvraagDto.Id) return BadRequest();

            var aanvraag = await _huurContext.Aanvragen.FindAsync(id);
            if (aanvraag == null) return NotFound();

            aanvraag.Goedgekeurd = aanvraagDto.Goedgekeurd;
            aanvraag.Status = aanvraagDto.Status;

            _huurContext.Entry(aanvraag).State = EntityState.Modified;
            await _huurContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("verwijderAanvraag/{id}")]
        public async Task<IActionResult> DeleteAanvraag(int id)
        {
            var aanvraag = await _huurContext.Aanvragen.FindAsync(id);
            if (aanvraag == null) return NotFound();

            _huurContext.Aanvragen.Remove(aanvraag);
            await _huurContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("GetgeboekteDatums/{id}")]
        public async Task<IActionResult> GetAanvraagDatums(int id)
        {

            var aanvragen = await _huurContext.Aanvragen.Where(aanv => aanv.AutoId == id).Select(aanv => new { aanv.StartDatum, aanv.EindDatum }).ToListAsync();
            if (!aanvragen.Any()) return NotFound();

            return Ok(aanvragen);
        }

        [HttpPost("postbedrijf")]
        public async Task<ActionResult> PostBedrijf(Bedrijf bedrijf)
        {
            _dbContext.Bedrijven.Add(bedrijf);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction("getBedrijf", new { kvknummer = bedrijf.KvkNummer }, bedrijf);
        }
        [HttpGet("getBedrijf{kvknummer}")]
        public async Task<ActionResult<Bedrijf>> GetBedrijf(string kvknummer)
        {
            var bedrijf = await _dbContext.Bedrijven.FindAsync(kvknummer); if (bedrijf == null)
            { return NotFound(); }
            return bedrijf;
        }


        [HttpPut("putBedrijfsAbonnement/{kvkNummer}")]
        public async Task<IActionResult> PutBedrijf(string kvkNummer, [FromBody] BedrijfPutDto dto)
        {
            var bedrijf = await _dbContext.Bedrijven.Include(b => b.Abonnement).FirstOrDefaultAsync(b => b.KvkNummer == kvkNummer);
            if (bedrijf == null)
            {
                return NotFound();
            }

            if (bedrijf.Abonnement == null)
            {
                bedrijf.Abonnement = new Abonnement
                {
                    AbonnementType = dto.AbonnementType,
                };
                _dbContext.Abonnementen.Add(bedrijf.Abonnement);

            }
            else
            {
                bedrijf.Abonnement.AbonnementType = dto.AbonnementType;
            }
                    try
                    {
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_dbContext.Bedrijven.Any(e => kvkNummer == e.KvkNummer))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return NoContent();

                }
            
        [HttpPost("AddGebruikerToBedrijf/{kvkNummer}")]
        public async Task<ActionResult> VoegMedewerkerToe(string kvkNummer, string email, string domeinNaam)
        {
            var gebruiker = await _userManager.FindByEmailAsync(email);
            if (gebruiker == null) return BadRequest("Gebruiker is niet gevonden");

            var bedrijf = await _dbContext.Bedrijven.Include(b => b.ZakelijkeHuurders).FirstOrDefaultAsync(b => b.KvkNummer == kvkNummer);
            if (bedrijf.ZakelijkeHuurders.Any(g => g.Email == email))
            {
                return BadRequest("Gebruiker is al gekoppeld aan dit bedrijf.");
            }
            var emailDomein = email.Split('@').LastOrDefault();
            if (bedrijf == null) return BadRequest("Bedrijf niet gevonden");
            if (bedrijf.DomeinNaam == emailDomein)
            {
                bedrijf.ZakelijkeHuurders.Add(gebruiker);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        
    }
}