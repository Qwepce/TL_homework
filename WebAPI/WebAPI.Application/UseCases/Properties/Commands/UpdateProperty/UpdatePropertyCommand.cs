namespace WebAPI.Application.UseCases.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommand : IPropertyCommand
{
    public int PropertyId { get; set; }

    public string Name { get; init; }

    public string Country { get; init; }

    public string City { get; init; }

    public string Address { get; init; }

    public decimal Latitude { get; init; }

    public decimal Longitude { get; init; }
}