import { GET_CURRENCIES_URI, GET_EXCHANGE_RATE_BASE_URI } from '../config/constants';
import dayjs from 'dayjs';
import { Currency, ExchangeRate } from '../types/types';

class CurrencyService {
  private readonly currenciesUri: string;
  private readonly exchangeRateBaseUri: string;

  constructor(
    currenciesUri: string = GET_CURRENCIES_URI,
    exchangeRateBaseUri: string = GET_EXCHANGE_RATE_BASE_URI
  ) {
    this.currenciesUri = currenciesUri;
    this.exchangeRateBaseUri = exchangeRateBaseUri;
  }

  public async fetchCurrencies(): Promise<Currency[]> {
    const response: Response = await fetch(this.currenciesUri);
    if (!response.ok) {
      throw new Error('Failed to fetch currencies');
    }
    return response.json();
  }

  public async fetchExchangeData(
    incomingCode: string,
    outcomingCode: string
  ): Promise<ExchangeRate[]> {
    const now: dayjs.Dayjs = dayjs();
    const formattedDate: string = now.format('YYYY-MM-DD');

    const apiUrl: string = `${this.exchangeRateBaseUri}PaymentCurrency=${outcomingCode}&PurchasedCurrency=${incomingCode}&FromDateTime=${formattedDate}`;
    const response: Response = await fetch(apiUrl);

    if (!response.ok) {
      throw new Error('Failed to fetch exchange data');
    }

    return response.json();
  }
}

export default CurrencyService;