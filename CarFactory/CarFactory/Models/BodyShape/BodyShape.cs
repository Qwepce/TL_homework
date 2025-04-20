using CarFactory.Models.BodyShapes;

namespace CarFactory.Models.BodyShape;

public class BodyShape : IBodyShape
{
    public string Name { get; }

    public double DragCoefficient { get; }

    public int SeatsCount { get; }

    public BodyShape(
        string bodyShapeName,
        double dragCoefficient,
        int seatsCount )
    {
        Name = bodyShapeName;
        DragCoefficient = dragCoefficient;
        SeatsCount = seatsCount;
    }
}
