using System.Linq.Expressions;
using FluentValidation;
using WebAPI.Application.UseCases.Properties.Commands;

namespace WebAPI.Application.Validation.Properties;

public abstract class BasePropertyValidator<T> : AbstractValidator<T>
    where T : class, IPropertyCommand
{
    public BasePropertyValidator()
    {
        ApplyStringRules();

        RuleFor( p => p.Latitude )
            .InclusiveBetween( -90m, 90m )
            .WithMessage( "Invalid latitude parameter" );

        RuleFor( p => p.Longitude )
            .InclusiveBetween( -180m, 180m )
            .WithMessage( "Invalid longitude parameter" );
    }

    private void ApplyStringRules()
    {
        RuleForStrings( p => p.Name, "Name" );
        RuleForStrings( p => p.Country, "Country" );
        RuleForStrings( p => p.City, "City" );
        RuleForStrings( p => p.Address, "Address" );
    }

    private IRuleBuilderOptions<T, string> RuleForStrings(
        Expression<Func<T, string>> propertySelector,
        string fieldName )
    {
        return RuleFor( propertySelector )
            .Cascade( CascadeMode.Stop )
            .Must( x => !string.IsNullOrWhiteSpace( x ) )
            .WithMessage( $"{fieldName} is required" )
            .MaximumLength( 100 )
            .WithMessage( $"{fieldName} must be less or equal than 100 characters" );
    }
}