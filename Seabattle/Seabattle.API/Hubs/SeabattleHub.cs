using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Seabattle.API.Models;
using Seabattle.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seabattle.API.Hubs
{
    public interface ISeabattleClient
    {
        Task OnConnected(OnConnectDto result);

        Task OnNewUserConnected(OnlineUser user);

        Task OnUserDisconnected(OnlineUser user);
    }

    [HubName("Hub")]
    public class SeabattleHub: Hub<ISeabattleClient>
    {
        static List<OnlineUser> Users = new List<OnlineUser>();

        // Подключение нового пользователя
        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.ConnectionId == id))
            {
                var user = new OnlineUser
                {
                    UserName = userName,
                    ConnectionId = id,
                    IsBusy = false
                };

                Users.Add(user);

                // Посылаем сообщение текущему пользователю
                await Clients.Caller.OnConnected(new OnConnectDto { Users = Users, ConnectionId = id });

                // Посылаем сообщение всем пользователям, кроме текущего
                await Clients.AllExcept(id).OnNewUserConnected(user);
            }
        }

        // Отключение пользователя
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.OnUserDisconnected(item);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}