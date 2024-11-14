using System.Runtime.CompilerServices;
using TourDeApp.Models.DataBaseModels;
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
        
        private CellState next { get; set; } = CellState.Cross;
        
        public Game(string name, DifficultyType difficulty)
        {
            Uuid = Guid.NewGuid().ToString();
            Name = name;
            Difficulty = difficulty;
            GameState = GameState.Beginning;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
            BoardState = new BoardState();
        }
        
        public void UpdateBoard(Cell cell)
        {
            this.BoardState.Board[cell.CellID[0], cell.CellID[1]].State = next;

            if (next == CellState.Cross) next = CellState.Circle;
            else next = CellState.Cross;
            
            CheckWin();
        }

        // TODO: Make it work vertically, diagonally and improve, make more readable
        public void CheckWin()
        {
            // Checks if there are 5 pieces next to each other horizontally
            for (int row = 0; row < BoardState.Size; row++)
            {
                int pieces_in_row = 0;
                CellState lastState = CellState.Empty;

                for (int column = 0; column < BoardState.Size; column++)
                {
                    if (BoardState.Board[row, column].State != CellState.Empty &&
                        BoardState.Board[row, column].State == lastState)
                    {
                        pieces_in_row++;
                    }
                    else if (BoardState.Board[row, column].State != CellState.Empty)
                    {
                        pieces_in_row = 1; // reset count but account for the current piece
                        lastState = BoardState.Board[row, column].State;
                    }
                    else
                    {
                        pieces_in_row = 0;
                        lastState = CellState.Empty;
                    }

                    if (pieces_in_row >= 5 && lastState != CellState.Empty)
                    {
                        Console.WriteLine($"{lastState.ToString()} has won the game!");
                        // Consider adding logic here to handle the win scenario
                        break;
                    }
                }
            }
        }

        // TODO: Create
        public void Win()
        {
            
        }
    }
}
