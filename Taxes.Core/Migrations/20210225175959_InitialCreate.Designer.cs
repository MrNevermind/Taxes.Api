﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taxes.Core;

namespace Taxes.Core.Migrations
{
    [DbContext(typeof(TaxesContext))]
    [Migration("20210225175959_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Taxes.Core.Tables.TaxTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Municipality")
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDate");

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.HasIndex("EndDate");

                    b.HasIndex("Municipality");

                    b.HasIndex("StartDate");

                    b.ToTable("Tax");
                });
#pragma warning restore 612, 618
        }
    }
}
