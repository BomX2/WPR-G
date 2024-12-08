using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations
{
    /// <inheritdoc />
    public partial class gebruikerFunc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "klanten");

            migrationBuilder.CreateTable(
                name: "Gebruiker",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BedrijfId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Gebruiker_Bedrijven_BedrijfId",
                        column: x => x.BedrijfId,
                        principalTable: "Bedrijven",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gebruiker_BedrijfId",
                table: "Gebruiker",
                column: "BedrijfId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gebruiker");

            migrationBuilder.CreateTable(
                name: "klanten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefoonnummer = table.Column<int>(type: "int", nullable: false),
                    BedrijfId = table.Column<int>(type: "int", nullable: true),
                    ZakelijkeHuurder_BedrijfId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_klanten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_klanten_Bedrijven_BedrijfId",
                        column: x => x.BedrijfId,
                        principalTable: "Bedrijven",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_klanten_Bedrijven_ZakelijkeHuurder_BedrijfId",
                        column: x => x.ZakelijkeHuurder_BedrijfId,
                        principalTable: "Bedrijven",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_klanten_BedrijfId",
                table: "klanten",
                column: "BedrijfId");

            migrationBuilder.CreateIndex(
                name: "IX_klanten_ZakelijkeHuurder_BedrijfId",
                table: "klanten",
                column: "ZakelijkeHuurder_BedrijfId");
        }
    }
}
