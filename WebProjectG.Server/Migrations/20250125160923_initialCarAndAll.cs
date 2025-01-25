using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations
{
    /// <inheritdoc />
    public partial class initialCarAndAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abonnement",
                columns: table => new
                {
                    AbonnementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbonnementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BetaalMethode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Periode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnement", x => x.AbonnementID);
                });

            migrationBuilder.CreateTable(
                name: "Voertuigen",
                columns: table => new
                {
                    Kenteken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HuurStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Merk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kleur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AanschafJaar = table.Column<int>(type: "int", nullable: false),
                    PrijsPerDag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InclusiefVerzekering = table.Column<bool>(type: "bit", nullable: false),
                    soort = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voertuigen", x => x.Kenteken);
                });

            migrationBuilder.CreateTable(
                name: "Bedrijf",
                columns: table => new
                {
                    KvkNummer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BedrijfsNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomeinNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AbonnementID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrijf", x => x.KvkNummer);
                    table.ForeignKey(
                        name: "FK_Bedrijf_Abonnement_AbonnementID",
                        column: x => x.AbonnementID,
                        principalTable: "Abonnement",
                        principalColumn: "AbonnementID");
                });

            migrationBuilder.CreateTable(
                name: "autos",
                columns: table => new
                {
                    Kenteken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AantalDeuren = table.Column<int>(type: "int", nullable: false),
                    BrandstofType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeeftAirco = table.Column<bool>(type: "bit", nullable: false),
                    BrandstofVerbruik = table.Column<double>(type: "float", nullable: false),
                    TransmissieType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bagageruimte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autos", x => x.Kenteken);
                    table.ForeignKey(
                        name: "FK_autos_Voertuigen_Kenteken",
                        column: x => x.Kenteken,
                        principalTable: "Voertuigen",
                        principalColumn: "Kenteken",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "campers",
                columns: table => new
                {
                    Kenteken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lengte = table.Column<double>(type: "float", nullable: false),
                    Hoogte = table.Column<double>(type: "float", nullable: false),
                    Slaapplaatsen = table.Column<int>(type: "int", nullable: false),
                    HeeftBadkamer = table.Column<bool>(type: "bit", nullable: false),
                    HeeftKeuken = table.Column<bool>(type: "bit", nullable: false),
                    WaterTankCapaciteit = table.Column<double>(type: "float", nullable: false),
                    AfvalTankCapaciteit = table.Column<double>(type: "float", nullable: false),
                    BrandstofVerbruik = table.Column<double>(type: "float", nullable: false),
                    HeeftZonnepanelen = table.Column<bool>(type: "bit", nullable: false),
                    FietsRekCapaciteit = table.Column<int>(type: "int", nullable: false),
                    HeeftLuifel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campers", x => x.Kenteken);
                    table.ForeignKey(
                        name: "FK_campers_Voertuigen_Kenteken",
                        column: x => x.Kenteken,
                        principalTable: "Voertuigen",
                        principalColumn: "Kenteken",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "caravans",
                columns: table => new
                {
                    Kenteken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lengte = table.Column<double>(type: "float", nullable: false),
                    Slaapplaatsen = table.Column<int>(type: "int", nullable: false),
                    HeeftKeuken = table.Column<bool>(type: "bit", nullable: false),
                    WaterTankCapaciteit = table.Column<double>(type: "float", nullable: false),
                    AfvalTankCapaciteit = table.Column<double>(type: "float", nullable: false),
                    HeeftLuifel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caravans", x => x.Kenteken);
                    table.ForeignKey(
                        name: "FK_caravans_Voertuigen_Kenteken",
                        column: x => x.Kenteken,
                        principalTable: "Voertuigen",
                        principalColumn: "Kenteken",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gebruiker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KvkNummer = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruiker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gebruiker_Bedrijf_KvkNummer",
                        column: x => x.KvkNummer,
                        principalTable: "Bedrijf",
                        principalColumn: "KvkNummer");
                });

            migrationBuilder.CreateTable(
                name: "Aanvragen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ophaaltijd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inlevertijd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefoonnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goedgekeurd = table.Column<bool>(type: "bit", nullable: true),
                    Gebruikerid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Kenteken = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aanvragen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aanvragen_Gebruiker_Gebruikerid",
                        column: x => x.Gebruikerid,
                        principalTable: "Gebruiker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aanvragen_Voertuigen_Kenteken",
                        column: x => x.Kenteken,
                        principalTable: "Voertuigen",
                        principalColumn: "Kenteken",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schadeFormulieren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchadeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kenteken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoertuigKenteken = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AanvraagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schadeFormulieren", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schadeFormulieren_Aanvragen_AanvraagId",
                        column: x => x.AanvraagId,
                        principalTable: "Aanvragen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schadeFormulieren_Voertuigen_VoertuigKenteken",
                        column: x => x.VoertuigKenteken,
                        principalTable: "Voertuigen",
                        principalColumn: "Kenteken");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aanvragen_Gebruikerid",
                table: "Aanvragen",
                column: "Gebruikerid");

            migrationBuilder.CreateIndex(
                name: "IX_Aanvragen_Kenteken",
                table: "Aanvragen",
                column: "Kenteken");

            migrationBuilder.CreateIndex(
                name: "IX_Bedrijf_AbonnementID",
                table: "Bedrijf",
                column: "AbonnementID");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_KvkNummer",
                table: "Gebruiker",
                column: "KvkNummer");

            migrationBuilder.CreateIndex(
                name: "IX_schadeFormulieren_AanvraagId",
                table: "schadeFormulieren",
                column: "AanvraagId");

            migrationBuilder.CreateIndex(
                name: "IX_schadeFormulieren_VoertuigKenteken",
                table: "schadeFormulieren",
                column: "VoertuigKenteken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "autos");

            migrationBuilder.DropTable(
                name: "campers");

            migrationBuilder.DropTable(
                name: "caravans");

            migrationBuilder.DropTable(
                name: "schadeFormulieren");

            migrationBuilder.DropTable(
                name: "Aanvragen");

            migrationBuilder.DropTable(
                name: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Voertuigen");

            migrationBuilder.DropTable(
                name: "Bedrijf");

            migrationBuilder.DropTable(
                name: "Abonnement");
        }
    }
}
