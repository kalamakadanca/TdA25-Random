using Microsoft.AspNetCore.SignalR;
using TourDeApp.Models;

namespace TourDeApp
{
    public class GameHub : Hub
    {
        public async Task UpdateGame(Models.Game game)
        {
            await Clients.All.SendAsync("ReceiveMessage", game);
        }
    }
}
