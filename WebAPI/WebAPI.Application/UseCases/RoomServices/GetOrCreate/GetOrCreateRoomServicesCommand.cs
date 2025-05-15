namespace WebAPI.Application.UseCases.RoomServices.GetOrCreate;

public class GetOrCreateRoomServicesCommand
{
    public IEnumerable<string> RoomServiceNames { get; init; } = [];
}
