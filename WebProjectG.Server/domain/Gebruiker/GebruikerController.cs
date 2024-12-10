using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProjectG.Server.domain.Gebruiker;

[ApiController]
[Route("api/gebruikers")]
public class GebruikerController : ControllerBase
{
    private readonly UserManager<Gebruiker> _userManager;

    public GebruikerController(UserManager<Gebruiker> userManager)
    {
        _userManager = userManager;
    }

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
}
