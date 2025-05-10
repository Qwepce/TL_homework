namespace WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommand : IPropertyCommand
{
    public int PropertyId { get; set; }

    public string Name { get; init; } = string.Empty;

    public string Country { get; init; } = string.Empty;

    public string City { get; init; } = string.Empty;

    public string Address { get; init; } = string.Empty;

    public decimal Latitude { get; init; }

    public decimal Longitude { get; init; }
}