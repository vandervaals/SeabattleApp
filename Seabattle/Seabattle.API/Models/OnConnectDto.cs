using Seabattle.Logic.Models;
using System.Collections.Generic;

namespace Seabattle.API.Models
{
    public class OnConnectDto
    {
        public string ConnectionId { get; set; }
        public List<OnlineUser> Users { get; set; }
    }
}