﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProjectG.Server.domain;

#nullable disable

namespace WebProjectG.Server.Migrations
{
    [DbContext(typeof(HuurContext))]
    [Migration("20241206145107_Bedrijfklasse")]
    partial class Bedrijfklasse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebProjectG.Server.domain.Gebruiker.Bedrijf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BedrijfsNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Domeinnaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kvknummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bedrijven");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Gebruiker.Klant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefoonnummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("klanten");

                    b.HasDiscriminator().HasValue("Klant");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Gebruiker.WagenParkBeheerder", b =>
                {
                    b.HasBaseType("WebProjectG.Server.domain.Gebruiker.Klant");

                    b.Property<int?>("BedrijfId")
                        .HasColumnType("int");

                    b.HasIndex("BedrijfId");

                    b.HasDiscriminator().HasValue("WagenParkBeheerder");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Gebruiker.ZakelijkeHuurder", b =>
                {
                    b.HasBaseType("WebProjectG.Server.domain.Gebruiker.Klant");

                    b.Property<int?>("BedrijfId")
                        .HasColumnType("int");

                    b.HasIndex("BedrijfId");

                    b.ToTable("klanten", t =>
                        {
                            t.Property("BedrijfId")
                                .HasColumnName("ZakelijkeHuurder_BedrijfId");
                        });

                    b.HasDiscriminator().HasValue("ZakelijkeHuurder");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Gebruiker.WagenParkBeheerder", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.Gebruiker.Bedrijf", null)
                        .WithMany("WagenParkBeheerders")
                        .HasForeignKey("BedrijfId");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Gebruiker.ZakelijkeHuurder", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.Gebruiker.Bedrijf", null)
                        .WithMany("ZakelijkeHuurders")
                        .HasForeignKey("BedrijfId");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Gebruiker.Bedrijf", b =>
                {
                    b.Navigation("WagenParkBeheerders");

                    b.Navigation("ZakelijkeHuurders");
                });
#pragma warning restore 612, 618
        }
    }
}
