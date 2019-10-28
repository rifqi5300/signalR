
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebAppSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string connId)
        {
            await Clients.Client(connId).SendAsync("ReceiveMessage", user, message);
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
