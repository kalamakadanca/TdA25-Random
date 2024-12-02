using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using TourDeApp.Infrastructure;
using TourDeApp.Infrastructure.CustomConverters;
using TourDeApp.Infrastructure.ExtensionMethods;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.JsonModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models
{
    public class Game
    {
        [Required]
        public string Uuid { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonConverter(typeof(LowercaseEnumConverter<DifficultyType>))]
        public DifficultyType Difficulty { get; set; }
        [JsonConverter(typeof(LowercaseEnumConverter<GameState>))]
        public GameState GameState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string[][] Board { get; set; }

        [JsonIgnore]
        public bool GameFinished { get; set; }
        [JsonIgnore]
        public CellState? Winner { get; set; }
        [JsonIgnore]
        public List<Move> History { get; set; }
        [JsonIgnore]
        public CellState Next { get; set; } = CellState.X;

        public Game(string name, DifficultyType difficulty)
        {
            Uuid = Guid.NewGuid().ToString();
            Name = name;
            Difficulty = difficulty;
            GameState = GameState.Opening;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
            this.GameGenerateEmptyBoard(); // Creates an empty board
            GameFinished = false;
            History = new List<Move>();
            CheckWin();
        }
        
        public void UpdateBoard(Cell cell)
        {
            if (GameFinished) return;
            Board[cell.CellID[0]][cell.CellID[1]] = Next.ToString() == "Empty" ? "" : Next.ToString();

            // Record the move to history
            History.Add(new Move(cell.CellID, Next));

            if (History.Count >= 5 && GameState == GameState.Opening) GameState = GameState.Midgame;

            if (Next == CellState.X) Next = CellState.O;
            else Next = CellState.X;
        }

        public bool CheckWin()
        {
            GameState = Board.Sum(row => row.Count(field => field != "")) <= 10 ? GameState.Opening : GameState.Midgame;
             
            const int eForWon = 5;
            const int maxForDiagonal = eForWon - 1;
            int lenght = Board.Length;
            int countX = Board.Sum(row => row.Count(field => field == "X"));
            int countO = Board.Sum(row => row.Count(field => field == "O"));
            
            Next = countX == countO ? CellState.X : CellState.O;
            
            for (int i = 0; i < lenght-maxForDiagonal; i++)
            {
                for (int j = 0; j < lenght-maxForDiagonal; j++)
                {
                    // Check for winning 
                    if (Board[i][j] != "" &&
                        Board[i][j] == Board[i + 1][j + 1] &&
                        Board[i][j] == Board[i + 2][j + 2] &&
                        Board[i][j] == Board[i + 3][j + 3] &&
                        Board[i][j] == Board[i + 4][j + 4]) {
                        GameState = GameState.Endgame;
                        Winner = Helper.StrToCellState(Board[i][j]);
                        return true;
                    }
                    if (Board[i][j] == "" &&
                        Board[i][j + 1] != "" &&
                        Board[i][j + 1] == Board[i + 2][j + 2] &&
                        Board[i][j + 1] == Board[i + 3][j + 3] &&
                        Board[i][j + 1] == Board[i + 4][j + 4]) {
                        if (Helper.StrToCellState(Board[i][j + 1]) == Next) GameState = GameState.Endgame;
                    } 
                    else if (Board[i + 4][j + 4] == "" &&
                             Board[i][j] != "" &&
                             Board[i][j] == Board[i][j + 1] &&
                             Board[i][j] == Board[i][j + 2] &&
                             Board[i][j] == Board[i][j + 3]) {
                        if (Helper.StrToCellState(Board[i][j]) == Next) GameState = GameState.Endgame;
                    }
                }
            }
            
            // Checks rows
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght-maxForDiagonal; j++)
                {
                    if (Board[i][j] != "" &&
                        Board[i][j] == Board[i][j + 1] &&
                        Board[i][j] == Board[i][j + 2] &&
                        Board[i][j] == Board[i][j + 3] &&
                        Board[i][j] == Board[i][j + 4]) {
                        GameState = GameState.Endgame;
                        Winner = Helper.StrToCellState(Board[i][j]);
                        return true;
                    }
                    if (Board[i][j] == "" &&
                        Board[i][j + 1] != "" &&
                        Board[i][j + 1] == Board[i][j + 2] &&
                        Board[i][j + 1] == Board[i][j + 3] &&
                        Board[i][j + 1] == Board[i][j + 4] &&
                        Helper.StrToCellState(Board[i][j + 1]) == Next) {
                        GameState = GameState.Endgame;
                    }
                    else if (Board[i][j + 4] == "" &&
                        Board[i][j] != "" &&
                        Board[i][j] == Board[i][j + 1] &&
                        Board[i][j] == Board[i][j + 2] &&
                        Board[i][j] == Board[i][j + 3] &&
                        Helper.StrToCellState(Board[i][j]) == Next) {
                        GameState = GameState.Endgame;
                    }
                }
            }

            // Checks columns
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght-maxForDiagonal; j++)
                {
                    if (Board[i][j] != "" &&
                        Board[i][j] == Board[i + 1][j] &&
                        Board[i][j] == Board[i + 2][j] &&
                        Board[i][j] == Board[i + 3][j] &&
                        Board[i][j] == Board[i + 4][j]) {
                        GameState = GameState.Endgame;
                        Winner = Helper.StrToCellState(Board[i][j]);
                        return true;
                    }
                    if (Board[i][j] == "" &&
                        Board[i + 1][j] != "" &&
                        Board[i + 1][j] == Board[i + 2][j] &&
                        Board[i + 1][j] == Board[i + 3][j] &&
                        Board[i + 1][j] == Board[i + 4][j] &&
                        Helper.StrToCellState(Board[i + 1][j]) == Next) {
                        GameState = GameState.Endgame;
                    }
                    else if (Board[i][j + 4] == "" &&
                        Board[i][j] != "" &&
                        Board[i][j] == Board[i + 1][j] &&
                        Board[i][j] == Board[i + 2][j] &&
                        Board[i][j] == Board[i + 3][j] &&
                        Helper.StrToCellState(Board[i][j]) == Next) {
                        GameState = GameState.Endgame;
                    }
                }
            }

            // Check diagonal '\'
            for (int i = 0; i < lenght-maxForDiagonal; i++)
            {
                for (int j = 0; j < lenght-maxForDiagonal; j++)
                {
                    if (Board[i][j] != "" &&
                        Board[i][j] == Board[i + 1][j + 1] &&
                        Board[i][j] == Board[i + 2][j + 2] &&
                        Board[i][j] == Board[i + 3][j + 3] &&
                        Board[i][j] == Board[i + 4][j + 4]) {
                        GameState = GameState.Endgame;
                        Winner = Helper.StrToCellState(Board[i][j]);
                        return true;
                    }
                    if (Board[i][j] == "" &&
                        Board[i + 1][j + 1] != "" &&
                        Board[i + 1][j + 1] == Board[i + 2][j + 2] &&
                        Board[i + 1][j + 1] == Board[i + 3][j + 3] &&
                        Board[i + 1][j + 1] == Board[i + 4][j + 4] &&
                        Helper.StrToCellState(Board[i + 1][j + 1]) == Next) {
                        GameState = GameState.Endgame;
                    }
                    else if (Board[i + 4][j + 4] == "" &&
                        Board[i][j] != "" &&
                        Board[i][j] == Board[i + 1][j + 1] &&
                        Board[i][j] == Board[i + 2][j + 2] &&
                        Board[i][j] == Board[i + 3][j + 3] &&
                        Helper.StrToCellState(Board[i][j]) == Next) {
                        GameState = GameState.Endgame;
                    }
                }
            }

            // Check diagonal '/'
            for (int i = 0; i < lenght-maxForDiagonal; i++)
            {
                for (int j = lenght-1; j >= maxForDiagonal; j--)
                {
                    if (Board[i][j] != "" &&
                        Board[i][j] == Board[i + 1][j - 1] &&
                        Board[i][j] == Board[i + 2][j - 2] &&
                        Board[i][j] == Board[i + 3][j - 3] &&
                        Board[i][j] == Board[i + 4][j - 4]) {
                        GameState = GameState.Endgame;
                        Winner = Helper.StrToCellState(Board[i][j]);
                        return true;
                    }
                    if (Board[i][j] == "" &&
                        Board[i + 1][j - 1] != "" &&
                        Board[i + 1][j - 1] == Board[i + 2][j - 2] &&
                        Board[i + 1][j - 1] == Board[i + 3][j - 3] &&
                        Board[i + 1][j - 1] == Board[i + 4][j - 4] &&
                        Helper.StrToCellState(Board[i + 1][j - 1]) == Next) {
                        GameState = GameState.Endgame;
                    }
                    else if (Board[i + 4][j - 4] == "" &&
                        Board[i][j] != "" &&
                        Board[i][j] == Board[i + 1][j - 1] &&
                        Board[i][j] == Board[i + 2][j - 2] &&
                        Board[i][j] == Board[i + 3][j - 3] &&
                        Helper.StrToCellState(Board[i][j]) == Next) {
                        GameState = GameState.Endgame;
                    }
                }
            }

            return false;
        }
    }
}