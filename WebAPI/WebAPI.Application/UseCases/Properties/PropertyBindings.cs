using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces.CQRS.HandlersInterfaces;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.UseCases.Properties.Commands.CreateProperty;
using WebAPI.Application.UseCases.Properties.Commands.DeletePropertyById;
using WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;
using WebAPI.Application.UseCases.Properties.Dto;
using WebAPI.Application.UseCases.Properties.Queries.GetAllProperties;
using WebAPI.Application.UseCases.Properties.Queries.GetPropertyById;

namespace WebAPI.Application.UseCases.Properties;

public static class PropertyBindings
{
    public static IServiceCollection AddPropertyBindings( this IServiceCollection services )
    {
        services.AddScoped<IQueryHandler<GetPropertyByIdQuery, PropertyDto>, GetPropertyByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllPropertiesQuery, List<PropertyDto>>, GetAllPropertiesQueryHandler>();
        services.AddScoped<ICommandHandler<UpdatePropertyCommand>, UpdatePropertyCommandHandler>();
        services.AddScoped<ICommandHandler<DeletePropertyByIdCommand>, DeletePropertyByIdCommandHandler>();
        services.AddScoped<ICommandHandlerWithResult<CreatePropertyCommand, int>, CreatePropertyCommandHandler>();

        services.AddScoped<IRequestValidator<CreatePropertyCommand>, CreatePropertyCommandValidator>();
        services.AddScoped<IRequestValidator<UpdatePropertyCommand>, UpdatePropertyCommandValidator>();
        services.AddScoped<IRequestValidator<DeletePropertyByIdCommand>, DeletePropertyByIdCommandValidator>();
        services.AddScoped<IRequestValidator<GetPropertyByIdQuery>, GetPropertyByIdQueryValidator>();

        PropertiesMappingConfiguration.AddEntityMapping();

        return services;
    }
}
