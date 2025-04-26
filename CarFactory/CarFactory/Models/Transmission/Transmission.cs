using CarFactory.Enums;

namespace CarFactory.Models.Transmission;

public class Transmission : ITransmission
{
    public string Name { get; }

    public int GearsCount { get; }

    public double Efficiency { get; }

    public CarDriveType DriveType { get; }

    public Transmission(
        string transmissionName,
        int gearsCount,
        double efficiency,
        CarDriveType driveType )
    {
        Name = transmissionName;
        GearsCount = gearsCount;
        Efficiency = efficiency;
        DriveType = driveType;
    }
}
