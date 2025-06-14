import { GET_CURRENCIES_URI, GET_EXCHANGE_RATE_BASE_URI } from '../config/config';
import dayjs from 'dayjs';
import { Currency, ExchangeRate } from '../types/types';

export const fetchCurrencies = async (): Promise<Currency[]> => {
  const response = await fetch(GET_CURRENCIES_URI);
  if (!response.ok) {
    throw new Error('Failed to fetch currencies');
  }
  return response.json();
};

export const fetchExchangeData = async (incomingCode: string, outcomingCode: string): Promise<ExchangeRate[]> => {
  const now = dayjs();
  const formattedDate = now.format('YYYY-MM-DD');

  const apiUrl = `${GET_EXCHANGE_RATE_BASE_URI}PaymentCurrency=${outcomingCode}&PurchasedCurrency=${incomingCode}&FromDateTime=${formattedDate}`;
  const response = await fetch(apiUrl);

  if (!response.ok) {
    throw new Error('Failed to fetch exchange data');
  }

  return response.json();
};
