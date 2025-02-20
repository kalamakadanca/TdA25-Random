using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace TourDeApp.Services;

public class SignalRService
{
    private readonly HubConnection _connection;


    public SignalRService(NavigationManager navigationManager)
    {

        string baseUrl = navigationManager.BaseUri; // e.g., "https://localhost:5001/"
        _connection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl}chathub") // Combine base URL with hub path
            .Build();

        _connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            // Handle received messages
            MessageReceived?.Invoke(this, (user, message));
        });
    }

    public event EventHandler<(string user, string message)> MessageReceived;

    public async Task StartAsync()
    {
        await _connection.StartAsync();
    }

    public async Task SendMessage(string user, string message)
    {
        await _connection.InvokeAsync("SendMessage", user, message);
    }

    public async Task StopAsync()
    {
        await _connection.StopAsync();
    }
}