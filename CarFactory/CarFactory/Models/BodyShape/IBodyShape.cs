namespace CarFactory.Models.BodyShapes;

public interface IBodyShape : IHaveName
{
    public double DragCoefficient { get; }

    public int SeatsCount { get; }
}
