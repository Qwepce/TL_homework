namespace WebAPI.Domain.Models.Entities;

public class Property : IEntityId
{
    public int Id { get; init; }

    public string Name { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Address { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public Property(
        string name,
        string country,
        string city,
        string address,
        decimal latitude,
        decimal longitude )
    {
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }
}