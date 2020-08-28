using System.Collections.Generic;

namespace Seabattle.Data.Models
{
    public class AreaDb
    {
        public int Id { get; set; }
        public ICollection<ShipDb> Ships { get; set; }
    }
}
