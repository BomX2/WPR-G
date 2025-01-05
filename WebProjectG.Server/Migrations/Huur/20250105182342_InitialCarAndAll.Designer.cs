﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProjectG.Server.domain;

#nullable disable

namespace WebProjectG.Server.Migrations.Huur
{
    [DbContext(typeof(HuurContext))]
    [Migration("20250105182342_InitialCarAndAll")]
    partial class InitialCarAndAll
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebProjectG.Server.domain.Huur.Aanvraag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EindDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Goedgekeurd")
                        .HasColumnType("bit");

                    b.Property<string>("PersoonsGegevens")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Telefoonnummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Aanvragen");
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
#pragma warning restore 612, 618
        }
    }
}
