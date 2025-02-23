using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TourDeApp.Services;

namespace TourDeApp.Components.Pages.Online;

public partial class OnlineGame : ComponentBase
{
    [Inject] private OnlineGameService _gameService { get; set; }
    [Inject] private SignalRService _signalRService { get; set; }
    [Inject] private NavigationManager _navigationManager { get; set; }

    
    private Models.OnlineGame _game { get; set; }

    [Parameter] public string Uuid { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        _signalRService.StartAsync();

        if (_navigationManager.Uri.Contains("/online/"))
        {        
            string currentUrl = _navigationManager.Uri;
            var index = currentUrl.IndexOf("/online/") + "/online/".Length;
            Uuid = currentUrl.Substring(index);
            _game = _gameService.JoinGame(_signalRService.HubConnection.ConnectionId, Uuid);
        }
        else
        {
            _game = _gameService.CreateGame(Uuid, _signalRService.HubConnection.ConnectionId);
            _navigationManager.NavigateTo($"/online/{_game.Uuid}");
        }
        
        await InvokeAsync(StateHasChanged);

        _signalRService.HubConnection.On<Models.Schemas.Cell>("SendMove", (cell) =>
        {
            _gameService.UpdateBoard(cell, _game.Uuid);
        });
        
        
    }

    private void OnMove()
    {
        
    }
}