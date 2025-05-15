using Mapster;
using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Properties.Dto;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Properties.Queries.GetById;

public class GetPropertyByIdQueryHandler : BaseQueryHandler<GetPropertyByIdQuery, PropertyDto>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertyByIdQueryHandler(
        IPropertyRepository propertyRepository,
        IRequestValidator<GetPropertyByIdQuery> validator )
        : base( validator )
    {
        _propertyRepository = propertyRepository;
    }

    protected override async Task<Result<PropertyDto>> HandleQuery( GetPropertyByIdQuery query, CancellationToken cancellationToken )
    {
        Property property = await _propertyRepository.GetById( query.PropertyId );

        PropertyDto propertyDto = property.Adapt<PropertyDto>();

        return Result<PropertyDto>.Success( propertyDto );
    }
}
