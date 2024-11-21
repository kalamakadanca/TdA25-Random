using AutoMapper;
using TourDeApp.Models;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure.AutoMapperConvertors;

public class GameDbToGame : ITypeConverter<GameDb, Game>, AutoMapper.ITypeConverter<GameDb, Game>
{ 
    public Game Convert(GameDb source, Game destination, ResolutionContext context)
    {
        //foreach (CellDb cell in source.GameBoard.Board) destination.BoardState[cell.Row][cell.Column] = cell.State;

        return destination;
    }
}