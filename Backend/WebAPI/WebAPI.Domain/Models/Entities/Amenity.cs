namespace WebAPI.Domain.Models.Entities;

public class Amenity : IEntityId
{
    public int Id { get; init; }

    public string Name { get; set; }

    public List<RoomType> RoomTypes { get; set; } = [];
}
