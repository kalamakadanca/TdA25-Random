using AutoMapper;
using TourDeApp.Models.JsonModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure.AutoMapperConvertors;

public class StrArrayToCellArrayTypeConverter : ITypeConverter<BoardStateJson, BoardState>, AutoMapper.ITypeConverter<BoardStateJson, BoardState>
{
    public BoardState Convert(BoardStateJson source, BoardState destination, ResolutionContext context)
    {
        destination = new BoardState();
        
        for (int i = 0; i < source.Board.Length; i++)
            for (int j = 0; j < source.Board.Length; j++)
                destination.Board[i, j] = new Cell
                {
                    CellID = [i, j],
                    State = Helper.StrToCellState(source.Board[i][j])
                };

        return destination;
    }
}