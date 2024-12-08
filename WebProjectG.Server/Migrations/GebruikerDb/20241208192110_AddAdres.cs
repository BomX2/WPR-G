using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations.GebruikerDb
{
    /// <inheritdoc />
    public partial class AddAdres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adres",
                table: "AspNetUsers");
        }
    }
}
