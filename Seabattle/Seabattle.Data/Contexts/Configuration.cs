using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Seabattle.Data.Contexts
{
    public class Configuration : DbMigrationsConfiguration<SeabattleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(SeabattleContext context)
        {
            if (context.Roles.Any()) return;

            context.Roles.Add(new IdentityRole("user"));
            context.SaveChanges();
        }
    }
}
