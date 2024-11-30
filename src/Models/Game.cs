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

            if (History.Count > 5 && GameState == GameState.Opening) GameState = GameState.Midgame;

            if (Next == CellState.X) Next = CellState.O;
            else Next = CellState.X;
        }

        public bool CheckWin()
        {
            if (GameFinished) return false;

            bool CheckLine(int startX, int startY, int stepX, int stepY)
            {
                int count = 1;
                CellState prevState = CellStateConverter.ToEnum(Board[startX][startY]);

                for (int i = 1; i < 5; i++)
                {
                    int x = startX + i * stepX;
                    int y = startY + i * stepY;

                    if (x < 0 || x >= GlobalSettings.BoardLength || y < 0 || y >= GlobalSettings.BoardLength) break;

                    var cellState = CellStateConverter.ToEnum(Board[x][y]);
                    if (cellState != CellState.Empty && cellState == prevState)
                    {
                        count++;
                        if (count == 5)
                        {
                            GameFinished = true;
                            Winner = cellState;
                            return true;
                        }
                        if (count == 4)
                        {
                            if (x - stepX >= 0 || y - stepY >= 0 || x + stepX < GlobalSettings.BoardLength || y + stepY < GlobalSettings.BoardLength)
                            {
                                if ((CellStateConverter.ToEnum(Board[x - stepX][y - stepY]) == CellState.Empty || CellStateConverter.ToEnum(Board[x + stepX][y + stepY]) == CellState.Empty) && prevState == Next)
                                {
                                    GameState = GameState.Endgame;
                                    Console.WriteLine(GameState.ToString());
                                }
                                else if (((CellStateConverter.ToEnum(Board[x - stepX][y - stepY]) == CellState.Empty && CellStateConverter.ToEnum(Board[x + stepX][y + stepY]) != CellState.Empty) ||
                                        (CellStateConverter.ToEnum(Board[x - stepX][y - stepY]) != CellState.Empty && CellStateConverter.ToEnum(Board[x + stepX][y + stepY]) == CellState.Empty)) &&
                                        prevState != Next)
                                {
                                    GameState = GameState.Midgame;
                                    Console.WriteLine(GameState.ToString());
                                }
                                else
                                {
                                    GameState = GameState.Midgame;
                                    Console.WriteLine(GameState.ToString());
                                }
                            }
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
            for (int row = 0; row < GlobalSettings.BoardLength; row++)
            {
                for (int col = 0; col < GlobalSettings.BoardLength; col++)
                {
                    if (CellStateConverter.ToEnum(Board[row][col]) != CellState.Empty)
                    {
                        if (CheckLine(row, col, 0, 1) ||
                            CheckLine(row, col, 1, 0) ||
                            CheckLine(row, col, 1, 1) ||
                            CheckLine(row, col, 1, -1))
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