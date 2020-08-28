using System.Collections.Generic;

namespace Seabattle.Logic.Models
{
    public class GameDto
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public AreaDto UserArea { get; set; }
        public AreaDto ComputerArea { get; set; }
        public List<ShotDto> ComputerShots { get; set; }
    }
}
