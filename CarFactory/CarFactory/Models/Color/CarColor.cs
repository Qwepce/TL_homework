using CarFactory.Models.Colors;

namespace CarFactory.Models.Color;

public class CarColor : ICarColor
{
    public string Name { get; }

    public CarColor( string colorName )
    {
        Name = colorName;
    }
}
