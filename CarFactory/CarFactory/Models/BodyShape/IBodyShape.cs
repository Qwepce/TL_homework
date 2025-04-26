namespace CarFactory.Models.BodyShapes;

public interface IBodyShape : IHasName
{
    public double DragCoefficient { get; }

    public int SeatsCount { get; }
}
