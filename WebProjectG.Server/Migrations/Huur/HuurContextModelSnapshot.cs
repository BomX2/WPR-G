﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProjectG.Server.domain;

#nullable disable

namespace WebProjectG.Server.Migrations
{
    [DbContext(typeof(HuurContext))]
    partial class HuurContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EindDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gebruikerid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Goedgekeurd")
                        .HasColumnType("bit");

                    b.Property<string>("InleverTijd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kenteken")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OphaalTijd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefoonnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Gebruikerid");

                    b.HasIndex("Kenteken");

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

                    b.Property<string>("BetaalMethode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Periode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("AbonnementID");

                    b.ToTable("Abonnement");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Huur.SchadeFormulier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AanvraagId")
                        .HasColumnType("int");

                    b.Property<string>("Kenteken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchadeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoertuigKenteken")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AanvraagId");

                    b.HasIndex("VoertuigKenteken");

                    b.ToTable("schadeFormulieren");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.VoertuigFiles.Auto", b =>
                {
                    b.Property<string>("Kenteken")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<string>("TransmissieType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Kenteken");

                    b.ToTable("autos");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.VoertuigFiles.Camper", b =>
                {
                    b.Property<string>("Kenteken")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("AfvalTankCapaciteit")
                        .HasColumnType("float");

                    b.Property<double>("BrandstofVerbruik")
                        .HasColumnType("float");

                    b.Property<int>("FietsRekCapaciteit")
                        .HasColumnType("int");

                    b.Property<bool>("HeeftBadkamer")
                        .HasColumnType("bit");

                    b.Property<bool>("HeeftKeuken")
                        .HasColumnType("bit");

                    b.Property<bool>("HeeftLuifel")
                        .HasColumnType("bit");

                    b.Property<bool>("HeeftZonnepanelen")
                        .HasColumnType("bit");

                    b.Property<double>("Hoogte")
                        .HasColumnType("float");

                    b.Property<double>("Lengte")
                        .HasColumnType("float");

                    b.Property<int>("Slaapplaatsen")
                        .HasColumnType("int");

                    b.Property<double>("WaterTankCapaciteit")
                        .HasColumnType("float");

                    b.HasKey("Kenteken");

                    b.ToTable("campers");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.VoertuigFiles.Caravan", b =>
                {
                    b.Property<string>("Kenteken")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("AfvalTankCapaciteit")
                        .HasColumnType("float");

                    b.Property<bool>("HeeftKeuken")
                        .HasColumnType("bit");

                    b.Property<bool>("HeeftLuifel")
                        .HasColumnType("bit");

                    b.Property<double>("Lengte")
                        .HasColumnType("float");

                    b.Property<int>("Slaapplaatsen")
                        .HasColumnType("int");

                    b.Property<double>("WaterTankCapaciteit")
                        .HasColumnType("float");

                    b.HasKey("Kenteken");

                    b.ToTable("caravans");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.VoertuigFiles.Voertuig", b =>
                {
                    b.Property<string>("Kenteken")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AanschafJaar")
                        .HasColumnType("int");

                    b.Property<string>("HuurStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InclusiefVerzekering")
                        .HasColumnType("bit");

                    b.Property<string>("Kleur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Merk")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrijsPerDag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("soort")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Kenteken");

                    b.ToTable("Voertuigen");
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
                    b.HasOne("WebProjectG.Server.domain.GebruikerFiles.Gebruiker", "Gebruiker")
                        .WithMany()
                        .HasForeignKey("Gebruikerid");

                    b.HasOne("WebProjectG.Server.domain.VoertuigFiles.Voertuig", "voertuig")
                        .WithMany()
                        .HasForeignKey("Kenteken")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gebruiker");

                    b.Navigation("voertuig");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.Huur.SchadeFormulier", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.Huur.Aanvraag", "aanvraag")
                        .WithMany()
                        .HasForeignKey("AanvraagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebProjectG.Server.domain.VoertuigFiles.Voertuig", "Voertuig")
                        .WithMany()
                        .HasForeignKey("VoertuigKenteken");

                    b.Navigation("Voertuig");

                    b.Navigation("aanvraag");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.VoertuigFiles.Auto", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.VoertuigFiles.Voertuig", "Voertuig")
                        .WithOne()
                        .HasForeignKey("WebProjectG.Server.domain.VoertuigFiles.Auto", "Kenteken")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Voertuig");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.VoertuigFiles.Camper", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.VoertuigFiles.Voertuig", "Voertuig")
                        .WithOne()
                        .HasForeignKey("WebProjectG.Server.domain.VoertuigFiles.Camper", "Kenteken")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Voertuig");
                });

            modelBuilder.Entity("WebProjectG.Server.domain.VoertuigFiles.Caravan", b =>
                {
                    b.HasOne("WebProjectG.Server.domain.VoertuigFiles.Voertuig", "Voertuig")
                        .WithOne()
                        .HasForeignKey("WebProjectG.Server.domain.VoertuigFiles.Caravan", "Kenteken")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Voertuig");
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
