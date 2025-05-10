using System.Linq.Expressions;
using FluentValidation;
using WebAPI.Application.UseCases.RoomTypes.Commands;
using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.Validation.RoomTypes;

public abstract class BaseRoomTypeValidator<T> : AbstractValidator<T>
    where T : class, IRoomTypeCommand
{
    protected BaseRoomTypeValidator()
    {
        RuleFor( rt => rt.Name )
            .Cascade( CascadeMode.Stop )
            .Must( x => !string.IsNullOrWhiteSpace( x ) )
            .WithMessage( "Name is required" )
            .MaximumLength( 30 )
            .WithMessage( "Room type name length should be less or equal than 30 characters" );

        RuleFor( rt => rt.Currency )
            .Must( currency => Enum.TryParse( typeof( Currency ), currency, ignoreCase: true, out object? _ ) )
            .WithMessage( "Invalid currency" );

        RuleFor( rt => rt.MinPersonCount )
            .LessThanOrEqualTo( rt => rt.MaxPersonCount )
            .WithMessage( "Minimum person count must be less or equal maximum person count" );

        ValidateStringCollection( rt => rt.RoomAmenities, "room amenity", 50 );
        ValidateStringCollection( rt => rt.RoomServices, "room service", 50 );
        ApplyNumbersRules();
    }

    private void ApplyNumbersRules()
    {
        RuleForNumbers( rt => ( int )rt.DailyPrice, "Daily price" );
        RuleForNumbers( rt => rt.MinPersonCount, "Minimum persons count" );
        RuleForNumbers( rt => rt.MaxPersonCount, "Maximum persons count" );
        RuleForNumbers( rt => rt.TotalRoomsCount, "Available rooms count" );
    }

    private IRuleBuilderOptions<T, int> RuleForNumbers(
        Expression<Func<T, int>> fieldSelector,
        string fieldName )
    {
        return RuleFor( fieldSelector )
            .GreaterThan( 0 )
            .WithMessage( $"{fieldName} should be more than zero" );
    }

    private void ValidateStringCollection(
        Expression<Func<T, IEnumerable<string>>> propertySelector,
        string itemName,
        int maxLength )
    {
        RuleFor( propertySelector )
            .Must( items => items.All( item => !string.IsNullOrWhiteSpace( item ) ) )
            .WithMessage( $"Unable to set {itemName} with empty name" )
            .Must( items => items.All( item => item.Length <= maxLength ) )
            .WithMessage( $"Max {itemName} name length is {maxLength} characters" );
    }
}
