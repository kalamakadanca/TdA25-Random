namespace TourDeApp.Models.Schemas
{
    public class BoardState
    {
        public Cell[,] Board { get; set; }

        // Sets the cross the be the first and then is used to determine who's turn it is
        private CellState next { get; set; } = CellState.Cross;

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
                    Board[i, j].CellID = new int[] {i, j};
                }
            }
        }

        public Cell UpdateCell(Cell cell)
        {
            Board[cell.CellID[0], cell.CellID[1]].State = next;

            if (next == CellState.Cross) next = CellState.Circle;
            else next = CellState.Cross;

            return Board[cell.CellID[0], cell.CellID[1]];
        }

        public enum NextState
        {
            Cross,
            Circle
        }
    }
}
