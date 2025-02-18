using TourDeApp.Models;
using TourDeApp.Models.Schemas;


// Toto je dočasně dokud Viktor neudělá controller
namespace TourDeApp.Components.Services
{
    public class GameService
    {
        private Game _game;

        public Game CreateGame()
        {
            _game = new Game("Nova hra", DifficultyType.Beginner);
            return _game;
        }

        public Game GetCurrentGame()
        {
            return _game;
        }

        public bool GameExists()
        {
            return _game != null;
        }

        public void SetGame(Game game)
        {
            _game = game;
        }
    }
}
