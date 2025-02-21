using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

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
}