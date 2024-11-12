using TourDeApp.Models.Schemas;

namespace TourDeApp.Models
{
    public class GameCreateUpdateRequest
    {
        public string uuid { get; set; }
        public string name { get; set; }
        public BoardState BoardState { get; set; }
        public DifficultyType difficulty { get; set; }

        public GameCreateUpdateRequest(string _uuid, string _name, BoardState boardState, DifficultyType _difficulty)
        {
            uuid = _uuid;
            name = _name;
            BoardState = boardState;
            difficulty = _difficulty;
        }
    }
}
