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
        public bool GameFinished { get; set; }
        public CellState? Winner { get; set; }
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
        }

        public void UpdateBoard(Cell cell)
        {
            if (GameFinished) return;
            this.BoardState.Board[cell.CellID[0], cell.CellID[1]].State = _next;

            if (_next == CellState.Cross) _next = CellState.Circle;
            else _next = CellState.Cross;

            CheckWin(); // I will later probably implement this to get called when the BoardState.Board is changed
        }

        // Checks if there are 5 pieces next to each other
        private void CheckWin()
        {
            if (GameFinished) return; // Checks whether the game has been won

            // Horizontal win check
            for (int row = 0; row < BoardState.Size; row++)
            {
                int piecesInRow = 0;
                CellState previousState = CellState.Empty;

                for (int column = 0; column < BoardState.Size; column++)
                {
                    if (BoardState.Board[row, column].State != CellState.Empty &&
                        BoardState.Board[row, column].State == previousState)
                    {
                        piecesInRow++;
                    }
                    else if (BoardState.Board[row, column].State != CellState.Empty)
                    {
                        piecesInRow = 1;
                        previousState = BoardState.Board[row, column].State;
                    }
                    else
                    {
                        piecesInRow = 0;
                        previousState = CellState.Empty;
                    }

                    if (piecesInRow >= 5 && previousState != CellState.Empty)
                    {
                        Console.WriteLine("Game has been won horizontally");
                        GameFinished = true;
                        Winner = BoardState.Board[row, column].State;
                        return;
                    }
                }
            }

            // Vertical win check
            for (int row = 0; row < BoardState.Size; row++)
            {
                int piecesInRow = 0;
                CellState previousState = CellState.Empty;

                for (int column = 0; column < BoardState.Size; column++)
                {
                    if (BoardState.Board[column, row].State != CellState.Empty &&
                        BoardState.Board[column, row].State == previousState)
                    {
                        piecesInRow++;
                    }
                    else if (BoardState.Board[column, row].State != CellState.Empty)
                    {
                        piecesInRow = 1;
                        previousState = BoardState.Board[column, row].State;
                    }
                    else
                    {
                        piecesInRow = 0;
                        previousState = CellState.Empty;
                    }

                    if (piecesInRow >= 5 && previousState != CellState.Empty)
                    {
                        Console.WriteLine("Game has been won vertically");
                        GameFinished = true;
                        Winner = BoardState.Board[column, row].State;
                        return;
                    }
                }
            }

            // Diagonal win check (top-left to bottom-right)
            for (int row = 0;
                 row < BoardState.Size - 4;
                 row++) // Only check starting rows where a diagonal of 5 can fit
            {
                for (int column = 0;
                     column < BoardState.Size - 4;
                     column++) // Only check starting columns where a diagonal of 5 can fit
                {
                    int piecesInDiagonal = 0;
                    CellState previousState = CellState.Empty;

                    for (int i = 0; i < 5; i++) // Check a diagonal of length 5
                    {
                        if (BoardState.Board[row + i, column + i].State != CellState.Empty &&
                            BoardState.Board[row + i, column + i].State == previousState)
                        {
                            piecesInDiagonal++;
                        }
                        else if (BoardState.Board[row + i, column + i].State != CellState.Empty)
                        {
                            piecesInDiagonal = 1;
                            previousState = BoardState.Board[row + i, column + i].State;
                        }
                        else
                        {
                            piecesInDiagonal = 0;
                            previousState = CellState.Empty;
                        }

                        if (piecesInDiagonal >= 5 && previousState != CellState.Empty)
                        {
                            Console.WriteLine("Game has been won diagonally (top-left to bottom-right)");
                            GameFinished = true;
                            Winner = BoardState.Board[row + i, column + i].State;
                            return;
                        }
                    }
                }
            }

            // Diagonal win check (top-right to bottom-left)
            for (int row = 0;
                 row < BoardState.Size - 4;
                 row++) // Only check starting rows where a diagonal of 5 can fit
            {
                for (int column = 4;
                     column < BoardState.Size;
                     column++) // Only check starting columns where a diagonal of 5 can fit
                {
                    int piecesInDiagonal = 0;
                    CellState previousState = CellState.Empty;

                    for (int i = 0; i < 5; i++) // Check a diagonal of length 5
                    {
                        if (BoardState.Board[row + i, column - i].State != CellState.Empty &&
                            BoardState.Board[row + i, column - i].State == previousState)
                        {
                            piecesInDiagonal++;
                        }
                        else if (BoardState.Board[row + i, column - i].State != CellState.Empty)
                        {
                            piecesInDiagonal = 1;
                            previousState = BoardState.Board[row + i, column - i].State;
                        }
                        else
                        {
                            piecesInDiagonal = 0;
                            previousState = CellState.Empty;
                        }

                        if (piecesInDiagonal >= 5 && previousState != CellState.Empty)
                        {
                            Console.WriteLine("Game has been won diagonally (top-right to bottom-left)");
                            GameFinished = true;
                            Winner = BoardState.Board[row + i, column - i].State;
                            return;
                        }
                    }
                }
            }
        }
    }
}