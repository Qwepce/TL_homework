namespace CarFactory.Models.Engine;

public interface IEngine : IHasName
{
    public int HorsePower { get; }
}
