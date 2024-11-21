using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models.DataBaseModels
{
    public class GameDb
    {
        public int Id { get; set; }
        [Required]
        public string Uuid { get; set; }
        [Required]
        public string Name { get; set; }
        public DifficultyType Difficulty { get; set; }
        public GameState GameState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string BoardJson { get; set; }
        [NotMapped]
        public string[][] Board
        {
            get => (string.IsNullOrEmpty(BoardJson) ? [] : JsonConvert.DeserializeObject<string[][]>(BoardJson))!;
            set => BoardJson = JsonConvert.SerializeObject(value);
        }
        
        public int GameBoardId { get; set; }
        public GameBoardDb GameBoard { get; set; } = new();
    }
}
