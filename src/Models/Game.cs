﻿using Newtonsoft.Json.Converters;
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
        public string[][] BoardState { get; set; }
        [JsonIgnore]
        public bool GameFinished { get; set; }
        [JsonIgnore]
        public CellState? Winner { get; set; }
        [JsonIgnore]
        public List<Move> History { get; set; }
        [JsonIgnore]
        private CellState _next { get; set; } = CellState.X;

        public Game(string name, DifficultyType difficulty)
        {
            Uuid = Guid.NewGuid().ToString();
            Name = name;
            Difficulty = difficulty;
            GameState = GameState.Beginning;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
            ModelsExtensions.GameGenerateEmptyBoard(this); // Creates an empty board
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
                    if (x < 0 || x >= GlobalSettings.BoardLength || y < 0 || y >= GlobalSettings.BoardLength) break;

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
                            if (x - stepX >= 0 || y - stepY >= 0 || x + stepX < GlobalSettings.BoardLength || y + stepY < GlobalSettings.BoardLength)
                            {
                                if (CellStateConverter.ToEnum(BoardState[x - stepX][y - stepY]) == CellState.Empty || CellStateConverter.ToEnum(BoardState[x + stepX][y + stepY]) == CellState.Empty)
                                {
                                    GameState = GameState.Endgame;
                                }
                                else
                                {
                                    GameState = GameState.Midgame;
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