using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.ResultPattern;

namespace WebAPI.Application.UseCases.RoomServices.GetOrCreate;

public class GetOrCreateRoomServicesCommandValidator : IRequestValidator<GetOrCreateRoomServicesCommand>
{
    public Task<Result> Validate( GetOrCreateRoomServicesCommand command )
    {
        if ( command.RoomServiceNames.Count() == 0 )
        {
            return Task.FromResult( Result.Failure( new Error( "Room services list can not be empty" ) ) );
        }

        foreach ( string roomServiceName in command.RoomServiceNames )
        {
            if ( string.IsNullOrWhiteSpace( roomServiceName ) )
            {
                return Task.FromResult( Result.Failure( new Error( "Can not create room service with empty name" ) ) );
            }
            if ( roomServiceName.Length > 50 )
            {
                return Task.FromResult( Result.Failure( new Error( $"Room service name must be less than 50 charcters. {roomServiceName}" ) ) );
            }
        }

        return Task.FromResult( Result.Success() );
    }
}
