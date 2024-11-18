using System.ComponentModel.DataAnnotations.Schema;

namespace TourDeApp.Models.DataBaseModels;

public class GameBoardDb
{
    public int Id { get; set; }
    public int Size { get; set; }
    public List<CellDb> Board { get; set; } = new();
    
    public int GameId { get; set; }
    public GameDb Game { get; set; }
}