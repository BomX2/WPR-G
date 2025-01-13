using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.VoertuigFiles;

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
            // var autos = await _huurContext.autos.ToListAsync();

            // query bouwer aanmaken
            var query = _huurContext.autos.AsQueryable();

            if (queryParams.MinPrijs != null && queryParams.MaxPrijs != null) {
                query = query.Where(v => queryParams.MinPrijs >= v.PrijsPerDag && v.PrijsPerDag <= queryParams.MaxPrijs);
            }

            if (queryParams.StartDatum != null && queryParams.EindDatum != null)
            {
                // we hebben aanvragen nodig context om te te kijken op startdatum en eindatum
                // om namelijk te kijken of een voertuig beschikbaar is moet je kunnen kijken
                // of er aanvragen zijn de overlappen met de begindatum en einddatum

                // als dit is gelukt zijn de stappen:
                // 1. aanvragen ophalen waar geld: aanvraag.StartDatum <= queryParams.EindDatum && aanvraag.EindDatm >= queryParams.StartDatum
                // 2. query = query.where(v => !bezetteKentekens}.Contains(v.Kenteken));
            }

            var voertuigen = await query.ToListAsync();

            return Ok(voertuigen);
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
