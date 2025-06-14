using Microsoft.AspNetCore.Mvc;
using server.Services;
using WebApi.Models.Currency;

namespace server.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class CurrencyController : ControllerBase
    {
        private ICurrencyService _currencyService;
        public CurrencyController( ICurrencyService currencyService )
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _currencyService.GetAll();
            return Ok( result );
        }

        [HttpGet]
        [Route( "{code}" )]
        public IActionResult GetByCode( string code )
        {
            var result = _currencyService.GetByCode( code );
            return Ok( result );
        }

        [HttpGet]
        [Route( "/prices" )]
        public IActionResult GetPriceChanges( [FromQuery] GetPricesRequest model )
        {
            var result = _currencyService.GetPriceChanges( model );
            return Ok( result );
        }
    }
}
