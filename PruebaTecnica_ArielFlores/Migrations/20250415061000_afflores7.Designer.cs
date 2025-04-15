﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaTecnica_ArielFlores.Context;

#nullable disable

namespace PruebaTecnica_ArielFlores.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250415061000_afflores7")]
    partial class afflores7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("PruebaTecnica_ArielFlores.Models.Entidades.Clientes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Ingresos")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("clientes");
                });

            modelBuilder.Entity("PruebaTecnica_ArielFlores.Models.Entidades.Cuentas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientesId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClientesId");

                    b.ToTable("cuentas");
                });

            modelBuilder.Entity("PruebaTecnica_ArielFlores.Models.Entidades.Cuentas", b =>
                {
                    b.HasOne("PruebaTecnica_ArielFlores.Models.Entidades.Clientes", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });
#pragma warning restore 612, 618
        }
    }
}
