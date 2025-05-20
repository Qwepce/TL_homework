using Mapster;
using WebAPI.Application.UseCases.Properties.Commands.UpdateCommand;
using WebAPI.Application.UseCases.Properties.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties;

public static class PropertiesMappingConfiguration
{
    public static void AddEntityMapping()
    {
        TypeAdapterConfig<Property, PropertyDto>.NewConfig()
            .Map( dest => dest.Name, src => src.Name )
            .Map( dest => dest.Country, src => src.Country )
            .Map( dest => dest.City, src => src.City )
            .Map( dest => dest.Address, src => src.Address )
            .Map( dest => dest.Latitude, src => src.Latitude )
            .Map( dest => dest.Longitude, src => src.Longitude );

        TypeAdapterConfig<UpdatePropertyCommand, Property>.NewConfig()
            .IgnoreNullValues( true )
            .Map( dest => dest.Name, src => src.Name.Trim() )
            .Map( dest => dest.Country, src => src.Country.Trim() )
            .Map( dest => dest.City, src => src.City.Trim() )
            .Map( dest => dest.Address, src => src.Address.Trim() )
            .Map( dest => dest.Latitude, src => src.Latitude )
            .Map( dest => dest.Longitude, src => src.Longitude );
    }
}
