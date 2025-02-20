using Microsoft.AspNetCore.SignalR;

namespace TourDeApp
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} has connected");
            await Clients.All.SendAsync($"{Context.ConnectionId} has connected");
        }
    }
}
