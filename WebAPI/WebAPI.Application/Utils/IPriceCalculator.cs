using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.Utils;

public interface IPriceCalculator
{
    decimal CalculateTotalPrice(
        int days,
        decimal baseRate,
        Currency roomTypeCurrency,
        Currency accommodationCurrency );
}
