namespace WebAPI.Application.UseCases.RoomServices.GetOrCreateCommand;

public class GetOrCreateRoomServicesCommand
{
    public IEnumerable<string> RoomServiceNames { get; init; } = [];
}
