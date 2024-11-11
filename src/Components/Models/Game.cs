using TourDeApp.Components.Models.Schemas;

namespace TourDeApp.Components.Models
{
    public class Game
    {
        public required string Uuid { get; set; }
        public required string Name { get; set; }
        public required Difficulty Difficulty { get; set; }
        public required GameState GameState { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public required GameBoard Board { get; set; }

        public Game(string name, Difficulty difficulty)
        {
            Uuid = Guid.NewGuid().ToString();
            Name = name;
            Difficulty = difficulty;
            GameState = GameState.Beginning;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
            Board = new GameBoard();
        }
    }
}
