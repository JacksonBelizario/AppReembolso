﻿// <auto-generated />
using System;
using App.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Web.Migrations
{
    [DbContext(typeof(SistemaDbContext))]
    partial class SistemaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("App.Web.Models.Solicitacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Anexo")
                        .HasColumnName("anexo")
                        .HasColumnType("text");

                    b.Property<int>("Categoria")
                        .HasColumnName("categoria")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataDaCompra")
                        .HasColumnName("data_da_compra")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataDaSolicitacao")
                        .HasColumnName("data_da_solicitacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Descricao")
                        .HasColumnName("descricao")
                        .HasColumnType("text");

                    b.Property<int>("Latitude")
                        .HasColumnName("latitude")
                        .HasColumnType("integer");

                    b.Property<int>("Longitude")
                        .HasColumnName("longitude")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("integer");

                    b.Property<double>("Valor")
                        .HasColumnName("valor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("solicitacao");
                });
#pragma warning restore 612, 618
        }
    }
}
