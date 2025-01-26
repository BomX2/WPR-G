using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.VoertuigFiles;
using WebProjectG.Server.domain.Huur;
using System.Data;
using WebProjectG.Server.domain.GebruikerFiles.Dtos;

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

                int ophaalTijd = dagdelen[queryParams.OphaalTijd.ToLower().Trim()];
                int inleverTijd = dagdelen[queryParams.InleverTijd.ToLower().Trim()];

                if (!dagdelen.TryGetValue(queryParams.OphaalTijd.ToLower().Trim(), out var OphaalTijd) ||
                    !dagdelen.TryGetValue(queryParams.InleverTijd.ToLower().Trim(), out var InleverTijd))
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
                        dagdelen[a.Ophaaltijd.ToLower().Trim()] <= inleverTijd &&
                        dagdelen[a.Inlevertijd.ToLower().Trim()] >= ophaalTijd)
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
            if (voertuig == null) return NotFound(new { message = "Auto niet gevonden" });

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
                    extraInfo = await _huurContext.caravans.FirstOrDefaultAsync(a => a.Kenteken == Kenteken);
                    break;

                default:
                    return BadRequest(new { message = "Ongeldige voertuige soort" });
            }

            return Ok(extraInfo);
        }

        [HttpGet("merken")]
        public async Task<IActionResult> GetMerken([FromQuery] string soort)
        {
            var merken = await _huurContext.Voertuigen
                .Where(v => v.soort.ToLower() == soort.ToLower())
                .Select(v => v.Merk)
                .Distinct()
                .ToListAsync();

            return Ok(merken);
        }
        [HttpPut("RepareerVoertuig/{kenteken}")]
        public async Task<IActionResult> RepareerAuto(string kenteken)
        {
            var voertuig = await _huurContext.Voertuigen.FirstOrDefaultAsync(auto => auto.Kenteken == kenteken);
            if (voertuig == null)
            {
                return BadRequest(); 
            }
            voertuig.Status = "Gerepareerd";
            try
            {
                await _huurContext.SaveChangesAsync();
            } 
            catch (DBConcurrencyException)
            {
                if (!_huurContext.Voertuigen.Any(v => v.Kenteken == kenteken)) {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost("createVoertuig")]
        public async Task<ActionResult> CreateVoertuig([FromBody] UpdateVoertuigDto voertuigDto)
        {
            if (voertuigDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var voertuig = new Voertuig(
                huurStatus: voertuigDto.HuurStatus,
                merk: voertuigDto.Merk,
                type: voertuigDto.Type,
                kenteken: voertuigDto.Kenteken,
                kleur: voertuigDto.Kleur,
                aanschafJaar: voertuigDto.AanschafJaar,
                prijsPerDag: voertuigDto.PrijsPerDag,
                inclusiefVerzekering: voertuigDto.InclusiefVerzekering
            )
            {
                soort = voertuigDto.Soort?.ToLower()
            };

            await _huurContext.Voertuigen.AddAsync(voertuig);

            switch (voertuigDto.Soort?.ToLower())
            {
                case "auto":
                    if (voertuigDto is not AutoDto autoDto)
                        return BadRequest(new { message = "Ontbrekende of ongeldige gegevens voor Auto." });

                    var auto = new Auto
                    {
                        Kenteken = voertuig.Kenteken,
                        Voertuig = voertuig,
                        AantalDeuren = autoDto.AantalDeuren,
                        BrandstofType = autoDto.BrandstofType,
                        HeeftAirco = autoDto.HeeftAirco,
                        BrandstofVerbruik = autoDto.BrandstofVerbruik,
                        TransmissieType = autoDto.TransmissieType,
                        Bagageruimte = autoDto.Bagageruimte
                    };
                    await _huurContext.autos.AddAsync(auto);
                    break;

                case "camper":
                    if (voertuigDto is not CamperDto camperDto)
                        return BadRequest(new { message = "Ontbrekende of ongeldige gegevens voor Camper." });

                    var camper = new Camper
                    {
                        Kenteken = voertuig.Kenteken,
                        Voertuig = voertuig,
                        Lengte = camperDto.Lengte,
                        Hoogte = camperDto.Hoogte,
                        Slaapplaatsen = camperDto.Slaapplaatsen,
                        HeeftBadkamer = camperDto.HeeftBadkamer,
                        HeeftKeuken = camperDto.HeeftKeuken,
                        WaterTankCapaciteit = camperDto.WaterTankCapaciteit,
                        AfvalTankCapaciteit = camperDto.AfvalTankCapaciteit,
                        BrandstofVerbruik = camperDto.BrandstofVerbruik,
                        HeeftZonnepanelen = camperDto.HeeftZonnepanelen,
                        FietsRekCapaciteit = camperDto.FietsRekCapaciteit,
                        HeeftLuifel = camperDto.HeeftLuifel
                    };
                    await _huurContext.campers.AddAsync(camper);
                    break;

                case "caravan":
                    if (voertuigDto is not CaravanDto caravanDto)
                        return BadRequest(new { message = "Ontbrekende of ongeldige gegevens voor Caravan." });

                    var caravan = new Caravan
                    {
                        Kenteken = voertuig.Kenteken,
                        Voertuig = voertuig,
                        Lengte = caravanDto.Lengte,
                        Slaapplaatsen = caravanDto.Slaapplaatsen,
                        HeeftKeuken = caravanDto.HeeftKeuken,
                        WaterTankCapaciteit = caravanDto.WaterTankCapaciteit,
                        AfvalTankCapaciteit = caravanDto.AfvalTankCapaciteit,
                        HeeftLuifel = caravanDto.HeeftLuifel
                    };
                    await _huurContext.caravans.AddAsync(caravan);
                    break;

                default:
                    return BadRequest(new { message = "Ongeldig voertuig soort." });
            }

            await _huurContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutoById), new { Kenteken = voertuig.Kenteken }, voertuig);
        }

        [HttpPut("updateVoertuig/{Kenteken}")]
        public async Task<ActionResult> UpdateVoertuig(string Kenteken, [FromBody] UpdateVoertuigDto voertuigDto)
        {
            if (voertuigDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var voertuig = await _huurContext.Voertuigen.FindAsync(Kenteken);
            if (voertuig == null)
                return NotFound(new { message = "Voertuig niet gevonden." });

            voertuig.HuurStatus = voertuigDto.HuurStatus;
            voertuig.Merk = voertuigDto.Merk;
            voertuig.Type = voertuigDto.Type;
            voertuig.Kleur = voertuigDto.Kleur;
            voertuig.AanschafJaar = voertuigDto.AanschafJaar;
            voertuig.PrijsPerDag = voertuigDto.PrijsPerDag;
            voertuig.InclusiefVerzekering = voertuigDto.InclusiefVerzekering;
            voertuig.soort = voertuigDto.Soort?.ToLower();

            switch (voertuigDto.Soort?.ToLower())
            {
                case "auto":
                    var existingAuto = await _huurContext.autos.FirstOrDefaultAsync(a => a.Kenteken == Kenteken);
                    if (existingAuto == null)
                        return NotFound(new { message = "Bijbehorende Auto gegevens niet gevonden." });

                    if (voertuigDto is not AutoDto autoDto)
                        return BadRequest(new { message = "Ontbrekende of ongeldige gegevens voor Auto." });

                    existingAuto.AantalDeuren = autoDto.AantalDeuren;
                    existingAuto.BrandstofType = autoDto.BrandstofType;
                    existingAuto.HeeftAirco = autoDto.HeeftAirco;
                    existingAuto.BrandstofVerbruik = autoDto.BrandstofVerbruik;
                    existingAuto.TransmissieType = autoDto.TransmissieType;
                    existingAuto.Bagageruimte = autoDto.Bagageruimte;
                    break;

                case "camper":
                    var existingCamper = await _huurContext.campers.FirstOrDefaultAsync(c => c.Kenteken == Kenteken);
                    if (existingCamper == null)
                        return NotFound(new { message = "Bijbehorende Camper gegevens niet gevonden." });

                    if (voertuigDto is not CamperDto camperDto)
                        return BadRequest(new { message = "Ontbrekende of ongeldige gegevens voor Camper." });

                    existingCamper.Lengte = camperDto.Lengte;
                    existingCamper.Hoogte = camperDto.Hoogte;
                    existingCamper.Slaapplaatsen = camperDto.Slaapplaatsen;
                    existingCamper.HeeftBadkamer = camperDto.HeeftBadkamer;
                    existingCamper.HeeftKeuken = camperDto.HeeftKeuken;
                    existingCamper.WaterTankCapaciteit = camperDto.WaterTankCapaciteit;
                    existingCamper.AfvalTankCapaciteit = camperDto.AfvalTankCapaciteit;
                    existingCamper.BrandstofVerbruik = camperDto.BrandstofVerbruik;
                    existingCamper.HeeftZonnepanelen = camperDto.HeeftZonnepanelen;
                    existingCamper.FietsRekCapaciteit = camperDto.FietsRekCapaciteit;
                    existingCamper.HeeftLuifel = camperDto.HeeftLuifel;
                    break;

                case "caravan":
                    var existingCaravan = await _huurContext.caravans.FirstOrDefaultAsync(c => c.Kenteken == Kenteken);
                    if (existingCaravan == null)
                        return NotFound(new { message = "Bijbehorende Caravan gegevens niet gevonden." });

                    if (voertuigDto is not CaravanDto caravanDto)
                        return BadRequest(new { message = "Ontbrekende of ongeldige gegevens voor Caravan." });

                    existingCaravan.Lengte = caravanDto.Lengte;
                    existingCaravan.Slaapplaatsen = caravanDto.Slaapplaatsen;
                    existingCaravan.HeeftKeuken = caravanDto.HeeftKeuken;
                    existingCaravan.WaterTankCapaciteit = caravanDto.WaterTankCapaciteit;
                    existingCaravan.AfvalTankCapaciteit = caravanDto.AfvalTankCapaciteit;
                    existingCaravan.HeeftLuifel = caravanDto.HeeftLuifel;
                    break;

                default:
                    return BadRequest(new { message = "Ongeldige voertuig soort." });
            }

            await _huurContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
