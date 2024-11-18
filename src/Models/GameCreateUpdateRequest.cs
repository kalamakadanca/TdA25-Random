using TourDeApp.Models.JsonModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models
{
    public class GameCreateUpdateRequest
    {
        public string name { get; set; }
        public BoardStateJson BoardState { get; set; }
        public DifficultyType difficulty { get; set; }

        public GameCreateUpdateRequest(string _name, BoardStateJson _boardstate, DifficultyType _difficulty)
        {
            name = _name;
            BoardState = _boardstate;
            difficulty = _difficulty;
        }
    }
}
