namespace WebAPI.Application.UseCases.Properties.Commands;

public interface IPropertyCommand
{
    public string Name { get; init; }

    public string Country { get; init; }

    public string City { get; init; }

    public string Address { get; init; }

    public decimal Latitude { get; init; }

    public decimal Longitude { get; init; }
}
