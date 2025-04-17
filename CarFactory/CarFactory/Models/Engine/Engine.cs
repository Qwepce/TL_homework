namespace CarFactory.Models.Engine;

public class Engine : IEngine
{
    public string Name { get; }

    public int HorsePower { get; }

    public Engine( string engineName, int horsePower )
    {
        Name = engineName;
        HorsePower = horsePower;
    }
}
