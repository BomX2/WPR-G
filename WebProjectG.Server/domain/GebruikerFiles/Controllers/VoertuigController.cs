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



            if (queryParams.StartDatum != null && queryParams.EindDatum != null &&
        !string.IsNullOrEmpty(queryParams.OphaalTijd) && !string.IsNullOrEmpty(queryParams.InleverTijd))
            {
                var startDatum = queryParams.StartDatum.Value;
                var eindDatum = queryParams.EindDatum.Value;

                // Zet ophaaltijd en inlevertijd om naar numerieke waarden voor vergelijking (bijvoorbeeld ochtend = 0, middag = 1, avond = 2)
                var dagdelen = new Dictionary<string, int> { { "ochtend", 0 }, { "middag", 1 }, { "avond", 2 } };

                int OphaalTijd = dagdelen[queryParams.OphaalTijd.ToLower().Trim()];
                int InleverTijd = dagdelen[queryParams.InleverTijd.ToLower().Trim()];

                if (!dagdelen.TryGetValue(queryParams.OphaalTijd.ToLower().Trim(), out var ophaalTijd) ||
                    !dagdelen.TryGetValue(queryParams.InleverTijd.ToLower().Trim(), out var inleverTijd))
                {
                    return BadRequest("OphaalTijd of InleverTijd is ongeldig.");
                }

                // Haal alle aanvragen op die relevant zijn
                var aanvragen = await _huurContext.Aanvragen
                    .Where(a =>
                        a.StartDatum < eindDatum ||
                        (a.StartDatum == eindDatum && a.StartDatum < eindDatum) ||
                        (a.EindDatum > startDatum) ||
                        (a.EindDatum == startDatum))
                    .ToListAsync();

                // Filter de aanvragen verder in-memory
                var bezetteAuto = aanvragen
                    .Where(a =>
                        dagdelen[a.ophaaltijd.ToLower().Trim()] <= InleverTijd &&
                        dagdelen[a.inlevertijd.ToLower().Trim()] >= OphaalTijd)
                    .Select(a => a.Kenteken)
                    .Distinct();

                // Filter voertuigen die niet beschikbaar zijn
                query = query.Where(v => !bezetteAuto.Contains(v.Kenteken));
            }

            // Haal gefilterde voertuigen op
            var gefilterdeVoertuigen = await query.ToListAsync();

            // Controleer welke soort is opgegeven en haal de bijbehorende objecten op
            if (!string.IsNullOrEmpty(queryParams.Soort))
            {
                if (queryParams.Soort.ToLower() == "auto")
                {
                    // Haal alleen auto's op die overeenkomen met de gefilterde kentekens
                    var autos = await _huurContext.autos
                        .Where(a => gefilterdeVoertuigen.Select(v => v.Kenteken).Contains(a.Kenteken))
                        .Include(a => a.Voertuig) // Koppel Voertuig-informatie
                        .ToListAsync();

                    return Ok(autos);
                }
                else if (queryParams.Soort.ToLower() == "camper")
                {
                    var campers = await _huurContext.campers
                        .Where(c => gefilterdeVoertuigen.Select(v => v.Kenteken).Contains(c.Kenteken))
                        .Include(c => c.Voertuig)
                        .ToListAsync();

                    return Ok(campers);
                }
                else if (queryParams.Soort.ToLower() == "caravan")
                {
                    var caravans = await _huurContext.caravans
                        .Where(c => gefilterdeVoertuigen.Select(v => v.Kenteken).Contains(c.Kenteken))
                        .Include(c => c.Voertuig)
                        .ToListAsync();

                    return Ok(caravans);
                }
                else
                {
                    return BadRequest("Ongeldige soort opgegeven. Kies uit 'auto', 'camper', of 'caravan'.");
                }
            }

            // Als geen specifieke soort is opgegeven, retourneer de voertuigen zonder filtering
            return Ok(gefilterdeVoertuigen);
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
