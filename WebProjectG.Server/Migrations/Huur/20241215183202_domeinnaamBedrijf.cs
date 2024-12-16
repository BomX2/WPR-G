using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations.Huur
{
    /// <inheritdoc />
    public partial class domeinnaamBedrijf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DomeinNaam",
                table: "Bedrijven",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DomeinNaam",
                table: "Bedrijven");
        }
    }
}
