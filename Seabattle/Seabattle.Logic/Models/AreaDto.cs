using System.Collections.Generic;

namespace Seabattle.Logic.Models
{
    public class AreaDto
    {
        public int Id { get; set; }
        public List<ShipDto> Ships { get; set; }
    }
}
