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

            var bedrijf = await _dbContext.Bedrijven.Include(b => b.Abonnement).FirstOrDefaultAsync(b => b.KvkNummer.ToLower() == kvkNummer.ToLower());
            if (bedrijf == null)
            {
                return NotFound($"Bedrijf with KvkNummer '{kvkNummer}' was not found.");
            }
            if(bedrijf.Abonnement == null)
            {
                bedrijf.Abonnement = new Abonnement
                {
                    AbonnementType = dto.AbonnementType,

                };  
            }
            else
            {
                bedrijf.Abonnement.AbonnementType = dto.AbonnementType;
            }
             _dbContext.Entry(bedrijf).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DBConcurrencyException)
            {
                return Conflict();
            }
            return NoContent();
        }

        [HttpPost("AddGebruikerToBedrijf/{kvkNummer}")]
        public async Task<ActionResult> VoekgMedewerkerToe(string kvkNummer, string email, string domeinNaam)
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