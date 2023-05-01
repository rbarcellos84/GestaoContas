﻿// <auto-generated />
using System;
using GestaoContas.InfraEstrutura.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestaoContas.InfraEstrutura.Migrations
{
    [DbContext(typeof(SqlServerContext))]
    [Migration("20230427045309_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GestaoContas.Dominio.Entities.Conta", b =>
                {
                    b.Property<Guid>("IdConta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IDCONTA");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATA");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("NOME");

                    b.Property<int>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("TIPO");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("VALOR");

                    b.HasKey("IdConta");

                    b.ToTable("CONTA", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}