using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Services;

public class SignalRService
{
    public HubConnection HubConnection { get; set; }
    private NavigationManager _navigationManager { get; set; }

    public SignalRService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public async Task StartAsync()
    {
        HubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/gamehub"))
            .Build();

        await HubConnection.StartAsync();
    }

    public async Task JoinGroup(string uuid)
    {
        await HubConnection.SendAsync("JoinGroup", uuid);
    }

    public async Task SendMove(string uuid, Models.Schemas.Cell cell)
    {
        await HubConnection.SendAsync("SendMove", uuid, cell);
    }
}