using Seabattle.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace Seabattle.Data.Contexts
{
    internal class GameDbConfiguration: EntityTypeConfiguration<GameDb>
    {
        public GameDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("Games");
            HasMany(x => x.ComputerShots);
        }
    }

    internal class AreaDbConfiguration : EntityTypeConfiguration<AreaDb>
    {
        public AreaDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("Areas");
            HasMany(x => x.Ships);
        }
    }

    internal class ShipDbConfiguration : EntityTypeConfiguration<ShipDb>
    {
        public ShipDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("Ships");
            HasMany(x => x.Cells);
        }
    }
}
