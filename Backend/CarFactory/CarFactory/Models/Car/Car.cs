using CarFactory.Models.Brand;
using CarFactory.Models.CarBody;
using CarFactory.Models.Engine;
using CarFactory.Models.Model;
using CarFactory.Models.Transmission;
using CarFactory.Models.WheelPosition;

namespace CarFactory.Models.Car;

public class Car : ICar
{
    public IBrand CarBrand { get; init; }
    public IModel CarModel { get; init; }
    public IEngine CarEngine { get; init; }
    public ITransmission CarTransmission { get; init; }
    public IWheelPosition SteeringWheelPosition { get; init; }
    public ICarBody CarBody { get; init; }

    public Car(
        IBrand carBrand,
        IModel carModel,
        IEngine carEngine,
        ITransmission carTransmission,
        IWheelPosition steeringWheelPosition,
        ICarBody carBody )
    {
        CarBrand = carBrand;
        CarModel = carModel;
        CarEngine = carEngine;
        CarTransmission = carTransmission;
        SteeringWheelPosition = steeringWheelPosition;
        CarBody = carBody;
    }

    public int GetMaxSpeed()
    {
        int maxSpeed = ( int )(
                CarEngine.HorsePower * 0.7 *
                CarTransmission.Efficiency *
                ( 1 - CarBody.CarBodyShape.DragCoefficient * 0.5 )
            );

        return maxSpeed;
    }

    public override string ToString()
    {
        return $"""
            Марка автомобиля - {CarBrand.Name}
            Модель автомобиля - {CarModel.Name}
            Кузов - {CarBody.CarBodyShape.Name}
            Цвет - {CarBody.CarBodyColor.Name}
            Положение руля - {SteeringWheelPosition.Name}
            Двигатель - {CarEngine.Name}
            Количество лошадиных сил - {CarEngine.HorsePower}
            Коробка передач - {CarTransmission.Name}
            Количество передач - {CarTransmission.GearsCount}
            Тип привода - {CarTransmission.DriveType.ToString()}
            Максимальная скорость - {GetMaxSpeed()} км/ч

            """;
    }
}
