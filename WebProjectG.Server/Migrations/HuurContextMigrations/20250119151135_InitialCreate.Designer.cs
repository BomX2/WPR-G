﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProjectG.Server.domain;

#nullable disable

namespace WebProjectG.Server.Migrations.HuurContextMigrations
{
    [DbContext(typeof(HuurContext))]
    [Migration("20250119151135_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebProjectG.Server.domain.BedrijfFiles.Bedrijf", b =>
                {
                    b.Property<string>("KvkNummer")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AbonnementID")
                        .HasColumnType("int");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BedrijfsNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DomeinNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KvkNummer");

                    b.HasIndex("AbonnementID");

                    b.ToTable("Bedrijf");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.GebruikerFiles.Gebruiker", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("KvkNummer")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KvkNummer");

                    b.ToTable("Gebruiker");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Huur.Aanvraag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AutoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EindDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gebruikerid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Goedgekeurd")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AutoId");

                    b.HasIndex("Gebruikerid");

                    b.ToTable("Aanvragen");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Huur.Abonnement", b =>
                {
                    b.Property<int>("AbonnementID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AbonnementID"));

                    b.Property<string>("AbonnementType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("AbonnementID");

                    b.ToTable("Abonnement");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Voertuig.Auto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AanschafJaar")
                        .HasColumnType("int");

                    b.Property<int>("AantalDeuren")
                        .HasColumnType("int");

                    b.Property<int>("Bagageruimte")
                        .HasColumnType("int");

                    b.Property<string>("BrandstofType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("BrandstofVerbruik")
                        .HasColumnType("float");

                    b.Property<bool>("HeeftAirco")
                        .HasColumnType("bit");

                    b.Property<string>("HuurStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InclusiefVerzekering")
                        .HasColumnType("bit");

                    b.Property<string>("Kenteken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kleur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Merk")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrijsPerDag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TransmissieType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("autos");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.BedrijfFiles.Bedrijf", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.Huur.Abonnement", "Abonnement")
                        .WithMany("Bedrijven")
                        .HasForeignKey("AbonnementID");

                    b.Navigation("Abonnement");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.GebruikerFiles.Gebruiker", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.BedrijfFiles.Bedrijf", "Bedrijf")
                        .WithMany("ZakelijkeHuurders")
                        .HasForeignKey("KvkNummer");

                    b.Navigation("Bedrijf");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Huur.Aanvraag", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.Voertuig.Auto", "Auto")
                        .WithMany()
                        .HasForeignKey("AutoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebProjectG.Server.domain.GebruikerFiles.Gebruiker", "Gebruiker")
                        .WithMany()
                        .HasForeignKey("Gebruikerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auto");

                    b.Navigation("Gebruiker");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.BedrijfFiles.Bedrijf", b =>
                {
                    b.Navigation("ZakelijkeHuurders");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Huur.Abonnement", b =>
                {
                    b.Navigation("Bedrijven");
                });
#pragma warning restore 612, 618
        }
    }
}
