using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.JsonModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models
{
    public class Game
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public DifficultyType Difficulty { get; set; }
        public GameState GameState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public BoardState BoardState { get; set; }
        public bool GameFinished { get; set; }
        public CellState? Winner { get; set; }
        public List<Move> History { get; set; }
        private CellState _next { get; set; } = CellState.Cross;

        public Game(string name, DifficultyType difficulty)
        {
            Uuid = Guid.NewGuid().ToString();
            Name = name;
            Difficulty = difficulty;
            GameState = GameState.Beginning;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
            BoardState = new BoardState();
            GameFinished = false;
            History = new List<Move>();
        }

        public void UpdateBoard(Cell cell)
        {
            if (GameFinished) return;
            BoardState.Board[cell.CellID[0], cell.CellID[1]].State = _next;

            // Record the move to history
            History.Add(new Move(cell.CellID, _next));

            if (History.Count > 5 && GameState == GameState.Beginning) GameState = GameState.Midgame;
            
            if (_next == CellState.Cross) _next = CellState.Circle;
            else _next = CellState.Cross;

        }

        public bool CheckWin()
        {
            if (GameFinished) return false;

            // Helper function to check a sequence of cells
            bool CheckLine(int startX, int startY, int stepX, int stepY)
            {
                int count = 1;
                CellState prevState = BoardState.Board[startX, startY].State;

                for (int i = 1; i < 5; i++)
                {
                    int x = startX + i * stepX, y = startY + i * stepY;
                    if (x < 0 || x >= BoardState.Size || y < 0 || y >= BoardState.Size) break;

                    var cellState = BoardState.Board[x, y].State;
                    if (cellState != CellState.Empty && cellState == prevState)
                    {
                        count++;
                        if (count == 5)
                        {
                            GameFinished = true;
                            Winner = cellState;
                            return true;
                        }
                        if (count == 4) // TODO: Check if win is actually possible
                        {
                            GameState = GameState.Endgame;
                        }
                    }
                    else
                    {
                        count = 1;
                        prevState = cellState;
                    }
                }

                return false;
            }

            // Check all directions
            for (int row = 0; row < BoardState.Size; row++)
            {
                for (int col = 0; col < BoardState.Size; col++)
                {
                    if (BoardState.Board[row, col].State != CellState.Empty)
                    {
                        if (CheckLine(row, col, 0, 1) || // Horizontal
                            CheckLine(row, col, 1, 0) || // Vertical
                            CheckLine(row, col, 1, 1) || // Diagonal top-left to bottom-right
                            CheckLine(row, col, 1, -1)) // Diagonal top-right to bottom-left
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}