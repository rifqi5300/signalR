
using Microsoft.AspNetCore.SignalR;
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
    }
}
