using AutoMapper;
using TourDeApp.Models;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure.AutoMapperConvertors;

public class GameDbToGame : ITypeConverter<GameDb, Game>, AutoMapper.ITypeConverter<GameDb, Game>
{ 
    public Game Convert(GameDb source, Game destination, ResolutionContext context)
    {
        destination = new Game(source.Name, source.Difficulty);
        destination.UpdatedAt = source.UpdatedAt;
        destination.CreatedAt = source.CreatedAt;
        destination.Uuid = source.Uuid;
        destination.Board = source.Board;
        destination.GameState = source.GameState;

        return destination;
    }
}