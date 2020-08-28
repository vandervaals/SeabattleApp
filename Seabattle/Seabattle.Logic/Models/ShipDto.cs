using System.Collections.Generic;

namespace Seabattle.Logic.Models
{
    public class ShipDto
    {
        public int Id { get; set; }
        public List<CellDto> Cells { get; set; }
        public bool? IsHorizontal { get; set; }
    }
}
