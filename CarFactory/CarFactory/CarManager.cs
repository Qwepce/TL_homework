using CarFactory.Enums;
using CarFactory.Factory;
using CarFactory.Models.Car;
using CarFactory.Utils;
using CarFactory.Utils.UserInputReader;

namespace CarFactory;

public class CarManager : ICarManager
{
    private readonly IInputReader _inputReader;
    private readonly ICarFactory _carFactory;

    private List<ICar> _cars = [];

    public CarManager( IInputReader inputReader, ICarFactory carFactory )
    {
        _inputReader = inputReader;
        _carFactory = carFactory;
    }

    public void Run()
    {
        Console.WriteLine( Messages.AvailableCommandsMessage );

        while ( true )
        {
            UserCommand userCommand = _inputReader.GetValidUserCommand();

            if ( userCommand.Equals( UserCommand.Exit ) )
            {
                break;
            }

            ProcessCommand( userCommand );
        }
    }

    private void ProcessCommand( UserCommand command )
    {
        switch ( command )
        {
            case UserCommand.Add:
                AddNewCarConfiguration();
                Console.WriteLine( Messages.NewCarAddedSuccessfully );
                break;

            case UserCommand.List:
                PrintAllCars();
                break;

            case UserCommand.Clear:
                Console.Clear();
                Console.WriteLine( Messages.AvailableCommandsMessage );
                break;

            default:
                Console.WriteLine( Messages.UnknownCommandSelected );
                break;
        }
    }

    private void AddNewCarConfiguration()
    {
        ICar newCar = _carFactory.CreateCar();
        _cars.Add( newCar );
    }

    private void PrintAllCars()
    {
        if ( _cars.Count == 0 )
        {
            Console.WriteLine( Messages.CarsListIsEmpty );
        }
        else
        {
            Console.WriteLine( "Список автомобилей:\n" );
            for ( int i = 0; i < _cars.Count; i++ )
            {
                Console.WriteLine( $"Машина #{i + 1}" );
                Console.WriteLine( _cars[ i ] );
            }
        }
    }
}
