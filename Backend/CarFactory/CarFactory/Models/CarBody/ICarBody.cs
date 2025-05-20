using CarFactory.Models.BodyShape;
using CarFactory.Models.Color;

namespace CarFactory.Models.CarBody;

public interface ICarBody
{
    public IBodyShape CarBodyShape { get; }

    public ICarColor CarBodyColor { get; }
}
