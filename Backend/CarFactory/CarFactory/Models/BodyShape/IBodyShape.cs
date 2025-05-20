namespace CarFactory.Models.BodyShape;

public interface IBodyShape : IHasName
{
    public double DragCoefficient { get; }

    public int SeatsCount { get; }
}
