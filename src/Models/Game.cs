using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using TourDeApp.Infrastructure;
using TourDeApp.Infrastructure.CustomConverters;
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
        public string[][] BoardState { get; set; }
        public bool GameFinished { get; set; }
        public CellState? Winner { get; set; }
        public List<Move> History { get; set; }
        private CellState _next { get; set; } = CellState.X;

        public Game(string name, DifficultyType difficulty)
        {
            Uuid = Guid.NewGuid().ToString();
            Name = name;
            Difficulty = difficulty;
            GameState = GameState.Beginning;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
            BoardState = new string[GlobalSettings.BoardLenght][];
            GameFinished = false;
            History = new List<Move>();
        }

        public void UpdateBoard(Cell cell)
        {
            if (GameFinished) return;
            BoardState[cell.CellID[0]][cell.CellID[1]] = _next.ToString() == "Empty" ? "" : _next.ToString();

            // Record the move to history
            History.Add(new Move(cell.CellID, _next));

            if (History.Count > 5 && GameState == GameState.Beginning) GameState = GameState.Midgame;
            
            if (_next == CellState.X) _next = CellState.O;
            else _next = CellState.X;

        }

        public bool CheckWin()
        {
            if (GameFinished) return false;

            // Helper function to check a sequence of cells
            bool CheckLine(int startX, int startY, int stepX, int stepY)
            {
                int count = 1;
                CellState prevState = CellStateConverter.ToEnum(BoardState[startX][startY]);

                for (int i = 1; i < 5; i++)
                {
                    int x = startX + i * stepX, y = startY + i * stepY;
                    if (x < 0 || x >= GlobalSettings.BoardLenght || y < 0 || y >= GlobalSettings.BoardLenght) break;

                    var cellState = CellStateConverter.ToEnum(BoardState[x][y]);
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
            for (int row = 0; row < GlobalSettings.BoardLenght; row++)
            {
                for (int col = 0; col < GlobalSettings.BoardLenght; col++)
                {
                    if (CellStateConverter.ToEnum(BoardState[row][col]) != CellState.Empty)
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