using CarFactory.Models.BodyShape;
using CarFactory.Models.Color;

namespace CarFactory.Models.CarBody;

public class CarBody : ICarBody
{
    public IBodyShape CarBodyShape { get; }

    public ICarColor CarBodyColor { get; }

    public CarBody( IBodyShape bodyShape, ICarColor bodyColor )
    {
        CarBodyShape = bodyShape;
        CarBodyColor = bodyColor;
    }
}
