using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations
{
    /// <inheritdoc />
    public partial class BedrijfEndpoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domeinnaam",
                table: "Bedrijven");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Domeinnaam",
                table: "Bedrijven",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
