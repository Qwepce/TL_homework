namespace WebAPI.Application.UseCases.RoomServices.Commands.GetOrCreateCommand;

public class GetOrCreateRoomServicesCommand
{
    public IEnumerable<string> RoomServiceNames { get; init; } = [];
}
