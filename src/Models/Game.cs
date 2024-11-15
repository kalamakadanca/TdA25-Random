﻿using System.Runtime.CompilerServices;
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
            this.BoardState.Board[cell.CellID[0], cell.CellID[1]].State = _next;

            if (_next == CellState.Cross) _next = CellState.Circle;
            else _next = CellState.Cross;
            
            // I will later implement this to get called when the BoardState.Board is changed
            CheckWin();
        }

        // Checks if there are 5 pieces next to each other horizontally
        public void CheckWin()
        {
            // TODO: Make it work vertically, diagonally and improve, make more readable
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
                        GameFinished = true;
                    }
                }

                if (GameFinished) break;
            }
        }
    }
}
