namespace TourDeApp.Models.Schemas
{
    public class Cell
    {
        public CellState State { get; set; }
        public int[]? CellID { get; set; }
        public bool WinningCell { get; set; } = false;
    }
}
