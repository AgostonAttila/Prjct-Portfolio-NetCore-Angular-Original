using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using PortfolioNetCore.Core.Model;

namespace PortfolioNetCore.Persistence
{
    public class PortfolioContext : DbContext
    {
        public DbSet<Fund> Funds { get; set; }
        public DbSet<FundDetail> FundDetails { get; set; }
        public DbSet<Management> Managements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=portfolio.db");
        }

       // public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }
    }

    //public VegaDbContext(DbContextOptions<VegaDbContext> options)
    //    : base(options)
    //{
    //}

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
    //      new { vf.VehicleId, vf.FeatureId });
    //}

}


