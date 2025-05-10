using WebAPI.Domain.Models.Enums;

namespace WebAPI.Application.Utils;

public class PriceCalculator : IPriceCalculator
{
    public decimal CalculateTotalPrice(
        int days,
        decimal baseRate,
        Currency roomTypeCurrency,
        Currency accommodationCurrency )
    {
        decimal roomTypeTotalPrice = baseRate * days;
        decimal exchangeRate = GetExchangeRate( roomTypeCurrency, accommodationCurrency );
        decimal totalPrice = roomTypeTotalPrice * exchangeRate;

        return totalPrice;
    }

    private static decimal GetExchangeRate( Currency roomTypeCurrency, Currency accommodationCurrency )
    {
        Dictionary<Currency, decimal> rates = new()
        {
            [ Currency.RUB ] = 1m,
            [ Currency.USD ] = 80m,
            [ Currency.EUR ] = 90m,
            [ Currency.CNY ] = 12m,
        };

        decimal roomTypeCurrencyRate = rates[ roomTypeCurrency ];
        decimal reservationCurrencyRate = rates[ accommodationCurrency ];

        return roomTypeCurrencyRate / reservationCurrencyRate;
    }
}
