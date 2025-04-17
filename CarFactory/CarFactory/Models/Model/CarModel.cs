namespace CarFactory.Models.Model;

public class CarModel : IModel
{
    public string Name { get; }

    public CarModel( string name )
    {
        Name = name;
    }
}
