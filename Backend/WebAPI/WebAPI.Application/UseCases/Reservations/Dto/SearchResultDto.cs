using WebAPI.Application.UseCases.RoomTypes.Dto;

namespace WebAPI.Application.UseCases.Reservations.Dto;

public class SearchResultDto
{
    public string Name { get; init; }

    public string Country { get; init; }

    public string City { get; init; }

    public string Address { get; init; }

    public IReadOnlyCollection<RoomTypeDto> RoomTypes { get; init; } = [];
}
