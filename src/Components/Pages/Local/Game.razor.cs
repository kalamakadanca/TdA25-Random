using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using TourDeApp.Services;
using TourDeApp.Controllers.API_V1.Games;
using TourDeApp.Models;
using TourDeApp.Models.Schemas;
using Cell = TourDeApp.Components.Pages.Shared.Cell;

namespace TourDeApp.Components.Pages.Local;

public partial class Game : ComponentBase
{
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Inject] private GameService _gameService { get; set; } = default!;
    [Inject] private GamesController _gamesController { get; set; } = default!;
    [Inject] private SignalRService _signalRService { get; set; }
    
    private HubConnection _hubConnection { get; set; }

    
    private Models.Game? _game { get; set; }
    public bool hr_ed = true;
    private string? _saveName { get; set; }
    private DifficultyType _saveDifficulty { get; set; }
    private Modal? ModalRef;
    private Modal WinningDialog;
    private UserModel UserModel { get; set; } = new();
    private Models.Game defaultGame = new Models.Game("Default game", DifficultyType.Beginner);
    private bool gamesLoaded = false;

    private void NewGame()
    {
        if (_navigationManager.Uri.Contains("/game/"))
        {
            _navigationManager.NavigateTo("/game", true);
        }
        else
        {
            _game = _gameService.CreateGame();
            StateHasChanged();
        }
    }

    [Parameter] public string? Uuid { get; set; }

    public void ChangeDifficulty(DifficultyType difficulty)
    {
        _saveDifficulty = difficulty;
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        _gameService.SubscribeMove(ReRender);
        _gameService.SubscribeWin(GameWon);
    }

    private void ReRender()
    {
        InvokeAsync(StateHasChanged);
    }
    
    protected override async Task OnInitializedAsync()
    {
        string currentUrl = _navigationManager.Uri;
        if (currentUrl.Contains("/game/"))
        {
            var index = currentUrl.IndexOf("/game/") + "/game/".Length;
            Uuid = currentUrl.Substring(index);
            _game = await LoadGame(Uuid);
        }
        else
        {
            _signalRService.StartAsync();

            _signalRService.HubConnection.On<Models.Schemas.Cell>("SendMove", (cell) =>
            {
                _gameService.UpdateBoard(cell);
            });
            
            _game = _gameService.GameExists() ? _gameService.CreateGame() : _gameService.GetGame();
        }
    }

    private async Task SaveGame()
    {
        if (_game == null || _saveName == null || _saveDifficulty == null) return;
        if (!string.IsNullOrEmpty(_saveName)) _game.Name = _saveName;
        if (_saveDifficulty != default) _game.Difficulty = _saveDifficulty;
        var request = new GameCreateUpdateRequest(new Models.Game
        {
            Name = _saveName,
            Difficulty = _saveDifficulty,
            Board = _game.Board
        });
        var result = await _gamesController.Post(request);

        if (result is ObjectResult objectResult && objectResult.StatusCode == 201)
        {
            if (objectResult.Value is Models.Game game)
            {
                Uuid = game.Uuid;
                _navigationManager.NavigateTo($"/game/{Uuid}");
            }
        }
        else
        {
            Console.WriteLine("Hra se NEuložila :(");
        }
    }

    private async Task<Models.Game> LoadGame(string uuid)
    {
        if (uuid == null) return defaultGame;

        var result = await _gamesController.Get(uuid);

        if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
        {
            if (objectResult.Value is TourDeApp.Models.Game game)
            {
                return game;
            }
            return defaultGame;
        }

        Console.WriteLine("There has been an error");
        return defaultGame;
    }


    private async Task HandleClick()
    {
        OpenModal();
        await SaveGame();
    }

    private void OpenModal() => ModalRef?.Open();
    private void HandleValidSubmit()
    {
        ModalRef?.Close();
    }

    private void GameWon()
    {
        WinningDialog.Open();
    }
    
    public void Dispose()
    {
        _gameService.UnsubscribeMove(StateHasChanged);
        _gameService.UnsubscribeWin(GameWon);
    }
}