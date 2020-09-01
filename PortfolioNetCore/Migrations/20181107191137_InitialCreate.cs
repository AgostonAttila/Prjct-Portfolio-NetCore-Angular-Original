﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioNetCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FundDetails",
                columns: table => new
                {
                    ISINNumber = table.Column<string>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Management = table.Column<string>(nullable: true),
                    Focus = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundDetails", x => x.ISINNumber);
                });

            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    ISINNumber = table.Column<string>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Management = table.Column<string>(nullable: true),
                    Focus = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    PerformanceYTD = table.Column<string>(nullable: true),
                    Performance1Year = table.Column<string>(nullable: true),
                    Performance3Year = table.Column<string>(nullable: true),
                    Performance5Year = table.Column<string>(nullable: true),
                    PerformanceFromBeggining = table.Column<string>(nullable: true),
                    PerformanceActualMinus9 = table.Column<string>(nullable: true),
                    PerformanceActualMinus8 = table.Column<string>(nullable: true),
                    PerformanceActualMinus7 = table.Column<string>(nullable: true),
                    PerformanceActualMinus6 = table.Column<string>(nullable: true),
                    PerformanceActualMinus5 = table.Column<string>(nullable: true),
                    PerformanceActualMinus4 = table.Column<string>(nullable: true),
                    PerformanceActualMinus3 = table.Column<string>(nullable: true),
                    PerformanceActualMinus2 = table.Column<string>(nullable: true),
                    PerformanceActualMinus1 = table.Column<string>(nullable: true),
                    PerformanceAverage = table.Column<string>(nullable: true),
                    VolatilityArrayString = table.Column<string>(nullable: true),
                    SharpRateArrayString = table.Column<string>(nullable: true),
                    BestMonthArrayString = table.Column<string>(nullable: true),
                    WorstMonthArrayString = table.Column<string>(nullable: true),
                    MaxLossArrayString = table.Column<string>(nullable: true),
                    OverFulFilmentArrayString = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.ISINNumber);
                });

            migrationBuilder.CreateTable(
                name: "Managements",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    FundISINNumberString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managements", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundDetails");

            migrationBuilder.DropTable(
                name: "Funds");

            migrationBuilder.DropTable(
                name: "Managements");
        }
    }
}