using System.Data.Entity;

namespace Seabattle.Data.Contexts
{
    public class Initializer : MigrateDatabaseToLatestVersion<SeabattleContext, Configuration>
    {
    }
}
