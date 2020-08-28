namespace Seabattle.Data.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Seabattle.Data.Contexts.SeabattleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Seabattle.Data.Contexts.SeabattleContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //if (context.Roles.Any()) return;

            //context.Roles.Add(new IdentityRole("user"));
            //context.SaveChanges();
        }
    }
}
