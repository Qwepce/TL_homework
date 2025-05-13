using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Properties.Commands;

namespace WebAPI.Application.UseCases.Properties.BaseValidator;

public abstract class BasePropertyCommandsValidator<TCommand> : IRequestValidator<TCommand>
    where TCommand : class, IPropertyCommand
{
    private readonly List<Error> _errors = [];

    protected int _maxStringLength = 100;

    private const int LowerLatitudeLimit = -90;
    private const int HigherLatitudeLimit = 90;

    private const int LowerLongitudeLimit = -180;
    private const int HigherLongitudeLimit = 180;

    public virtual Task<Result> Validate( TCommand command )
    {
        ApplyStringsCheck( new()
        {
            {"Property name", command.Name },
            {"Country name", command.Country },
            {"City name", command.City },
            {"Address", command.Address },
        } );

        ApplyCoordinatesCheck( command.Latitude, command.Longitude );

        if ( _errors.Any() )
        {
            return Task.FromResult( Result.Failure( _errors ) );
        }

        return Task.FromResult( Result.Success() );
    }

    protected virtual void ApplyStringsCheck( Dictionary<string, string> checks )
    {
        foreach ( KeyValuePair<string, string> check in checks )
        {
            if ( string.IsNullOrWhiteSpace( check.Value ) )
            {
                _errors.Add( new Error( $"{check.Key} is required" ) );
                continue;
            }
            if ( check.Value.Length > _maxStringLength )
            {
                _errors.Add( new Error( $"{check.Key} must be less or equal than {_maxStringLength} characters" ) );
            }
        }
    }

    protected virtual void ApplyCoordinatesCheck( decimal latitude, decimal longitude )
    {
        if ( latitude < LowerLatitudeLimit || latitude > HigherLatitudeLimit )
        {
            _errors.Add( new Error( "Invalid latitude value" ) );
        }

        if ( longitude < LowerLongitudeLimit || longitude > HigherLongitudeLimit )
        {
            _errors.Add( new Error( "Invalid longitude parameter" ) );
        }
    }
}