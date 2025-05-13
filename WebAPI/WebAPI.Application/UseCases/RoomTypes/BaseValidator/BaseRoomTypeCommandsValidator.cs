using WebAPI.Application.Interfaces.CQRS.ValidatorInterface;
using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.RoomTypes.Commands;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.UseCases.RoomTypes.BaseValidator;

public abstract class BaseRoomTypeCommandsValidator<TCommand> : IRequestValidator<TCommand>
    where TCommand : class, IRoomTypeCommand
{
    private readonly List<Error> _errors = [];

    public virtual Task<Result> Validate( TCommand command )
    {
        ApplyNameCheck( command.Name );

        ApplyNumbersChecks( new()
        {
            { "Daily price", (int)command.DailyPrice },
            { "Minimum persons", command.MinPersonCount },
            { "Maximum persons", command.MaxPersonCount },
            { "Total rooms", command.TotalRoomsCount }
        } );

        if ( !Enum.TryParse( typeof( Currency ), command.Currency, ignoreCase: true, out object _ ) )
        {
            _errors.Add( new Error( "Invalid currency" ) );
        }

        if ( command.MinPersonCount > command.MaxPersonCount )
        {
            _errors.Add( new Error( "Minimum persons count must be less or equal than maximum persons count" ) );
        }

        if ( _errors.Count != 0 )
        {
            return Task.FromResult( Result.Failure( _errors ) );
        }

        return Task.FromResult( Result.Success() );
    }

    protected virtual void ApplyNameCheck( string name )
    {
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            _errors.Add( new Error( "Name is required" ) );
            return;
        }

        if ( name.Length > 30 )
        {
            _errors.Add( new Error( "Room type name should be less or equal 30 charcters" ) );
        }
    }

    protected virtual void ApplyNumbersChecks( Dictionary<string, int> checks )
    {
        foreach ( KeyValuePair<string, int> kvp in checks )
        {
            if ( kvp.Value <= 0 )
            {
                _errors.Add( new Error( $"{kvp.Key} must be more than zero" ) );
            }
        }
    }
}
