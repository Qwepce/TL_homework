using CarFactory.Models.Car;

namespace CarFactory.Factory;

public interface ICarsFactory
{
    ICar CreateCar();
}
