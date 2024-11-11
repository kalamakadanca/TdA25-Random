namespace TourDeApp.Components.Models
{
    public class GameCreateUpdateRequest
    {
        private Game _game { get; set; }

        public GameCreateUpdateRequest(Game game)
        {
            _game = game;
        }
    }
}
