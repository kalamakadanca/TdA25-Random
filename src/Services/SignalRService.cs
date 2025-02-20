using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TourDeApp.Components.Pages;
using TourDeApp.Components.Services;

namespace TourDeApp.Services;

public class SignalRService
{
    private readonly HubConnection _connection;
    private readonly GameService _gameService;

    public SignalRService(NavigationManager navigationManager, GameService gameService)
    {
        _gameService = gameService;

        string baseUrl = navigationManager.BaseUri; // e.g., "https://localhost:5001/"
        _connection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl}gamehub") // Combine base URL with hub path
            .Build();

        _connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            // Handle received messages
            MessageReceived?.Invoke(this, (user, message));
        });

        _connection.On<Models.Game>("UpdateGame", game =>
        {
            GameUpdated?.Invoke(this, game);
        });
    }

    public event EventHandler<(string user, string message)> MessageReceived;
    public event EventHandler<Models.Game> GameUpdated;

    public async Task StartAsync()
    {
        await _connection.StartAsync();
    }

    public async Task SendMessage(string user, string message)
    {
        await _connection.InvokeAsync("SendMessage", user, message);
    }

    public async Task UpdateGame(Models.Game game)
    {
        await _connection.InvokeAsync("UpdateGame", _gameService.GetCurrentGame());
    }

    public async Task StopAsync()
    {
        await _connection.StopAsync();
    }
}