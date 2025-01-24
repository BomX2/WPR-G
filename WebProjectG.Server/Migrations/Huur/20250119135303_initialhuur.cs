using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations.Huur
{
    /// <inheritdoc />
    public partial class initialhuur : Migration
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
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnement", x => x.AbonnementID);
                });

            migrationBuilder.CreateTable(
                name: "autos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AantalDeuren = table.Column<int>(type: "int", nullable: false),
                    BrandstofType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeeftAirco = table.Column<bool>(type: "bit", nullable: false),
                    BrandstofVerbruik = table.Column<double>(type: "float", nullable: false),
                    TransmissieType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bagageruimte = table.Column<int>(type: "int", nullable: false),
                    HuurStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Merk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kenteken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kleur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AanschafJaar = table.Column<int>(type: "int", nullable: false),
                    PrijsPerDag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InclusiefVerzekering = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autos", x => x.Id);
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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Goedgekeurd = table.Column<bool>(type: "bit", nullable: true),
                    Gebruikerid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aanvragen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aanvragen_Gebruiker_Gebruikerid",
                        column: x => x.Gebruikerid,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aanvragen_autos_AutoId",
                        column: x => x.AutoId,
                        principalTable: "autos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aanvragen_AutoId",
                table: "Aanvragen",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aanvragen_Gebruikerid",
                table: "Aanvragen",
                column: "Gebruikerid");

            migrationBuilder.CreateIndex(
                name: "IX_Bedrijf_AbonnementID",
                table: "Bedrijf",
                column: "AbonnementID");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_KvkNummer",
                table: "Gebruiker",
                column: "KvkNummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aanvragen");

            migrationBuilder.DropTable(
                name: "Gebruiker");

            migrationBuilder.DropTable(
                name: "autos");

            migrationBuilder.DropTable(
                name: "Bedrijf");

            migrationBuilder.DropTable(
                name: "Abonnement");
        }
    }
}
