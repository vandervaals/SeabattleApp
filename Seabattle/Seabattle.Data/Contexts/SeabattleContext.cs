using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Seabattle.Data.Contexts
{
    public sealed class SeabattleContext : IdentityDbContext
    {
        public SeabattleContext(): base("SeabattleContext")
        { }
    }
}
