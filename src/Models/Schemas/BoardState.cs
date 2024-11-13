namespace TourDeApp.Models.Schemas
{
    public class BoardState
    {
        public Cell[,] Board { get; set; }


        public BoardState(int size = 15)
        {
            Board = new Cell[size, size];
        }
    }
}
