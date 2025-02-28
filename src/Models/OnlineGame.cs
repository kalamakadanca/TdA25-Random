using TourDeApp.Models.Schemas;

namespace TourDeApp.Models;

public class OnlineGame : Game
{
    public string Player1Id { get; set; }
    public string Player2Id { get; set; }
    public string CurrentPlayerId { get; set; }
    public string FriendCode { get; set; }
    public event Action OnMove;
    public event Action OnWin;

    public OnlineGame(string name, DifficultyType difficulty, string? player1Id = null) : base(name, difficulty)
    {
        Player1Id = player1Id;
        CurrentPlayerId = Player1Id;
    }
    
    public override async Task UpdateBoard(Cell cell)
    {
        if (GameFinished || Board[cell.CellID[0]][cell.CellID[1]] != "") return;
        
        Board[cell.CellID[0]][cell.CellID[1]] = Next.ToString();
        
        History.Add(new Move(cell.CellID, Next));

        if (History.Count >= 5 && GameState == GameState.Opening) GameState = GameState.Midgame;

        if (Next == CellState.X)
        {
            Next = CellState.O;
            CurrentPlayerId = Player2Id;
        }
        else
        {
            Next = CellState.X;
            CurrentPlayerId = Player1Id;
        }

        OnMove.Invoke();
    }
}