using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using TourDeApp.Models;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Services;

public class OnlineGameService
{
    private List<OnlineGame> _games = new();

    public OnlineGame CreateGame(string player1Id)
    {
        var game = new OnlineGame("Online hra", DifficultyType.Beginner, player1Id) { Player1Id = player1Id};
        
        _games.Add(game);

        return game;
    }

    public string CreateGameWithCode()
    {
        Random random = new Random();
        int code = random.Next(10000, 100000);

        if (!_games.Any(p => p.FriendCode == code.ToString()))
        {
            var game = new OnlineGame("Hra s kamarádem", DifficultyType.Beginner) {FriendCode = code.ToString()};
            return game.Uuid;
        }

        return null;
    }

    public OnlineGame JoinGame(string player2Id, string uuid)
    {
        var game = _games.Find(p => p.Uuid == uuid);

        if (game is not null)
        {
            if (game.Player1Id != player2Id)
            {
                game.Player2Id = player2Id;
                return game;
            }
            else if (game.Player1Id is null)
            {
                game.Player1Id = player2Id;
            }
        }

        return null;
    }

    public OnlineGame GetGame(string uuid)
    {
        return _games.Find(p => p.Uuid == uuid) ?? throw new InvalidOperationException();
    }

    public OnlineGame UpdateBoard(Cell cell, string uuid)
    {
        var game = _games.Find(p => p.Uuid == uuid);

        game?.UpdateBoard(cell);

        return game;
    }

    public void Subscribe(string uuid, Action onMove, Action onWin)
    {
        var game = _games.Find(p => p.Uuid == uuid);

        if (game is not null)
        {
            game.OnMove += onMove;
            game.OnWin += onWin;
        }
    }
}