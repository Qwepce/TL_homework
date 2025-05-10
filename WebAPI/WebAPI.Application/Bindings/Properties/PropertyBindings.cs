using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.MappingConfigurations.Properties;
using WebAPI.Application.UseCases.Properties.Commands.CreateProperty;
using WebAPI.Application.UseCases.Properties.Commands.DeleteProperty;
using WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;
using WebAPI.Application.UseCases.Properties.Dto;
using WebAPI.Application.UseCases.Properties.Queries.GetAll;
using WebAPI.Application.UseCases.Properties.Queries.GetById;

namespace WebAPI.Application.Bindings.Properties;

public static class PropertyBindings
{
    public static IServiceCollection AddPropertyBindings( this IServiceCollection services )
    {
        services.AddScoped<IQueryHandler<GetPropertyByIdQuery, PropertyDto>, GetPropertyByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllPropertiesQuery, List<PropertyDto>>, GetAllPropertiesQueryHandler>();
        services.AddScoped<ICommandHandler<UpdatePropertyCommand>, UpdatePropertyCommandHandler>();
        services.AddScoped<ICommandHandler<DeletePropertyCommand>, DeletePropertyCommandHandler>();
        services.AddScoped<ICommandHandlerWithResult<CreatePropertyCommand, int>, CreatePropertyCommandHandler>();

        PropertiesMappingConfiguration.AddEntityMapping();

        return services;
    }
}
