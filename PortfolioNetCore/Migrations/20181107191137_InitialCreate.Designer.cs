﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortfolioNetCore.Core.Model;
using PortfolioNetCore.Persistence;

namespace PortfolioNetCore.Migrations
{
    [DbContext(typeof(PortfolioContext))]
    [Migration("20181107191137_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("PortfolioNetCore.Model.Fund", b =>
                {
                    b.Property<string>("ISINNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BestMonthArrayString");

                    b.Property<string>("Currency");

                    b.Property<string>("Focus");

                    b.Property<string>("Management");

                    b.Property<string>("MaxLossArrayString");

                    b.Property<string>("Name");

                    b.Property<string>("OverFulFilmentArrayString");

                    b.Property<string>("Performance1Year");

                    b.Property<string>("Performance3Year");

                    b.Property<string>("Performance5Year");

                    b.Property<string>("PerformanceActualMinus1");

                    b.Property<string>("PerformanceActualMinus2");

                    b.Property<string>("PerformanceActualMinus3");

                    b.Property<string>("PerformanceActualMinus4");

                    b.Property<string>("PerformanceActualMinus5");

                    b.Property<string>("PerformanceActualMinus6");

                    b.Property<string>("PerformanceActualMinus7");

                    b.Property<string>("PerformanceActualMinus8");

                    b.Property<string>("PerformanceActualMinus9");

                    b.Property<string>("PerformanceAverage");

                    b.Property<string>("PerformanceFromBeggining");

                    b.Property<string>("PerformanceYTD");

                    b.Property<string>("SharpRateArrayString");

                    b.Property<string>("Type");

                    b.Property<string>("Url");

                    b.Property<string>("VolatilityArrayString");

                    b.Property<string>("WorstMonthArrayString");

                    b.HasKey("ISINNumber");

                    b.ToTable("Funds");
                });

            modelBuilder.Entity("PortfolioNetCore.Model.FundDetail", b =>
                {
                    b.Property<string>("ISINNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Currency");

                    b.Property<string>("Focus");

                    b.Property<string>("Management");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.Property<string>("Url");

                    b.HasKey("ISINNumber");

                    b.ToTable("FundDetails");
                });

            modelBuilder.Entity("PortfolioNetCore.Model.Management", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FundISINNumberString");

                    b.HasKey("Name");

                    b.ToTable("Managements");
                });
#pragma warning restore 612, 618
        }
    }
}
