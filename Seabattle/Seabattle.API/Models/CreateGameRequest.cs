using Seabattle.Logic.Models;
using System.Collections.Generic;

namespace Seabattle.API.Models
{
    public class CreateGameRequest
    {
        public string ConnectionId { get; set; }
        public List<ShipDto> Ships { get; set; }
    }
}