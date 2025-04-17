using CarFactory.Models.BodyShapes;
using CarFactory.Models.Colors;

namespace CarFactory.Models.CarBody;

public interface ICarBody
{
    public IBodyShape CarBodyShape { get; }

    public ICarColor CarBodyColor { get; }
}
