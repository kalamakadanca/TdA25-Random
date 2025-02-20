using Microsoft.AspNetCore.SignalR;
using TourDeApp.Models;

namespace TourDeApp
{
    public class GameHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} has connected to GameHub");
        }

        public async Task UpdateGame(Game game)
        {
            await Clients.All.SendAsync("ReceiveMessage", game);
        }
    }
}
