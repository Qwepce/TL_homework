namespace CarFactory.Models.WheelPosition;

public class WheelPosition : IWheelPosition
{
    public string Name { get; }

    public WheelPosition( string name )
    {
        Name = name;
    }
}
