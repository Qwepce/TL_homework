using System.ComponentModel.DataAnnotations;

namespace WebAPI.Web.CustomAttributes;

public class DecimalRangeAttribute : ValidationAttribute
{
    private readonly decimal _min;
    private readonly decimal _max;

    public DecimalRangeAttribute( double min, double max )
    {
        _min = ( decimal )min;
        _max = ( decimal )max;
    }

    protected override ValidationResult? IsValid( object? value, ValidationContext validationContext )
    {
        if ( value is decimal decimalValue )
        {
            if ( decimalValue < _min || decimalValue > _max )
            {
                return new ValidationResult( ErrorMessage ?? $"{validationContext.DisplayName} must be between {_min} and {_max}" );
            }
        }

        return ValidationResult.Success;
    }
}
