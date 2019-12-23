
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppSignalR.Areas.Identity.Models;
using System.Linq;
using Newtonsoft.Json;

namespace WebAppSignalR.Hubs
{
    public class ChatHub : Hub
    {

        public class UserConnected
        {
            public string ConnectionId { get; set; }

            public string UserName { get; set; }
        }

        static List<UserConnected> CurrentConnections = new List<UserConnected>();

        //dipakai utk dapatkan userId menggunakan "serManager.GetUserId(Context.user)"
        //private UserManager<CustomIdentityUserModel> userManager;

        public async Task SendMessage(string user, string message, string userId)
        {
            //await Clients.All.SendAsync("ReceiveMessage", user, message);

            //get users connected
            //Clients.All.listUserConnected(Users.Values)            

            //await Clients.Client(userId).SendAsync("ReceiveMessage", user, message);

            //userId is Id in table AspNetUsers

            //var test = userManager.GetUserId(Context.User);

            await Clients.User(userId).SendAsync("ReceiveMessage", user, message);
        }

        public async Task GetAllConnectedUser()
        {
            var connectedUsers = JsonConvert.SerializeObject(CurrentConnections);
            await Clients.All.SendAsync("ReceiveConnectedUsers", connectedUsers);
        }

        public override async Task OnConnectedAsync()
        {            
            CurrentConnections.Add(new UserConnected { ConnectionId = Context.ConnectionId, UserName = Context.User.Identity.Name });
            
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");

            await Clients.All.SendAsync("ReceiveMessage", "");            

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var _connectedUser = new UserConnected { ConnectionId = Context.ConnectionId, UserName = Context.User.Identity.Name };

            if (CurrentConnections.Any(a => a.ConnectionId == Context.ConnectionId))
            {
                CurrentConnections.Remove(CurrentConnections.Select(a => a).Where(a => a.ConnectionId == Context.ConnectionId).FirstOrDefault());
            }


            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception); 
        }

    }
}
