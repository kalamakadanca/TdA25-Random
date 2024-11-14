namespace TourDeApp.Models.Schemas
{
    public class BoardState
    {
        public Cell[,] Board { get; set; }

        public int Size { get; set; }
        public BoardState(int size = 15)
        {
            Size = size;
            Board = new Cell[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Board[i, j] = new Cell();
                    Board[i, j].CellID = [i, j];
                }
            }
        }
    }
}
