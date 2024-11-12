using TourDeApp.Models.Schemas;

namespace TourDeApp.Models.DataBaseModels;

public class CellDb
{
    public int Id { get; set; }
    public CellState State { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    
    public int GameBoardDbId { get; set; }
    public GameBoardDb GameBoard { get; set; } = new();
}