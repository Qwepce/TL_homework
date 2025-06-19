using server.Entities;
using WebApi.Helpers;
using WebApi.Models.Currencies;
using WebApi.Models.Currency;

namespace server.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly DataContext _context;

        public CurrencyService( DataContext context )
        {
            _context = context;
        }

        public IEnumerable<Currency> GetAll()
        {
            return _context.Currencies;
        }

        public Currency GetByCode( string code )
        {
            var currency = _context.Currencies.Find( code );
            if ( currency == null )
                throw new KeyNotFoundException( "Currency not found" );

            return currency;
        }

        // TODO
        public IEnumerable<PriceChange> GetPriceChanges( GetPricesRequest model )
        {
            string purchasedCurrency = model.PurchasedCurrency;
            string paymentCurrency = model.PaymentCurrency;

            var currencyCodes = _context.Currencies.Select( c => c.Code );
            if ( !currencyCodes.Contains( purchasedCurrency ) )
            {
                throw new AppException( $"Unknown currency {purchasedCurrency}" );
            }

            if ( !currencyCodes.Contains( paymentCurrency ) )
            {
                throw new AppException( $"Unknown currency {paymentCurrency}" );
            }

            var result = _context.CurrencyPrices
                .Where( c => ( c.CurrencyCode == purchasedCurrency || c.CurrencyCode == paymentCurrency ) &&
                    c.DateTime >= model.FromDateTime &&
                    ( model.ToDateTime == null || c.DateTime <= model.ToDateTime ) )
                .OrderBy( c => c.DateTime )
                .ToList()
                .GroupBy( c => c.DateTime )
                .Select( ( IGrouping<DateTime, CurrencyPrice> g ) =>
                {
                    var purchased = g.FirstOrDefault( item => item.CurrencyCode == purchasedCurrency );
                    var payment = g.FirstOrDefault( item => item.CurrencyCode == paymentCurrency );

                    if ( purchased == null || payment == null )
                    {
                        throw new AppException( $"Grouping should contain both currencies, there are no currencies payment = {payment}, purchased = {purchased} for date {g.Key}" );
                    }

                    return new PriceChange
                    {
                        DateTime = g.Key,
                        PaymentCurrencyCode = payment.CurrencyCode,
                        PurchasedCurrencyCode = purchased.CurrencyCode,
                        Price = decimal.Round( purchased.Price / payment.Price, 3, MidpointRounding.ToPositiveInfinity )
                    };
                } );

            return result;
        }
    }
}
