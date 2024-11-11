namespace TourDeApp.Components.Models.Schemas
{
    public class GameBoard
    {
        public int Size { get; set; }


        public Cell[,] Board { get; set; }

        public GameBoard(int size = 15)
        {
            Size = size;
            Board = new Cell[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Board[i, j].ID = [i, j];
                }
            }
        }
    }
}
