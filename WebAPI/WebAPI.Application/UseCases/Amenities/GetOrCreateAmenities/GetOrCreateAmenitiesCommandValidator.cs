using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.UseCases.Amenities.GetOrCreateAmenities;

public class GetOrCreateAmenitiesCommandValidator : IRequestValidator<GetOrCreateAmenitiesCommand>
{
    public Task<Result> Validate( GetOrCreateAmenitiesCommand command )
    {
        if ( !command.AmenityNames.Any() )
        {
            return Task.FromResult( Result.Failure( new Error( "Amenities list can not be empty" ) ) );
        }

        foreach ( string amenityName in command.AmenityNames )
        {
            if ( string.IsNullOrWhiteSpace( amenityName ) )
            {
                return Task.FromResult( Result.Failure( new Error( "Can not create amenity with empty name" ) ) );
            }
            if ( amenityName.Length > 50 )
            {
                return Task.FromResult( Result.Failure( new Error( $"Amenity name must be less than 50 charcters. {amenityName}" ) ) );
            }
        }

        return Task.FromResult( Result.Success() );
    }
}
