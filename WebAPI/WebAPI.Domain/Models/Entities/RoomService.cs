namespace WebAPI.Domain.Models.Entities;

public class RoomService : IEntityId
{
    public int Id { get; init; }

    public string Name { get; set; }

    public List<RoomType> RoomTypes { get; set; } = [];
}
