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
        public async Task<ActionResult> CreateVoertuig(
        string huurStatus,
        string merk,
        string type,
        string kenteken,
        string kleur,
        int aanschafJaar,
        decimal prijsPerDag,
        bool inclusiefVerzekering,
        string soort,
        int? aantalDeuren = null,
        string? brandstofType = null,
        bool? heeftAirco = null,
        double? brandstofVerbruik = null,
        string? transmissieType = null,
        int? bagageruimte = null,
        double? lengte = null,
        double? hoogte = null,
        int? slaapplaatsen = null,
        bool? heeftBadkamer = null,
        bool? heeftKeuken = null,
        double? waterTankCapaciteit = null,
        double? afvalTankCapaciteit = null,
        bool? heeftZonnepanelen = null,
        int? fietsRekCapaciteit = null,
        bool? heeftLuifel = null)
        {
            // Create the base Voertuig object
            var voertuig = new Voertuig(
                huurStatus: huurStatus,
                merk: merk,
                type: type,
                kenteken: kenteken,
                kleur: kleur,
                aanschafJaar: aanschafJaar,
                prijsPerDag: prijsPerDag,
                inclusiefVerzekering: inclusiefVerzekering
            )
            {
                soort = soort.ToLower()
            };

            await _huurContext.Voertuigen.AddAsync(voertuig);

            // Create specific subtype based on "soort"
            switch (voertuig.soort)
            {
                case "auto":
                    if (aantalDeuren == null || brandstofType == null || heeftAirco == null || brandstofVerbruik == null || transmissieType == null || bagageruimte == null)
                    {
                        return BadRequest(new { message = "Missing required fields for Auto." });
                    }

                    var auto = new Auto
                    {
                        Kenteken = voertuig.Kenteken,
                        Voertuig = voertuig,
                        AantalDeuren = aantalDeuren.Value,
                        BrandstofType = brandstofType,
                        HeeftAirco = heeftAirco.Value,
                        BrandstofVerbruik = brandstofVerbruik.Value,
                        TransmissieType = transmissieType,
                        Bagageruimte = bagageruimte.Value
                    };
                    await _huurContext.autos.AddAsync(auto);
                    break;

                case "camper":
                    if (lengte == null || hoogte == null || slaapplaatsen == null || heeftBadkamer == null || heeftKeuken == null || waterTankCapaciteit == null || afvalTankCapaciteit == null || brandstofVerbruik == null || heeftZonnepanelen == null || fietsRekCapaciteit == null || heeftLuifel == null)
                    {
                        return BadRequest(new { message = "Missing required fields for Camper." });
                    }

                    var camper = new Camper
                    {
                        Kenteken = voertuig.Kenteken,
                        Voertuig = voertuig,
                        Lengte = lengte.Value,
                        Hoogte = hoogte.Value,
                        Slaapplaatsen = slaapplaatsen.Value,
                        HeeftBadkamer = heeftBadkamer.Value,
                        HeeftKeuken = heeftKeuken.Value,
                        WaterTankCapaciteit = waterTankCapaciteit.Value,
                        AfvalTankCapaciteit = afvalTankCapaciteit.Value,
                        BrandstofVerbruik = brandstofVerbruik.Value,
                        HeeftZonnepanelen = heeftZonnepanelen.Value,
                        FietsRekCapaciteit = fietsRekCapaciteit.Value,
                        HeeftLuifel = heeftLuifel.Value
                    };
                    await _huurContext.campers.AddAsync(camper);
                    break;

                case "caravan":
                    if (lengte == null || slaapplaatsen == null || heeftKeuken == null || waterTankCapaciteit == null || afvalTankCapaciteit == null || heeftLuifel == null)
                    {
                        return BadRequest(new { message = "Missing required fields for Caravan." });
                    }

                    var caravan = new Caravan
                    {
                        Kenteken = voertuig.Kenteken,
                        Voertuig = voertuig,
                        Lengte = lengte.Value,
                        Slaapplaatsen = slaapplaatsen.Value,
                        HeeftKeuken = heeftKeuken.Value,
                        WaterTankCapaciteit = waterTankCapaciteit.Value,
                        AfvalTankCapaciteit = afvalTankCapaciteit.Value,
                        HeeftLuifel = heeftLuifel.Value
                    };
                    await _huurContext.caravans.AddAsync(caravan);
                    break;

                default:
                    return BadRequest(new { message = "Invalid voertuig type." });
            }

            // Save all changes to the database
            await _huurContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutoById), new { Kenteken = voertuig.Kenteken }, voertuig);
        }

    }
}
