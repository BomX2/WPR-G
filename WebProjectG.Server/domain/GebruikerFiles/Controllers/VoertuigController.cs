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
            var query = _huurContext.autos.AsQueryable();

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

        [HttpGet("getAutoById/{id}")]
        public async Task<ActionResult> GetAutoById(int id)
        {
            var auto = await _huurContext.autos.FindAsync(id);
            if (auto == null) return NotFound(new { message = "Auto not found" });
            return Ok(auto);
        }
    }
}
