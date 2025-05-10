using WebAPI.Application.Interfaces.CQRSInterfaces;
using WebAPI.Application.Interfaces.Repositories;
using WebAPI.Application.ResultPattern;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Application.UseCases.RoomServices.GetOrCreate;

public class GetOrCreateRoomServicesCommandHandler : ICommandHandlerWithResult<GetOrCreateRoomServicesCommand, List<RoomService>>
{
    private readonly IRoomServiceRepository _roomServiceRepository;

    public GetOrCreateRoomServicesCommandHandler( IRoomServiceRepository roomServiceRepository )
    {
        _roomServiceRepository = roomServiceRepository;
    }

    public async Task<Result<List<RoomService>>> Handle( GetOrCreateRoomServicesCommand command, CancellationToken cancellationToken )
    {
        IReadOnlyList<RoomService> existingRoomServices = await _roomServiceRepository.GetAllByNames( command.RoomServiceNames );

        List<string> newRoomServicesNames = command.RoomServiceNames
            .Except( existingRoomServices.Select( a => a.Name ) )
            .ToList();

        List<RoomService> newRoomServices = newRoomServicesNames.Select( name => new RoomService
        {
            Name = name
        } ).ToList();

        if ( newRoomServices.Any() )
        {
            await _roomServiceRepository.CreateRangeAsync( newRoomServices );
        }

        List<RoomService> resultList = existingRoomServices
            .Concat( newRoomServices )
            .ToList();

        return Result<List<RoomService>>.Success( resultList );
    }
}
