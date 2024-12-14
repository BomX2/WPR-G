using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SQLitePCL;
using WebProjectG.Server.domain;
using WebProjectG.Server.domain.Gebruiker;
using WebProjectG.Server.domain.GebruikerFiles.Dtos;

namespace WebProjectG.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {

        private readonly HuurContext _huurContext;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly SignInManager<Gebruiker> _signInManager;
        public GebruikerController(
        HuurContext huurContext,
        UserManager<Gebruiker> userManager,
        SignInManager<Gebruiker> signInManager)
        {
            _huurContext = huurContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("postbedrijf")]
        public async Task<ActionResult<Bedrijf>> PostBedrijf(Bedrijf bedrijf)
        {
            _huurContext.Bedrijven.Add(bedrijf);
            await _huurContext.SaveChangesAsync();
            return CreatedAtAction("GetGebruiker", new {id = bedrijf.Id}, bedrijf);
        }
        [HttpPut("putBedrijfsAbonnement/{id}")]
        public async Task<IActionResult> PutBedrijf(int id, BedrijfPutDto dto)
        {
            {
                if (id != dto.Id)
                {
                    return BadRequest();
                }
                var bedrijf = await _huurContext.Bedrijven.Include(b => b.Abonnement).FirstOrDefaultAsync(b => b.Id == id); 
                if (bedrijf == null)
                {
                    return NotFound();
                }
                if (bedrijf.Abonnement == null)
                {
                    bedrijf.Abonnement = new Abonnement();
                }
                bedrijf.Abonnement.AbonnementType = dto.AbonnementType;
                _huurContext.Entry(bedrijf).State = EntityState.Modified;
                try
                {
                    await _huurContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_huurContext.Bedrijven.Any(e => e.Id == id))
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
            [HttpPost("postGebruiker")]
        public async Task<ActionResult<Gebruiker>> PostGebruiker(Gebruiker gebruiker)
        {
            _huurContext.gebruikers.Add(gebruiker);
            await _huurContext.SaveChangesAsync();

            return CreatedAtAction("GetGebruiker", new { id = gebruiker.Id}, gebruiker);
        }
  
        [HttpGet("Getgebruiker{id}")]
        public async Task<ActionResult<Gebruiker>> GetGebruiker(int id)
        {
            var gebruiker = await _huurContext.gebruikers.FindAsync(id); 

            if (gebruiker == null)
            {
                return NotFound();
            }

            return Ok(gebruiker);
        }

        [HttpPut("update")]
        public async Task<IActionResult> PutGebruiker(string id, Gebruiker gebruiker)
        {
            if (id != gebruiker.Id) { return BadRequest(); }

            _huurContext.Entry(gebruiker).State = EntityState.Modified;

            try { await _huurContext.SaveChangesAsync(); }

            catch (DbUpdateConcurrencyException)
            {
                if (!_huurContext.gebruikers.Any(e => gebruiker.Id == id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteGebruiker(int id)
        {
            var gebruiker = await _huurContext.gebruikers.FindAsync(id);
            if (gebruiker == null) { return NotFound(); }

            _huurContext.gebruikers.Remove(gebruiker);
            await _huurContext.SaveChangesAsync();

            return NoContent();
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
                return Ok(new { message = "Login successful" });
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
    }
}
