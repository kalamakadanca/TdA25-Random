namespace TourDeApp.Models.Schemas
{
    public class BoardState
    {
        private int _size { get; set; }
        public Cell[,] Board { get; set; }

        public BoardState(int size = 15)
        {
            _size = size;
            Board = new Cell[_size, _size];
        }
    }
}
