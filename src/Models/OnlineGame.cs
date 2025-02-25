using TourDeApp.Models.Schemas;

namespace TourDeApp.Models;

public class OnlineGame : Game
{
    public string Player1Id { get; set; }
    public string Player2Id { get; set; }
    public string CurrentPlayerId { get; set; }
    public event Action OnMove;
    public event Action OnWin;

    public OnlineGame(string name, DifficultyType difficulty, string player1Id) : base(name, difficulty)
    {
        Player1Id = player1Id;
        CurrentPlayerId = Player1Id;
    }
    
    public override async Task UpdateBoard(Cell cell)
    {
        if (GameFinished) return;
        Board[cell.CellID[0]][cell.CellID[1]] = Next.ToString() == "Empty" ? "" : Next.ToString();

        // Record the move to history
        History.Add(new Move(cell.CellID, Next));

        if (History.Count >= 5 && GameState == GameState.Opening) GameState = GameState.Midgame;

        Next = Next == CellState.X ? CellState.O : CellState.X;
        
        Console.WriteLine(Next.ToString());
        
        OnMove.Invoke();
    }
}