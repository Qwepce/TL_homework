namespace CarFactory.Models.Engine;

public interface IEngine : IHaveName
{
    public int HorsePower { get; }
}
