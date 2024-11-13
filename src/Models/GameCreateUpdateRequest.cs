using TourDeApp.Models.Schemas;

namespace TourDeApp.Models
{
    public class GameCreateUpdateRequest
    {
        public string name { get; set; }
        public BoardState BoardState { get; set; }
        public DifficultyType difficulty { get; set; }

        public GameCreateUpdateRequest(string _name, BoardState _boardstate, DifficultyType _difficulty)
        {
            name = _name;
            BoardState = _boardstate;
            difficulty = _difficulty;
        }
    }
}
