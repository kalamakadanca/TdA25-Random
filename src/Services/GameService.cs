using TourDeApp.Models;
using TourDeApp.Models.Schemas;


// Toto je dočasně dokud Viktor neudělá controller
namespace TourDeApp.Services
{
    public class GameService
    {
        private Game _game;
        private event Action OnWin;
        private event Action OnMove;

        public Game CreateGame()
        {
            _game = new Game("Nova hra", DifficultyType.Beginner);
            return _game;
        }

        public Game GetGame() => _game;

        public bool GameExists() => _game is not null;
        
        public void UpdateBoard(Models.Schemas.Cell cell)
        {
            _game.UpdateBoard(cell);
            
            OnMove.Invoke();
            
            if (_game.CheckWinAndSetGameState())
            {
                OnWin.Invoke();
            }
        }
        
        public void SubscribeMove(Action callback)
        {
            OnMove += callback;
        }

        public void SubscribeWin(Action callback)
        {
            OnWin += callback;
        }

        public void UnsubscribeMove(Action callback)
        {
            OnMove -= callback;
        }

        public void UnsubscribeWin(Action callback)
        {
            OnWin -= callback;
        }
    }
}
