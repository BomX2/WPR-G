using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.VoertuigFiles;
using WebProjectG.Server.domain.Huur;
using Microsoft.AspNetCore.Identity;
using WebProjectG.Server.domain.GebruikerFiles.Dtos;
using WebProjectG.Server.domain.Voertuig;

namespace WebProjectG.Server.domain.GebruikerFiles.Controllers
{
    [ApiController]
    [Route("api/voertuigen")]
    public class VoertuigController : ControllerBase
    {
        private readonly HuurContext _huurContext;

        public VoertuigController(
            HuurContext huurContext)
        {
            _huurContext = huurContext;
        }

        [HttpGet("autos")]
        public async Task<ActionResult> GetAutos([FromQuery] GetVoertuigenDto queryParams)
        {
            var query = _huurContext.autos.AsQueryable();

            if (queryParams.StartDatum != null && queryParams.EindDatum != null)
            {
                var startDatum = queryParams.StartDatum.Value;
                var eindDatum = queryParams.EindDatum.Value;

                var bezetteAuto = await _huurContext.Aanvragen
                    .Where(a => a.StartDatum <= eindDatum &&
                                a.EindDatum >= startDatum)
                    .Select(a => a.AutoId)
                    .Distinct()
                    .ToListAsync();

                query = query.Where(v => !bezetteAuto.Contains(v.Id));
            }

            var voertuigen = await query.ToListAsync();

            return Ok(voertuigen);
        }

        [HttpGet("getAutoById/{id}")]
        public async Task<ActionResult> GetAutoById(int id)
        {
            var auto = await _huurContext.autos.FindAsync(id);
            if (auto == null) return NotFound(new { message = "Auto niet gevonden" });
            return Ok(auto);
        }

        [HttpPost("createAuto")]
        public async Task<ActionResult> CreateAuto([FromBody] Auto newAuto)
        {
            if (newAuto == null) return BadRequest(new { message = "Invalid data" });

            await _huurContext.autos.AddAsync(newAuto);
            await _huurContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAutoById), new { id = newAuto.Id }, newAuto);
        }

        [HttpPut("updateAuto/{id}")]
        public async Task<ActionResult> UpdateAuto(int id, [FromBody] Auto updatedAuto)
        {
            if (id != updatedAuto.Id) return BadRequest(new { message = "ID mismatch" });

            var existingAuto = await _huurContext.autos.FindAsync(id);
            if (existingAuto == null) return NotFound(new { message = "Auto niet gevonden" }); 

            existingAuto.Merk = updatedAuto.Merk;
            existingAuto.Type = updatedAuto.Type;
            existingAuto.Kenteken = updatedAuto.Kenteken;
            existingAuto.Kleur = updatedAuto.Kleur;
            existingAuto.AanschafJaar = updatedAuto.AanschafJaar;

            _huurContext.autos.Update(existingAuto);
            await _huurContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("deleteAuto/{id}")]
        public async Task<ActionResult> DeleteAuto(int id)
        {
            var auto = await _huurContext.autos.FindAsync(id);
            if (auto == null) return NotFound(new { message = "Auto niet gevonden" });

            _huurContext.autos.Remove(auto);
            await _huurContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
