using CarFactory.Models.BodyShapes;
using CarFactory.Models.CarBody;
using CarFactory.Models.Colors;

namespace CarFactory.Models.IBody;

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
