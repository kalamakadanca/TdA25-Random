using TourDeApp.Models;
using TourDeApp.Models.Schemas;


// Toto je dočasně dokud Viktor neudělá controller
namespace TourDeApp.Components.Services
{
    public class GameService
    {
        private Game _game;

        public GameService()
        {
            _game = new Game("Default game", DifficultyType.Easy);
        }

        public Game GetGame()
        {
            return _game;
        }

        public BoardState GetBoard(Game game)
        {
            return game.BoardState;
        }
    }
}