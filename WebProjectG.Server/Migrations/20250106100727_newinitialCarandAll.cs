﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProjectG.Server.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:WebProjectG.Server/Migrations/20250106100727_newinitialCarandAll.cs
    public partial class newinitialCarandAll : Migration
========
    public partial class initialHuur : Migration
>>>>>>>> origin/BedrijfUitwerking:WebProjectG.Server/Migrations/20250115134342_initialHuur.cs
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Aanvragen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersoonsGegevens = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefoonnummer = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Goedgekeurd = table.Column<bool>(type: "bit", nullable: true),
                    AutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aanvragen", x => x.Id);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aanvragen");

            migrationBuilder.DropTable(
                name: "autos");
        }
    }
}
