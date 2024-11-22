using AutoMapper;

namespace TourDeApp.Infrastructure.AutoMapperConvertors;

public interface ITypeConverter<in TSource, TDestination>
{
    TDestination Convert(TSource source, TDestination destination, ResolutionContext context);
}