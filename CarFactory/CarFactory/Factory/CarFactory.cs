using CarFactory.ConsoleInputReader;
using CarFactory.Models;
using CarFactory.Models.BodyShapes;
using CarFactory.Models.Brand;
using CarFactory.Models.Car;
using CarFactory.Models.CarBody;
using CarFactory.Models.Colors;
using CarFactory.Models.Engine;
using CarFactory.Models.IBody;
using CarFactory.Models.Model;
using CarFactory.Models.Transmissions;
using CarFactory.Models.WheelPosition;
using CarFactory.Store;
using CarFactory.Utils;

namespace CarFactory.Factory;

public class CarFactory : ICarFactory
{
    private readonly IInputReader _inputReader;

    public CarFactory( IInputReader inputReader )
    {
        _inputReader = inputReader;
    }

    public ICar CreateCar()
    {
        IBrand carBrand = SelectAvailableOptions(
            CarOptionsStore.Brands,
            Messages.AskUserChooseCarBrand );

        IModel carModel = SelectAvailableOptions(
            carBrand.GetBrandModels(),
            Messages.AskUserChooseCarModel );

        IEngine carEngine = SelectAvailableOptions(
            CarOptionsStore.Engines,
            Messages.AskUserChooseEngine );

        ITransmission carTransmission = SelectAvailableOptions(
            CarOptionsStore.Transmissions,
            Messages.AskUserChooseTransmission );

        IWheelPosition steeringWheelPosition = SelectAvailableOptions(
            CarOptionsStore.SteeringWheelPositions,
            Messages.AskUserChoiceSteeringWheelPosition
            );

        ICarColor carColor = SelectAvailableOptions(
            CarOptionsStore.Colors,
            Messages.AskUserChooseCarColor );


        IBodyShape carBodyShape = SelectAvailableOptions(
            CarOptionsStore.BodyShapes,
            Messages.AskUserChooseBodyShape );

        ICarBody carBody = new CarBody( carBodyShape, carColor );

        ICar newCar = new Car(
            carBrand,
            carModel,
            carEngine,
            carTransmission,
            steeringWheelPosition,
            carBody );

        return newCar;
    }

    private T SelectAvailableOptions<T>(
        IReadOnlyDictionary<int, T> options,
        string optionsMessage ) where T : IHasName
    {
        int userOptionChoice;

        Console.WriteLine( optionsMessage );

        foreach ( KeyValuePair<int, T> pair in options )
        {
            Console.WriteLine( $"{pair.Key}. {pair.Value.Name}" );
        }

        Console.Write( Messages.AskUserSelectOption );

        while ( true )
        {
            userOptionChoice = _inputReader.GetValidUserOption();

            if ( options.ContainsKey( userOptionChoice ) )
            {
                break;
            }

            Console.Write( Messages.UnknownOption );
        }

        return options[ userOptionChoice ];
    }
}
