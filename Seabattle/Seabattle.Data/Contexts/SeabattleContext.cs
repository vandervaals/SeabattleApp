using Microsoft.AspNet.Identity.EntityFramework;
using Seabattle.Data.Migrations;
using Seabattle.Data.Models;
using System.Data.Entity;

namespace Seabattle.Data.Contexts
{
    public sealed class SeabattleContext : IdentityDbContext
    {
        public SeabattleContext(): base("SeabattleContext")
        {
            Database.SetInitializer<SeabattleContext>(new MigrateDatabaseToLatestVersion<SeabattleContext, Configuration>());
        }

        public DbSet<GameDb> Games { get; set; }
        public DbSet<AreaDb> Areas { get; set; }
        public DbSet<ShipDb> Ships { get; set; }
        public DbSet<CellDb> Cells { get; set; }
        public DbSet<ShotDb> Shots { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(typeof(SeabattleContext).Assembly);
        }
    }
}
