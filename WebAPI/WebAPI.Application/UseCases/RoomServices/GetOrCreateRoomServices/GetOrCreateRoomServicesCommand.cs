namespace WebAPI.Application.UseCases.RoomServices.GetOrCreateRoomServices;

public class GetOrCreateRoomServicesCommand
{
    public IEnumerable<string> RoomServiceNames { get; init; } = [];
}
