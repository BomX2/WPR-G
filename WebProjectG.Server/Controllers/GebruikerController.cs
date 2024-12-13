using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SQLitePCL;
using WebProjectG.Server.domain;
using WebProjectG.Server.domain.Gebruiker;

namespace WebProjectG.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly HuurContext _huurContext;

        public GebruikerController(HuurContext huurContext)
        {
            _huurContext = huurContext; 
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
  
        [HttpGet("{id}")]
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
    }
}
