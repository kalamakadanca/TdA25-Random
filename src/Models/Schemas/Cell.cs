namespace TourDeApp.Models.Schemas
{
    public class Cell
    {
        public CellState State { get; set; }
        public int[] CellID { get; set; }
        
        public Cell()
        {
            State = CellState.Empty;
        }
    }
}
