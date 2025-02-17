using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TourDeApp.Components.Services;
using TourDeApp.Controllers.API_V1.Games;
using TourDeApp.Models;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Inject] private GamesController _gamesController { get; set; } = default!;

    private Random random;
    private Modal? ModalRef;
    private Modal multiplayerSelectModal;
    private UserModel UserModel { get; set; } = new();
    public bool meh = true;
    private Models.Game defaultGame = new Models.Game("Default game", DifficultyType.Beginner);
    private bool gamesLoading { get; set; }
    private bool playWithFriend { get; set; } = false;
    private string? friendCode { get; set; }
    private bool waitingForFriend { get; set; } = false;
    private List<Models.Game>? _games;
    public List<Models.Game>? Games
    {
        get
        {
            return _games;
        }
        set
        {
            _games = value;
            StateHasChanged();
        }
    }

    private void OpenModal() => ModalRef?.Open();
    private void HandleValidSubmit()
    {
        ModalRef?.Close();
    }
    private void PlayLocally()
    {
        _navigationManager.NavigateTo("/game", true);
    }

    private void PlayOnline()
    {
        multiplayerSelectModal.Open();
    }

    private async Task PlayQuickGame()
    {
        // TODO: Player will be added to matchmaking queue
    }

    private void PlayWithFriend()
    {
        playWithFriend = true;
        StateHasChanged();
    }

    private async Task JoinFriend()
    {
        // TODO
    }

    private string GenerateFriendCode() => random.Next(100000, 999999).ToString();
    
    private IList<Todoitem> Todos { get; set; } = new List<Todoitem>();

    protected override void OnInitialized()
    {
        gamesLoading = true;
        random = new();
        // Start a background task to load games without blocking the UI
        _ = LoadGamesAsync();
    }

    private async Task LoadGamesAsync()
    {
        Games = await GetGames();
        gamesLoading = false;
        await InvokeAsync(StateHasChanged); // Notify Blazor to update the UI
    }

    public string ItemClass(Todoitem item)
    {
        return item.Completed ? "item-completed" : "";
    }

    private async Task<List<Models.Game>> GetGames()
    {
        var result = await _gamesController.Get();

        if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
        {
            if (objectResult.Value is List<Models.Game> games)
            {
                return games;
            }
        }

        return new List<Models.Game>() { defaultGame };
    }

    private void OpenGame(string uuid)
    {
        _navigationManager.NavigateTo($"/game/{uuid}");
    }
}