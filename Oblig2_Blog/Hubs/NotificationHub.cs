using Microsoft.AspNetCore.SignalR;

namespace Oblig2_Blog.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string msg)
        {
            await Clients.All.SendAsync("ReceiveMsg", msg);
        }
    }
}
