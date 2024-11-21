namespace TourDeApp.Models.Schemas;

public class Move
{
    public int[] Position { get; set; }
    public CellState State { get; set; }

    public Move(int[] position, CellState state)
    {
        Position = position;
        State = state;
    }
}