using System.Linq.Expressions;
using FluentValidation;
using WebAPI.Application.UseCases.Reservations.Commands.Create;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.Validation.Reservations;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        ApplyStringRules();

        RuleFor( x => x.GuestsCount )
            .GreaterThan( 0 )
            .WithMessage( "Guests count cannot be lower than 1" );

        RuleFor( x => x.ArrivalDate )
            .GreaterThan( DateOnly.FromDateTime( DateTime.UtcNow ) )
            .WithMessage( "Arrival date must be in the future" );

        RuleFor( x => x.DepartureDate )
            .GreaterThan( x => x.ArrivalDate )
            .WithMessage( "Departure date must be after arrival date" );

        RuleFor( x => x.ReservationCurrency )
            .Must( currency => Enum.TryParse( typeof( Currency ), currency, ignoreCase: true, out _ ) )
            .WithMessage( "Invalid currency" );
    }

    private void ApplyStringRules()
    {
        RuleForStrings( r => r.GuestName, "Guest name", 100 );
        RuleForStrings( r => r.GuestPhoneNumber, "Guest phone number length", 20 );
    }

    private IRuleBuilderOptions<CreateReservationCommand, string> RuleForStrings(
        Expression<Func<CreateReservationCommand, string>> propertySelector,
        string fieldName, int stringLength )
    {
        return RuleFor( propertySelector )
            .Cascade( CascadeMode.Stop )
            .Must( x => !string.IsNullOrWhiteSpace( x ) )
            .WithMessage( $"{fieldName} is required" )
            .MaximumLength( stringLength )
            .WithMessage( $"{fieldName} must be less or equal than 100 characters" );
    }
}