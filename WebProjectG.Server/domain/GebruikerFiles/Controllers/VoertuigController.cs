using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.VoertuigFiles;
using WebProjectG.Server.domain.Huur;

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
            var query = _huurContext.Voertuigen.AsQueryable();

            if (queryParams.StartDatum != null && queryParams.EindDatum != null)
            {
                var startDatum = queryParams.StartDatum.Value;
                var eindDatum = queryParams.EindDatum.Value;

                var bezetteAuto = await _huurContext.Aanvragen
                    .Where(a => a.StartDatum <= eindDatum &&
                                a.EindDatum >= startDatum)
                    .Select(a => a.Kenteken)
                    .Distinct()
                    .ToListAsync();

                query = query.Where(v => !bezetteAuto.Contains(v.Kenteken));
            }

            var voertuigen = await query.ToListAsync();

            return Ok(voertuigen);
        }

        [HttpGet("getByKenteken/{Kenteken}")]
        public async Task<ActionResult> GetAutoById(String Kenteken)
        {
            var voertuig = await _huurContext.Voertuigen.FindAsync(Kenteken);
            if (voertuig == null) return NotFound(new { message = "Auto not found" });

            object extraInfo = null;

            switch (voertuig.soort.ToLower())
            {
                case "auto":
                    extraInfo = await _huurContext.autos.FirstOrDefaultAsync(a => a.Kenteken == Kenteken);
                    break;

                case "camper":
                    extraInfo = await _huurContext.campers.FirstOrDefaultAsync(a => a.Kenteken == Kenteken);
                    break;

                case "caravan":
                    extraInfo = await _huurContext.caravans.FirstOrDefaultAsync(a => a.Kenteken== Kenteken);
                    break;

                default:
                    return BadRequest(new { message = "invalid voertuig type" });
            }

            return Ok(extraInfo);
        }
    }
}
