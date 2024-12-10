using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProjectG.Server.domain.Gebruiker;
using WebProjectG.Server.domain.Gebruiker.Dto_s;
using System.Threading.Tasks;
using System.Linq;

[ApiController]
[Route("api/gebruikers")]
public class GebruikerController : ControllerBase
{
    private readonly UserManager<Gebruiker> _userManager;
    private readonly SignInManager<Gebruiker> _signInManager;

    public GebruikerController(UserManager<Gebruiker> userManager, SignInManager<Gebruiker> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // 1. Register a new user
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

    // 2. Login a user
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

    // 3. Fetch user details
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

    // 4. Update user details
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

    // 5. Delete a user
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
