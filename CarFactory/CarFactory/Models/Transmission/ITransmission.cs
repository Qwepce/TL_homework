using CarFactory.Enums;

namespace CarFactory.Models.Transmissions;

public interface ITransmission : IHasName
{
    int GearsCount { get; }

    double Efficiency { get; }

    CarDriveType DriveType { get; }
}
