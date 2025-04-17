using System.Collections.Frozen;
using CarFactory.Enums;
using CarFactory.Models.BodyShape;
using CarFactory.Models.BodyShapes;
using CarFactory.Models.Brand;
using CarFactory.Models.Color;
using CarFactory.Models.Colors;
using CarFactory.Models.Engine;
using CarFactory.Models.Transmission;
using CarFactory.Models.Transmissions;
using CarFactory.Models.WheelPosition;

namespace CarFactory.Store;

public static class CarOptionsStore
{
    public static readonly IReadOnlyDictionary<int, IEngine> Engines =
        new Dictionary<int, IEngine>()
        {
            { 1, new Engine("VW EA888 2.0 TSI", horsePower: 310) },
            { 2, new Engine("Toyota 2JZ-GTE", horsePower: 330) },
            { 3, new Engine("Tesla Plaid Tri-Motor", horsePower: 1020) }
        }.ToFrozenDictionary();

    public static readonly IReadOnlyDictionary<int, ITransmission> Transmissions =
        new Dictionary<int, ITransmission>()
        {
            { 1, new Transmission("Toyota 6MT", gearsCount: 6, efficiency: 0.95, CarDriveType.RWD) },
            { 2, new Transmission("ZF 8HP Automatic", gearsCount: 8, efficiency: 0.85, CarDriveType.RWD) },
            { 3, new Transmission("Porsche PDK", gearsCount: 7, efficiency: 0.80, CarDriveType.AWD) },
            { 4, new Transmission("Jatco CVT", gearsCount: 0, efficiency: 0.80, CarDriveType.FWD) }
        }.ToFrozenDictionary();

    public static readonly IReadOnlyDictionary<int, IWheelPosition> SteeringWheelPositions =
        new Dictionary<int, IWheelPosition>()
        {
            { 1, new WheelPosition("Левое положение руля") },
            { 2, new WheelPosition("Правое положение руля") }
        }.ToFrozenDictionary();

    public static readonly IReadOnlyDictionary<int, IBodyShape> BodyShapes =
        new Dictionary<int, IBodyShape>()
        {
            { 1, new BodyShape("Седан", dragCoefficient: 0.30, seatsCount: 5) },
            { 2, new BodyShape("Внедорожник", dragCoefficient: 0.38, seatsCount: 5) },
            { 3, new BodyShape("Купе", dragCoefficient: 0.28, seatsCount: 2) },
            { 4, new BodyShape("Хэтчбэк", dragCoefficient: 0.32, seatsCount: 5) }
        }.ToFrozenDictionary();

    public static readonly IReadOnlyDictionary<int, IBrand> Brands =
        new Dictionary<int, IBrand>()
        {
            { 1, new Audi() },
            { 2, new Mercedes() },
            { 3, new BMW() },
            { 4, new Opel() }
        }.ToFrozenDictionary();

    public static readonly IReadOnlyDictionary<int, ICarColor> Colors =
        new Dictionary<int, ICarColor>()
        {
            { 1, new CarColor("Огненный красный") },
            { 2, new CarColor("Черный") },
            { 3, new CarColor("Ледниковый белый") },
            { 4, new CarColor("Серебряный") },
            { 5, new CarColor("Полуночный синий") }
        }.ToFrozenDictionary();
}
