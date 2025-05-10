namespace WebAPI.Web.Contracts.RoomTypeContracts;

public class ReadRoomTypeContract
{
    public int PropertyId { get; init; }
    public string Name { get; init; }
    public decimal DailyPrice { get; init; }
    public string Currency { get; init; }
    public int MinPersonCount { get; init; }
    public int MaxPersonCount { get; init; }
    public int TotalRoomsCount { get; init; }
    public List<string> RoomServices { get; init; } = [];
    public List<string> RoomAmenities { get; init; } = [];
}
