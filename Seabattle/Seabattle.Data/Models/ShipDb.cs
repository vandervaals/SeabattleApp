using System.Collections.Generic;

namespace Seabattle.Data.Models
{
    public class ShipDb
    {
        public int Id { get; set; }
        public ICollection<CellDb> Cells { get; set; }
    }
}
