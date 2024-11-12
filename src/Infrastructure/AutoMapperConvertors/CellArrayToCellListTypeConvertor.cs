using AutoMapper;
using TourDeApp.Models;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure.AutoMapperConvertors;

public abstract class CellArrayToCellListTypeConvertor : ITypeConverter<Game, GameDb>, AutoMapper.ITypeConverter<Game, GameDb>
{
    public GameDb Convert(Game source, GameDb destination, ResolutionContext context)
    {
        for (int i = 0; i < source.BoardState.Board.Length; i++)
            for (int j = 0; j < source.BoardState.Board.Length; j++)
                destination.GameBoard.Board.Add(new CellDb { Column = j, Row = i, State = source.BoardState.Board[i, j].State });

        return destination;
    }
}