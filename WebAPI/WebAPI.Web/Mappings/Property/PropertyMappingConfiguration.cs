using Mapster;
using WebAPI.Application.UseCases.Properties.Commands.CreateProperty;
using WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;
using WebAPI.Application.UseCases.Properties.Dto;
using WebAPI.Web.Contracts.PropertyContracts;

namespace WebAPI.Web.Mappings.Property;

public static class PropertyMappingConfiguration
{
    public static void AddEntityMapping()
    {
        TypeAdapterConfig<CreatePropertyContract, CreatePropertyCommand>.NewConfig()
            .Map( dest => dest.Name, src => src.Name.Trim() )
            .Map( dest => dest.Country, src => src.Country.Trim() )
            .Map( dest => dest.City, src => src.City.Trim() )
            .Map( dest => dest.Address, src => src.Address.Trim() )
            .Map( dest => dest.Latitude, src => src.Latitude )
            .Map( dest => dest.Longitude, src => src.Longitude );

        TypeAdapterConfig<UpdatePropertyContract, UpdatePropertyCommand>.NewConfig()
            .Map( dest => dest.Country, src => src.Country.Trim() )
            .Map( dest => dest.City, src => src.City.Trim() )
            .Map( dest => dest.Name, src => src.Name.Trim() )
            .Map( dest => dest.Address, src => src.Address.Trim() )
            .Map( dest => dest.Latitude, src => src.Latitude )
            .Map( dest => dest.Longitude, src => src.Longitude );

        TypeAdapterConfig<PropertyDto, ReadPropertyContract>.NewConfig()
            .Map( dest => dest.Name, src => src.Name )
            .Map( dest => dest.Country, src => src.Country )
            .Map( dest => dest.City, src => src.City )
            .Map( dest => dest.Address, src => src.Address )
            .Map( dest => dest.Latitude, src => src.Latitude )
            .Map( dest => dest.Longitude, src => src.Longitude );
    }
}
