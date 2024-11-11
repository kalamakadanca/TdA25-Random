using TourDeApp.Controllers.api.v1;

namespace TourDeApp.Models
{
    public class Game
    {
        public Guid Uuid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public DifficultyType Difficulty { get; set; }
        public string GameState { get; set; }
        public string[][] Board { get; set; }
    }
}
