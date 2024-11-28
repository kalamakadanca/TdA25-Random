using System.Runtime.CompilerServices;

namespace TourDeApp.Infrastructure.ExtensionMethods
{
    public static class ModelsExtensions
    {
        public static void GameGenerateEmptyBoard(this Models.Game game)
        {
            if (game.Board == null) game.Board = new string[GlobalSettings.BoardLength][];

            for (int row = 0; row < GlobalSettings.BoardLength; row++) 
            {
                game.Board[row] = new string[GlobalSettings.BoardLength];

                for (int column = 0; column < GlobalSettings.BoardLength; column++)
                    game.Board[row][column] = "";
            }
                
        }
    }
}
