using System.Runtime.CompilerServices;

namespace TourDeApp.Infrastructure.ExtensionMethods
{
    public static class ModelsExtensions
    {
        public static void GameGenerateEmptyBoard(this Models.Game game)
        {
            if (game.BoardState == null) game.BoardState = new string[GlobalSettings.BoardLength][];

            for (int row = 0; row < GlobalSettings.BoardLength; row++) 
            {
                game.BoardState[row] = new string[GlobalSettings.BoardLength];

                for (int column = 0; column < GlobalSettings.BoardLength; column++)
                    game.BoardState[row][column] = "";
            }
                
        }
    }
}
