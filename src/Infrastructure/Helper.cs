using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure;

public static class Helper
{
    public static CellState StrToCellState(string state) => state switch
    {
        "" => CellState.Empty,
        "X" => CellState.X,
        "O" => CellState.O,
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
    };
}