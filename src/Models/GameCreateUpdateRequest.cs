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
        public string Name { get; set; }
        [JsonIgnore]
        public BoardStateJson BoardState { get; set; }
        [Required]
        public string[][] Board { get => BoardState.Board; set => BoardState.Board = value; }
        [Required]
        public string Difficulty { get; set; }
        [JsonIgnore]
        public DifficultyType EnumDifficulty { get; set; }

        public bool BindDifficultyType()
        {
            Difficulty = Char.ToUpper(Difficulty[0]) + Difficulty.Substring(1).ToLower();

            if (Enum.TryParse(typeof(DifficultyType), Difficulty, out var result))
            {
                EnumDifficulty = (DifficultyType)result;
                return true;
            }

            return false;
        }

        public GameCreateUpdateRequest(Game game)
        {
            Name = game.Name;
            BoardState = new BoardStateJson(game.Board);
            EnumDifficulty = game.Difficulty;
            Difficulty = EnumDifficulty.ToString();
        }
    }
}
