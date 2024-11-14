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
        
        // Sets the cross to be the first and then is used to determine who's turn it is
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
        }

    }
}
