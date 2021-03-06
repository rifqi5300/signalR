﻿
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebAppSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string userId)
        {
            //await Clients.All.SendAsync("ReceiveMessage", user, message);

            //get users connected
            //Clients.All.listUserConnected(Users.Values)            

            //await Clients.Client(userId).SendAsync("ReceiveMessage", user, message);

            //userId is Id in table AspNetUsers

            await Clients.User(userId).SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var test = Context.User;
            
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var test = Context.User;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception); 
        }

    }
}
