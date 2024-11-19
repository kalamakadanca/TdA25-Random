using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models.DataBaseModels
{
    public class GameDb
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Uuid { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DifficultyType Difficulty { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GameState GameState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        [JsonIgnore]
        public int GameBoardId { get; set; }
        [JsonIgnore]
        public GameBoardDb GameBoard { get; set; } = new();
    }
}
