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
    public class ValuesController : ControllerBase
    {
        private readonly HuurContext _huurContext;

        public ValuesController(HuurContext huurContext)
        {
            _huurContext = huurContext; 
        }

        [HttpPost]
        public async Task<ActionResult<Gebruiker>> PostTest(Gebruiker gebruiker)
        {
            _huurContext.klanten.Add(gebruiker);
            await _huurContext.SaveChangesAsync();

            return CreatedAtAction("GetKlant", new { id = gebruiker.Id}, gebruiker);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gebruiker>> GetKlant(int id)
        {
            var gebruiker = await _huurContext.klanten.FindAsync(id); 

            if (gebruiker == null)
            {
                return NotFound();
            }

            return Ok(gebruiker);
        }

        [HttpPut]
        public async Task<IActionResult> PutKlant(string id, Gebruiker gebruiker)
        {
            if (id != gebruiker.Id) { return BadRequest(); }

            _huurContext.Entry(gebruiker).State = EntityState.Modified;

            try { await _huurContext.SaveChangesAsync(); }

            catch (DbUpdateConcurrencyException)
            {
                if (!_huurContext.klanten.Any(e => gebruiker.Id == id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteKlant(int id)
        {
            var klant = await _huurContext.klanten.FindAsync(id);
            if (klant == null) { return NotFound(); }

            _huurContext.klanten.Remove(klant);
            await _huurContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
