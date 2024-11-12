using System.ComponentModel.DataAnnotations;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models.DataBaseModels
{
    public class GameDb
    {
        [Required]
        public string Uuid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DifficultyType Difficulty { get; set; }
        [Required]
        public GameState GameState { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public GameBoard Board { get; set; }
    }
}
