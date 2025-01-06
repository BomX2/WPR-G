using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations.Huur
{
    /// <inheritdoc />
    public partial class AutoAanvraagLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutoId",
                table: "Aanvragen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Aanvragen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aanvragen_AutoId",
                table: "Aanvragen",
                column: "AutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aanvragen_autos_AutoId",
                table: "Aanvragen",
                column: "AutoId",
                principalTable: "autos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aanvragen_autos_AutoId",
                table: "Aanvragen");

            migrationBuilder.DropIndex(
                name: "IX_Aanvragen_AutoId",
                table: "Aanvragen");

            migrationBuilder.DropColumn(
                name: "AutoId",
                table: "Aanvragen");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Aanvragen");
        }
    }
}
