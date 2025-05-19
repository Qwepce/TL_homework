using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Amenities.GetOrCreateAmenities;

public class GetOrCreateAmenitiesCommandHandler : BaseCommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>>
{
    private readonly IAmenityRepository _amenityRepository;

    public GetOrCreateAmenitiesCommandHandler(
        IAmenityRepository amenityRepository,
        IRequestValidator<GetOrCreateAmenitiesCommand> validator )
        : base( validator )
    {
        _amenityRepository = amenityRepository;
    }

    protected override async Task<Result<List<Amenity>>> HandleCommand( GetOrCreateAmenitiesCommand command, CancellationToken cancellationToken )
    {
        IReadOnlyList<Amenity> existingAmenities = await _amenityRepository.GetAllByNames( command.AmenityNames );

        List<string> newAmenityNames = command.AmenityNames
            .Except( existingAmenities.Select( a => a.Name ), StringComparer.OrdinalIgnoreCase )
            .ToList();

        List<Amenity> newAmenities = newAmenityNames.Select( name => new Amenity
        {
            Name = name
        } ).ToList();

        if ( newAmenities.Any() )
        {
            await _amenityRepository.AddRangeAsync( newAmenities );
        }

        List<Amenity> resultList = existingAmenities
            .Concat( newAmenities )
            .ToList();

        return Result<List<Amenity>>.Success( resultList );
    }
}
