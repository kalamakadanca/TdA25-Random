using AutoMapper;
using TourDeApp.Models;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure.AutoMapperConvertors;

public class GameToGameDb : ITypeConverter<Game, GameDb>, AutoMapper.ITypeConverter<Game, GameDb>
{
    public GameDb Convert(Game source, GameDb destination, ResolutionContext context)
    {
        // Initializing a new gamedb object
        destination = new GameDb();

        destination.Board = source.Board;
        destination.GameState = source.GameState;
        destination.Difficulty = source.Difficulty;
        destination.Name = source.Name;
        destination.Uuid = source.Uuid;
        destination.CreatedAt = source.CreatedAt;
        destination.UpdatedAt = source.UpdatedAt;
        
        return destination;
    }
}