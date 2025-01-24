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

            if (model.Role != "ZakelijkeHuurder" 
                && model.Role != "WagenparkBeheerder" 
                && model.Role != "Particulier" 
                && model.Role != "BackOffice"
                && model.Role != "FrontOffice"
                && model.Role != "Admin")
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

        // Login a user with password, check if 2FA is required
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid data provided." });
                }

                // Find user by email or username
                var user = model.EmailOrUsername.Contains("@")
                    ? await _userManager.FindByEmailAsync(model.EmailOrUsername)
                    : await _userManager.FindByNameAsync(model.EmailOrUsername);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email/username or password." });
                }

                // Attempt sign-in with password
                var result = await _signInManager.PasswordSignInAsync(
                    user,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false
                );

                // If 2FA is required, we do NOT do the final cookie sign-in here.
                // Instead, we tell the front end: "2FA needed."
                if (result.RequiresTwoFactor)
                {
                    return Ok(new
                    {
                        requiresTwoFactor = true,
                        userId = user.Id,
                        // optionally:  message = "2FA required"
                    });
                }

                if (result.Succeeded)
                {
                    // Build custom claims (if you want extra claims beyond defaults):
                    var roles = await _userManager.GetRolesAsync(user);
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "User"),
                new Claim("UserId", user.Id)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    // This signs in the user by issuing the auth cookie
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );

                    return Ok(new { message = "Login successful" });
                }

                // If we reach here, it's usually a bad password or locked out scenario
                if (result.IsLockedOut)
                {
                    return Unauthorized(new { message = "User is locked out." });
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

        [HttpPost("verify-2fa-login")]
        public async Task<IActionResult> VerifyTwoFactorLogin([FromBody] TwoFactorLoginDto model)
        {
            // Example Dto: { userId: "XYZ", twoFactorCode: "123456", rememberMe: true }
            try
            {
                // The user isn't signed in yet, so we can't just do _userManager.GetUserAsync(User).
                // Instead, we rely on _signInManager for the second factor sign in.
                var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
                    model.TwoFactorCode,
                    model.RememberMe,
                    rememberClient: false
                );

                if (result.Succeeded)
                {
                    // The sign-in manager will issue the cookie automatically if 2FA is correct.
                    return Ok(new { message = "2FA login successful" });
                }
                else if (result.IsLockedOut)
                {
                    return Unauthorized(new { message = "User is locked out." });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid 2FA code." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred verifying 2FA.",
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
            var adres = User.FindFirst(ClaimTypes.StreetAddress)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var phonenumber = User.FindFirst(ClaimTypes.MobilePhone)?.Value;

            return Ok(new
            {
                id = userId,
                email = email,
                adres = adres,
                role = role,
                name = username,
                phonenumber = phonenumber
            });
        }

        [HttpPost("enable-2fa")]
        [Authorize] // user must be logged in via cookie
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { Message = "User not found." });

            // 1) Ensure user has an authenticator key
            var key = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(key))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                key = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            // 2) Build the URI for TOTP apps
            var email = user.Email ?? user.UserName;
            var issuer = "MyAwesomeApp"; // put your app/service name here
            var otpauthUrl = $"otpauth://totp/{issuer}:{email}?secret={key}&issuer={issuer}&digits=6";

            // Return the otpauth URL so the front-end can generate a QR code
            return Ok(new { OtpAuthUrl = otpauthUrl });
        }

        [HttpPost("disable-2fa")]
        [Authorize]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { message = "User not found." });

            // Set TwoFactorEnabled = false
            user.TwoFactorEnabled = false;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest(new { message = "Could not disable 2FA." });
            }

            // Optionally clear/reset the authenticator key
            // so any old TOTP codes won’t work anymore
            await _userManager.ResetAuthenticatorKeyAsync(user);

            return Ok(new { message = "2FA disabled successfully." });
        }

        [HttpPost("verify-2fa")]
        [Authorize]
        public async Task<IActionResult> VerifyTwoFactorToken([FromBody] VerifyTokenDto model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { Message = "User not found." });

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                user,
                TokenOptions.DefaultAuthenticatorProvider,
                model.Token);

            if (!isValid)
                return BadRequest(new { Message = "Invalid token." });

            user.TwoFactorEnabled = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Could not enable 2FA." });

            return Ok(new { Message = "2FA is enabled." });
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
        public async Task<ActionResult> VoegMedewerkerToe( string kvkNummer, [FromBody] GebruikerToevoegenDto gebruikerToevoegen)
        {
            if (gebruikerToevoegen == null)
            {
                return BadRequest();
            }
            var email = gebruikerToevoegen.Email;
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