using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectG.Server.domain.VoertuigFiles;
using WebProjectG.Server.domain.Huur;
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
                        dagdelen[a.ophaaltijd.ToLower()] <= inleverTijd &&
                        dagdelen[a.inlevertijd.ToLower()] >= ophaalTijd)
                    .Select(a => a.Kenteken)
                    .Distinct();

                // Filter voertuigen die niet beschikbaar zijn
                query = query.Where(v => !bezetteAuto.Contains(v.Kenteken));
            }

            if (!string.IsNullOrEmpty(queryParams.Soort))
            {
                query = query.Where(v => v.soort.ToLower() == queryParams.Soort.ToLower());
            }

            var voertuigen = await query.ToListAsync();

            return Ok(voertuigen);
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
                    extraInfo = await _huurContext.caravans.FirstOrDefaultAsync(a => a.Kenteken== Kenteken);
                    break;

                default:
                    return BadRequest(new { message = "Ongeldige voertuige soort" });
            }

            return Ok(extraInfo);
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
