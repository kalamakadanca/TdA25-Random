using TourDeApp.Models.JsonModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models
{
    public class GameCreateUpdateRequest
    {
        public string Name { get; set; }
        public BoardStateJson BoardState { get; set; }
        public DifficultyType Difficulty { get; set; }
    }
}
