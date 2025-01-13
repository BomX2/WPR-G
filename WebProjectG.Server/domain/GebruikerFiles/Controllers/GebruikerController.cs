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
                UserName = model.Email,
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
    if (!ModelState.IsValid)
    {
        return BadRequest(new { message = "Invalid data provided." });
    }

    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

    if (result.Succeeded)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        // Ensure the user exists
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user); // Fetch roles for the user

            await _signInManager.SignInAsync(user, model.RememberMe);

            return Ok(new
            {
                message = "Login successful",
                id = user.Id,
                name = user.UserName, // Assuming UserName holds the user's name
                email = user.Email,
                role = roles.FirstOrDefault() // Return the first role, if any
            });
        }
    }

    return Unauthorized(new { message = "Invalid email or password." });
}


        //Logout a user
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }

        //Fetch user details
        [HttpGet("{id}")]
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
                .Select(aanv => new { aanv.Id, aanv.PersoonsGegevens, aanv.StartDatum, aanv.EindDatum, aanv.Email, aanv.Telefoonnummer, AutoType = aanv.Auto.Type, AutoMerk = aanv.Auto.Merk })
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
                .Select(aanv => new { aanv.Id, aanv.PersoonsGegevens, aanv.StartDatum, aanv.EindDatum, aanv.Email, aanv.Telefoonnummer, aanv.Status, AutoType = aanv.Auto.Type, AutoMerk = aanv.Auto.Merk })
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
            var aanvragen = await _huurContext.Aanvragen.Where(aanv => aanv.AutoId == id).ToListAsync();
            if (!aanvragen.Any()) return NotFound();

            return Ok(aanvragen);
        }

        [HttpPost("postbedrijf")]
        public async Task<ActionResult> PostBedrijf(Bedrijf bedrijf)
        {
            _dbContext.Bedrijven.Add(bedrijf);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("putBedrijfsAbonnement/{kvkNummer}")]
        public async Task<IActionResult> PutBedrijf(string kvkNummer, [FromBody] BedrijfPutDto dto)
        {
            if (kvkNummer != dto.KvkNummer) return BadRequest();

            var bedrijf = await _dbContext.Bedrijven.Include(b => b.Abonnement).FirstOrDefaultAsync(b => b.KvkNummer == kvkNummer);
            if (bedrijf == null) return NotFound();

            bedrijf.Abonnement ??= new Abonnement();
            bedrijf.Abonnement.AbonnementType = dto.AbonnementType;

            _dbContext.Entry(bedrijf).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("AddGebruikerTo")]
        public async Task<ActionResult> VoegMedewerkerToe(string kvkNummer, string email)
        {
            var gebruiker = await _userManager.FindByEmailAsync(email);
            if (gebruiker == null) return BadRequest("Gebruiker is niet gevonden");

            var bedrijf = await _dbContext.Bedrijven.Include(b => b.ZakelijkeHuurders).FirstOrDefaultAsync(b => b.KvkNummer == kvkNummer);
            if (bedrijf == null) return BadRequest("Bedrijf niet gevonden");

            if (bedrijf.ZakelijkeHuurders.Any(g => g.Email == email))
            {
                return BadRequest("Gebruiker is al gekoppeld aan dit bedrijf.");
            }

            bedrijf.ZakelijkeHuurders.Add(gebruiker);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("autos")]
        public async Task<ActionResult> GetAutos()
        {
            var autos = await _huurContext.autos.ToListAsync();
            return Ok(autos);
        }

        [HttpGet("getAutoById/{id}")]
        public async Task<ActionResult> GetAutoById(int id)
        {
            var auto = await _huurContext.autos.FindAsync(id);
            if (auto == null) return NotFound(new { message = "Auto not found" });
            return Ok(auto);
        }
    }
}