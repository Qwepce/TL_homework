using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.Amenities.GetOrCreate;

public class GetOrCreateAmenitiesCommandHandler : ICommandHandlerWithResult<GetOrCreateAmenitiesCommand, List<Amenity>>
{
    private readonly IAmenityRepository _amenityRepository;

    public GetOrCreateAmenitiesCommandHandler( IAmenityRepository amenityRepository )
    {
        _amenityRepository = amenityRepository;
    }

    public async Task<Result<List<Amenity>>> Handle( GetOrCreateAmenitiesCommand command, CancellationToken cancellationToken )
    {
        IReadOnlyList<Amenity> existingAmenities = await _amenityRepository.GetAllByNames( command.AmenityNames );

        List<string> newAmenityNames = command.AmenityNames
            .Except( existingAmenities.Select( a => a.Name ) )
            .ToList();

        List<Amenity> newAmenities = newAmenityNames.Select( name => new Amenity
        {
            Name = name
        } ).ToList();

        if ( newAmenities.Any() )
        {
            await _amenityRepository.CreateRangeAsync( newAmenities );
        }

        List<Amenity> resultList = existingAmenities
            .Concat( newAmenities )
            .ToList();

        return Result<List<Amenity>>.Success( resultList );
    }
}
