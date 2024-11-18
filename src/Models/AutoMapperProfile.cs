using AutoMapper;
using TourDeApp.Infrastructure.AutoMapperConvertors;
using TourDeApp.Models.DataBaseModels;
using TourDeApp.Models.JsonModels;
using TourDeApp.Models.Schemas;

namespace TourDeApp.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Game, GameDb>().ConvertUsing<GameToGameDb>();
        CreateMap<GameDb, Game>().ConvertUsing<GameDbToGame>();
        CreateMap<BoardStateJson, BoardState>().ConvertUsing<StrArrayToCellArrayTypeConverter>();
    }
}