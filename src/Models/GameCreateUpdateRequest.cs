using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using TourDeApp.Components;
using TourDeApp.Models.JsonModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models
{
    public class GameCreateUpdateRequest
    {
        [Required] 
        public string Name { get; set; } = string.Empty;
        [Required] 
        public BoardStateJson BoardState { get; set; } = new();
        [Required]
        public string Difficulty { get; set; } = String.Empty;
        [JsonIgnore]
        public DifficultyType EnumDifficulty { get; set; }
        
        public bool BindDifficultyType()
        {
            if (Enum.TryParse(typeof(DifficultyType), Difficulty, out var result))
            {
                EnumDifficulty = (DifficultyType)result;
                return true;
            }

            return false;
        }
    }
}
