using TourDeApp.Components.Models;
using TourDeApp.Components.Models.Schemas;

namespace TourDeApp.Components.Services
{
    public class GameService
    {
        private Game _game;

        public GameService()
        {
            _game = new Game("Default game", Difficulty.Easy);
        }

        public Game GetGame()
        {
            return _game;
        }
    }
}