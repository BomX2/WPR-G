using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.Huur;
using WebProjectG.Server.domain.GebruikerFiles.Dtos;
using WebProjectG.Server.domain.BedrijfFiles;
using WebProjectG.Server.domain.VoertuigFiles;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Reflection.Metadata.Ecma335;

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
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data provided." });

            // Find user by Email or Username
            Gebruiker user = model.EmailOrUsername.Contains('@')
                ? await _userManager.FindByEmailAsync(model.EmailOrUsername)
                : await _userManager.FindByNameAsync(model.EmailOrUsername);

            if (user == null)
                return Unauthorized(new { message = "Invalid email/username or password." });

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (result.RequiresTwoFactor)
                return Ok(new { requiresTwoFactor = true, userId = user.Id });

            if (result.Succeeded)
                return Ok(new { message = "Login successful" });

            if (result.IsLockedOut)
                return Unauthorized(new { message = "User is locked out." });

            return Unauthorized(new { message = "Invalid email/username or password." });
        }


        [HttpPost("verify-2fa-login")]
        public async Task<IActionResult> VerifyTwoFactorLogin([FromBody] TwoFactorLoginDto model)
        {
            try
            {
                var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
                    model.TwoFactorCode,
                    model.RememberMe,
                    rememberClient: false
                );

                if (result.Succeeded)
                {
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
            var twoFactorEnabledString = User.FindFirst("TwoFactorEnabled")?.Value;
            bool twoFactorEnabled = bool.TryParse(twoFactorEnabledString, out var parsedResult) && parsedResult;


            return Ok(new
            {
                id = userId,
                email = email,
                adres = adres,
                role = role,
                username = username,
                phonenumber = phonenumber,
                twoFactorEnabled = twoFactorEnabled
            });
        }

        [HttpPost("enable-2fa")]
        [Authorize]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { Message = "User not found." });

            var key = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(key))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                key = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            var email = user.Email ?? user.UserName;
            var issuer = "CarAndAll";
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

            user.TwoFactorEnabled = false;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest(new { message = "Could not disable 2FA." });
            }

            await _userManager.ResetAuthenticatorKeyAsync(user);

            return Ok(new { message = "2FA disabled successfully." });
        }

        [HttpPost("verify-2fa")] //verify for enabling
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
        [HttpPut("updateGebruiker/{id}")]
        public async Task<IActionResult> UpdateUserDetails(string id, [FromBody] UpdateGebruikerDto model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound(new { message = "User not found." });

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Adres = model.Adres;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { message = string.Join(", ", result.Errors.Select(e => e.Description)) });

            // refresh claims
            await _signInManager.RefreshSignInAsync(user);

            return Ok(new { message = "User details updated, claims refreshed." });
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
            var aanvragen = await _huurContext.Aanvragen.Where(aanv => aanv.Goedgekeurd == false && aanv.Status != "beschadigd")
                .Include(aanv => aanv.voertuig)
                .Select(aanv => new { aanv.Id, aanv.StartDatum, aanv.EindDatum, aanv.Adres, aanv.Email, aanv.Telefoonnummer, aanv.Status, AutoType = aanv.voertuig.Type, AutoMerk = aanv.voertuig.Merk, aanv.Kenteken })

                .ToListAsync();

            if (aanvragen.Count == 0) return NotFound();
            return Ok(aanvragen);
        }

        [HttpGet("getAanvragenFront")]
        public async Task<ActionResult> GetAanvragenFront()
        {
            var aanvragen = await _huurContext.Aanvragen
                .Where(aanv => aanv.Goedgekeurd == true && aanv.Status != "beschadigd")
                .Include(aanv => aanv.voertuig)
                .Select(aanv => new { aanv.Id, aanv.StartDatum, aanv.EindDatum, aanv.Adres, aanv.Email, aanv.Telefoonnummer, aanv.Status, AutoType = aanv.voertuig.Type, AutoMerk = aanv.voertuig.Merk, Kenteken = aanv.Kenteken })
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

        [HttpGet("GetgeboekteDatums/{Kenteken}")]
        public async Task<IActionResult> GetAanvraagDatums(String Kenteken)
        {

            var aanvragen = await _huurContext.Aanvragen.Where(aanv => aanv.Kenteken == Kenteken).Select(aanv => new { aanv.StartDatum, aanv.EindDatum }).ToListAsync();
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
                    BetaalMethode = dto.BetaalMethode,
                    Prijs = dto.Prijs,
                    Periode = dto.Periode
                };
                _dbContext.Abonnementen.Add(bedrijf.Abonnement);

            }
            else
            {
                bedrijf.Abonnement.AbonnementType = dto.AbonnementType;
                bedrijf.Abonnement.BetaalMethode = dto.BetaalMethode;
                bedrijf.Abonnement.Prijs = dto.Prijs;
                bedrijf.Abonnement.Periode = dto.Periode;
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

        [HttpGet("LaatGebruikersZien/{kvknummer}")]
        public async Task<ActionResult<Bedrijf>> GetMedewerkers(string kvknummer)
        {

            var bedrijf = await _dbContext.Bedrijven.Include(b => b.ZakelijkeHuurders).FirstOrDefaultAsync(bedr => bedr.KvkNummer == kvknummer);
            if (bedrijf == null)
            {
                return BadRequest();
            }
            if (bedrijf.ZakelijkeHuurders == null)
            {
                return NoContent();
            }

            var zakelijkehuurders = bedrijf.ZakelijkeHuurders.Select(geb => geb.Email).ToList();
            return Ok(zakelijkehuurders);
        }
        [HttpDelete("VerwijderMedewerker/{email}")]
        public async Task<IActionResult> VerwijderMedewerker(string email, [FromBody] ZakelijkeHuurderDto zakelijkeHuurderDto)
        {
            var bedrijf = await _dbContext.Bedrijven.Include(b => b.ZakelijkeHuurders).FirstOrDefaultAsync(bed => bed.KvkNummer == zakelijkeHuurderDto.kvknummer);
            if (bedrijf == null)
            {
                return NotFound("Bedrijf is niet gevonden");
            }
            var zakelijkehuurder = bedrijf.ZakelijkeHuurders.FirstOrDefault(geb => geb.Email == email);
            if (zakelijkehuurder == null)
            {
                return BadRequest("Gebruiker is niet gekoppeld aan dit bedrijf");
            }
            bedrijf.ZakelijkeHuurders.Remove(zakelijkehuurder);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("AddGebruikerToBedrijf/{kvkNummer}")]
        public async Task<ActionResult> VoegMedewerkerToe(string kvkNummer, [FromBody] GebruikerToevoegenDto gebruikerToevoegen)
        {

            if (gebruikerToevoegen == null)
            {
                return BadRequest();
            }
            var email = gebruikerToevoegen.Email;
            var gebruiker = await _userManager.FindByEmailAsync(email);
            if (gebruiker == null) return BadRequest("Gebruiker is niet gevonden");

            var bedrijf = await _dbContext.Bedrijven.Include(b => b.ZakelijkeHuurders).FirstOrDefaultAsync(b => b.KvkNummer == kvkNummer);
            if (bedrijf == null) return BadRequest("Bedrijf bestaat niet.");
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
        [HttpGet("GetSFormulieren")]
        public async Task<ActionResult> GetFormulieren()
        {
            var Schadeformulieren = await _huurContext.schadeFormulieren.ToListAsync();
            if (Schadeformulieren.Count == 0)
            {
                return NotFound();
            }
            return Ok(Schadeformulieren);
        }
        [HttpDelete("SchadeformVerwijder/{id}")]
        public async Task<IActionResult> VerwijderSchadeForm(int id)
        {
            var schadeFormulier = await _huurContext.schadeFormulieren.FirstOrDefaultAsync(schade => schade.Id == id);
            if (schadeFormulier == null)
            {
                return BadRequest();
            }
            _huurContext.schadeFormulieren.Remove(schadeFormulier);
            await _huurContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("SchadeFormulier/{id}")]
        public async Task<ActionResult<SchadeFormulier>> GetSchadeFormulier(int id)
        {
            var schadeFormulier = await _huurContext.schadeFormulieren.FindAsync(id);

            if (schadeFormulier == null)
            {
                return NotFound();
            }

            return Ok(schadeFormulier);
        }
        [HttpPut("PutSchadeForm/{SchadeId}")]
        public async Task<IActionResult> PutSFormulier(int SchadeId, PutSchadeformulierDto putSchadeformulier)
        {
            var schadeFormulier = await _huurContext.schadeFormulieren.FirstOrDefaultAsync(schade => schade.Id == SchadeId);
            if (schadeFormulier == null)
            {
                return BadRequest();
            }
            var voertuig = await _huurContext.Voertuigen.FirstOrDefaultAsync(car => car.Kenteken == schadeFormulier.Kenteken);
            if (voertuig == null)
            {

                return NotFound();
            }
            schadeFormulier.ErnstVDSchade = putSchadeformulier.ErnstVDSchade;
            schadeFormulier.SchadeType = putSchadeformulier.SchadeType;
            voertuig.Status = "beschadigd";
            try
            {
                await _huurContext.SaveChangesAsync();
            }
            catch (DBConcurrencyException)
            {
                if (!_huurContext.schadeFormulieren.Any(sch => sch.Id == schadeFormulier.Id))
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
    }
}