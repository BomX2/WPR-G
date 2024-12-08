﻿using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<Klant>> PostTest(Klant klant)
        {
            _huurContext.klanten.Add(klant);
            await _huurContext.SaveChangesAsync();

            return CreatedAtAction("GetKlant", new { id = klant.Id}, klant);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Klant>> GetKlant(int id)
        {
            var klant = await _huurContext.klanten.FindAsync(id); 

            if (klant == null)
            {
                return NotFound();
            }

            return Ok(klant);
        }

        [HttpPut]
        public async Task<IActionResult> PutKlant(int id, Klant klant)
        {
            if (id != klant.Id) { return BadRequest(); }

            _huurContext.Entry(klant).State = EntityState.Modified;

            try { await _huurContext.SaveChangesAsync(); }

            catch (DbUpdateConcurrencyException)
            {
                if (!_huurContext.klanten.Any(e => klant.Id == id)) { return NotFound(); }
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
