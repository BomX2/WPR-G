using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations
{
    /// <inheritdoc />
    public partial class Bedrijfklasse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BedrijfId",
                table: "klanten",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "klanten",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ZakelijkeHuurder_BedrijfId",
                table: "klanten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bedrijven",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedrijfsNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kvknummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domeinnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrijven", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_klanten_BedrijfId",
                table: "klanten",
                column: "BedrijfId");

            migrationBuilder.CreateIndex(
                name: "IX_klanten_ZakelijkeHuurder_BedrijfId",
                table: "klanten",
                column: "ZakelijkeHuurder_BedrijfId");

            migrationBuilder.AddForeignKey(
                name: "FK_klanten_Bedrijven_BedrijfId",
                table: "klanten",
                column: "BedrijfId",
                principalTable: "Bedrijven",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_klanten_Bedrijven_ZakelijkeHuurder_BedrijfId",
                table: "klanten",
                column: "ZakelijkeHuurder_BedrijfId",
                principalTable: "Bedrijven",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_klanten_Bedrijven_BedrijfId",
                table: "klanten");

            migrationBuilder.DropForeignKey(
                name: "FK_klanten_Bedrijven_ZakelijkeHuurder_BedrijfId",
                table: "klanten");

            migrationBuilder.DropTable(
                name: "Bedrijven");

            migrationBuilder.DropIndex(
                name: "IX_klanten_BedrijfId",
                table: "klanten");

            migrationBuilder.DropIndex(
                name: "IX_klanten_ZakelijkeHuurder_BedrijfId",
                table: "klanten");

            migrationBuilder.DropColumn(
                name: "BedrijfId",
                table: "klanten");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "klanten");

            migrationBuilder.DropColumn(
                name: "ZakelijkeHuurder_BedrijfId",
                table: "klanten");
        }
    }
}
