using WebAPI.Application.Interfaces.CQRS.BaseHandlers;
using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomServices.GetOrCreateCommand;

public class GetOrCreateRoomServicesCommandHandler : BaseCommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>>
{
    private readonly IRoomServiceRepository _roomServiceRepository;

    public GetOrCreateRoomServicesCommandHandler(
        IRoomServiceRepository roomServiceRepository,
        IRequestValidator<GetOrCreateRoomServicesCommand> validator )
        : base( validator )
    {
        _roomServiceRepository = roomServiceRepository;
    }

    protected override async Task<Result<List<RoomService>>> HandleCommand( GetOrCreateRoomServicesCommand command, CancellationToken cancellationToken )
    {
        IReadOnlyList<RoomService> existingRoomServices = await _roomServiceRepository.GetAllByNames( command.RoomServiceNames );

        List<string> newRoomServicesNames = command.RoomServiceNames
            .Except( existingRoomServices.Select( a => a.Name ), StringComparer.OrdinalIgnoreCase )
            .ToList();

        List<RoomService> newRoomServices = newRoomServicesNames.Select( name => new RoomService
        {
            Name = name
        } ).ToList();

        if ( newRoomServices.Any() )
        {
            await _roomServiceRepository.AddRangeAsync( newRoomServices );
        }

        List<RoomService> resultList = existingRoomServices
            .Concat( newRoomServices )
            .ToList();

        return Result<List<RoomService>>.Success( resultList );
    }
}
