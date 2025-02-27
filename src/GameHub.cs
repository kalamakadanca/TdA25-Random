using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourDeApp.Models;
using TourDeApp.Services;

namespace TourDeApp;

public class GameHub : Hub
{
    private OnlineGameService _gameService { get; set; }

    public GameHub(OnlineGameService gameService)
    {
        _gameService = gameService;
    }

    public async override Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine($"{Context.ConnectionId} has disconnected");
    }
    
    public async Task JoinGroup(string uuid)
    {
        var game = _gameService.JoinGame(Context.ConnectionId, uuid);

        if (game is not null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, uuid);
            await Clients.Group(uuid).SendAsync("PlayerJoined", uuid);
        }
    }

    public async Task CreateGame(string uuid)
    {
        _gameService.CreateGame(Context.ConnectionId);

        await Groups.AddToGroupAsync(Context.ConnectionId, uuid);
    }
    
    public async Task SendMove(string uuid, Models.Schemas.Cell cell)
    {
        await Clients.Group(uuid).SendAsync("SendMove", cell);
    }
}