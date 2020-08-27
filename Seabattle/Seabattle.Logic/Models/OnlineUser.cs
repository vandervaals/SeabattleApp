using System;

namespace Seabattle.Logic.Models
{
    public class OnlineUser
    {
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
        public bool IsBusy { get; set; }
    }
}
