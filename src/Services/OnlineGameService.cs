using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using TourDeApp.Models;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Services;

public class OnlineGameService
{
    private List<OnlineGame> _games = new();

    public OnlineGame CreateGame( string uuid, string player1Id)
    {
        var game = new OnlineGame("Online hra", DifficultyType.Beginner, player1Id)
        {
            Uuid = uuid
        };
        
        _games.Add(game);

        return game;
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
        }

        return null;
    }

    public OnlineGame GetGame(string uuid)
    {
        return _games.Find(p => p.Uuid == uuid) ?? throw new InvalidOperationException();
    }

    public void UpdateBoard(Cell cell, string uuid)
    {
        var game = _games.Find(p => p.Uuid == uuid);

        game?.UpdateBoard(cell);
        
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