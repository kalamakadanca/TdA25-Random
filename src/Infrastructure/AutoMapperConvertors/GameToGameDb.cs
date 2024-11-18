using AutoMapper;
using TourDeApp.Models;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Infrastructure.AutoMapperConvertors;

public class GameToGameDb : ITypeConverter<Game, GameDb>, AutoMapper.ITypeConverter<Game, GameDb>
{
    public GameDb Convert(Game source, GameDb destination, ResolutionContext context)
    {
        int lenght = source.BoardState.Board.GetLength(1);
        destination = new GameDb();
        
        for (int i = 0; i < lenght; i++)
            for (int j = 0; j < lenght; j++)
                destination.GameBoard.Board.Add(new CellDb { Column = j, Row = i, State = source.BoardState.Board[i, j].State });

        destination.GameState = source.GameState;
        destination.Difficulty = source.Difficulty;
        destination.Name = source.Name;
        destination.Uuid = source.Uuid;
        destination.CreatedAt = source.CreatedAt;
        destination.UpdatedAt = source.UpdatedAt;
        
        return destination;
    }
}