using AutoMapper;
using TourDeApp.Models;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure.AutoMapperConvertors;

public abstract class CellArrayToCellListTypeConvertor : ITypeConverter<Game, GameDb>, AutoMapper.ITypeConverter<Game, GameDb>
{
    public GameDb Convert(Game source, GameDb destination, ResolutionContext context)
    {
        for (int i = 0; i < source.Board.Board.Length; i++)
            for (int j = 0; j < source.Board.Board.Length; j++)
                destination.GameBoard.Board.Add(new CellDb { Column = j, Row = i, State = source.Board.Board[i, j].State });

        return destination;
    }
}