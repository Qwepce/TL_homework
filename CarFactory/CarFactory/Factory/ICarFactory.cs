using CarFactory.Models.Car;

namespace CarFactory.Factory;

public interface ICarFactory
{
    ICar CreateCar();
}
