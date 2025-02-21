using Microsoft.AspNetCore.SignalR;

namespace TourDeApp;

public class GameHub : Hub
{
    public async Task SendMove(Models.Schemas.Cell cell)
    {
        await Clients.All.SendAsync("SendMove", cell);
    }
}