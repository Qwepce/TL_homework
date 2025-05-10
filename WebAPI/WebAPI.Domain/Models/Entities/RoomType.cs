using WebAPI.Domain.Models.Enums;

namespace WebAPI.Domain.Models.Entities;

public class RoomType : IEntityId<int>
{
    public int Id { get; init; }

    public int PropertyId { get; init; }

    public string Name { get; set; }

    public decimal DailyPrice { get; set; }

    public Currency Currency { get; set; }

    public int MinPersonCount { get; set; }

    public int MaxPersonCount { get; set; }

    public List<RoomService> Services { get; set; }

    public List<Amenity> Amenities { get; set; }

    public int TotalRoomsCount { get; set; }

    public RoomType(
        int propertyId,
        string name,
        decimal dailyPrice,
        Currency currency,
        int minPersonCount,
        int maxPersonCount,
        int totalRoomsCount )
    {
        PropertyId = propertyId;
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        TotalRoomsCount = totalRoomsCount;
        Services = [];
        Amenities = [];
    }
}