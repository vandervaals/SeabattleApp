using System.Collections.Generic;

namespace Seabattle.Data.Models
{
    public class GameDb
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public AreaDb UserArea { get; set; }
        public AreaDb ComputerArea { get; set; }
        public List<ShotDb> ComputerShots { get; set; }
    }
}
