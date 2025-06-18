using server.Entities;
using WebApi.Models.Currencies;
using WebApi.Models.Currency;

namespace server.Services
{
    public interface ICurrencyService
    {
        IEnumerable<Currency> GetAll();
        Currency GetByCode( string code );
        IEnumerable<PriceChange> GetPriceChanges( GetPricesRequest model );
    }
}
