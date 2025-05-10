using Mapster;
using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Properties.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Queries.GetAll;

public class GetAllPropertiesQueryHandler : IQueryHandler<GetAllPropertiesQuery, List<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetAllPropertiesQueryHandler( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<List<PropertyDto>>> Handle( GetAllPropertiesQuery query, CancellationToken cancellationToken )
    {
        IEnumerable<Property> properties = await _propertyRepository.GetAll();

        List<PropertyDto> propertiesDto = properties.Adapt<List<PropertyDto>>();

        return Result<List<PropertyDto>>.Success( propertiesDto );
    }
}
