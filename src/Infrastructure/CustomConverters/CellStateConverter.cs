using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure.CustomConverters;

public static class CellStateConverter
{
    public static CellState ToEnum(string state)
    {
        if (Enum.TryParse(typeof(CellState), state, out var result))
            return (CellState)result;

        // Default value
        return CellState.Empty;
    }

    public static string ToString(CellState state)
    {
        return state == CellState.Empty ? "" : state.ToString();
    }
}