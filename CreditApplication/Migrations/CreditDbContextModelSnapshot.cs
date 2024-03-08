﻿// <auto-generated />
using System;
using CreditApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CreditApplication.Migrations
{
    [DbContext(typeof(CreditDbContext))]
    partial class CreditDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CreditApplication.Models.Credit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreditRateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullMoneyAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifyDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MonthPayAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PayingAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RemainingDebt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnpaidDebt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreditRateId");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("CreditApplication.Models.CreditRate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MonthPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CreditRates");
                });

            modelBuilder.Entity("CreditApplication.Models.Credit", b =>
                {
                    b.HasOne("CreditApplication.Models.CreditRate", "CreditRate")
                        .WithMany()
                        .HasForeignKey("CreditRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditRate");
                });
#pragma warning restore 612, 618
        }
    }
}
