using AutoMapper;
using TourDeApp.Infrastructure.AutoMapperConvertors;
using TourDeApp.Models.DataBaseModels;

namespace TourDeApp.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Game, GameDb>().ConvertUsing<CellArrayToCellListTypeConvertor>();
        CreateMap<GameDb, Game>().ConvertUsing<CellListToCellArrayTypeConvertor>();
    }
}