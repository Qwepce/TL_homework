import { create } from 'zustand';
import type { Currency, ExchangeRate } from '../types/types';
import dayjs from 'dayjs';

interface ExchangeState {
  currencies: Currency[];
  loading: boolean;
  error: string | null;

  incomingCurrency: Currency;
  outcomingCurrency: Currency;
  incomingCurrencyAmount: number;
  outcomingCurrencyAmount: number;
  exchangeRate: number;
  exchangeData: ExchangeRate[];

  setIncomingCurrency: (currency: Currency) => void;
  setOutcomingCurrency: (currency: Currency) => void;
  setIncomingAmount: (amount: number) => void;
  setOutcomingAmount: (amount: number) => void;

  fetchCurrencies: () => Promise<void>;
  fetchExchangeData: () => Promise<void>;
}

const GET_CURRENCIES_URI = 'http://localhost:5081/Currency';
const GET_EXCHANGE_RATE_BASE_URI = 'http://localhost:5081/prices?';

export const useExchangeStore = create<ExchangeState>((set, get) => ({
  currencies: [],
  loading: false,
  error: null,
  incomingCurrency: { code: '', name: '', description: '', symbol: '' },
  outcomingCurrency: { code: '', name: '', description: '', symbol: '' },
  incomingCurrencyAmount: 1,
  outcomingCurrencyAmount: 1,
  exchangeRate: 1,
  exchangeData: [],

  fetchCurrencies: async () => {
    set({ loading: true, error: null });
    try {
      const response = await fetch(GET_CURRENCIES_URI);
      const data = await response.json();

      set({
        currencies: data,
        incomingCurrency: data[0],
        outcomingCurrency: data[1],
        loading: false
      });

      await get().fetchExchangeData();
    } catch (err) {
      set({ error: 'Could not get data from the server', loading: false });
    }
  },

  setIncomingCurrency: (currency) => {
    set({ incomingCurrency: currency });
    get().fetchExchangeData();
  },

  setOutcomingCurrency: (currency) => {
    set({ outcomingCurrency: currency });
    get().fetchExchangeData();
  },

  setIncomingAmount: (amount) => {
    const { exchangeRate } = get();
    set({
      incomingCurrencyAmount: amount,
      outcomingCurrencyAmount: exchangeRate ? amount * exchangeRate : 1
    });
  },

  setOutcomingAmount: (amount) => {
    const { exchangeRate } = get();
    set({
      outcomingCurrencyAmount: amount,
      incomingCurrencyAmount: exchangeRate ? amount / exchangeRate : 1
    });
  },

  fetchExchangeData: async () => {
    const { incomingCurrency, outcomingCurrency } = get();

    if (!incomingCurrency?.code || !outcomingCurrency?.code) {
      return;
    }

    const now = dayjs();
    const formattedDate = now.format(`DD-MM-YYYY HH:mm:ss`).replace(/ /g, `%20`).replace(/:/g, `%3A`);

    const apiUrl = `${GET_EXCHANGE_RATE_BASE_URI}PaymentCurrency=${outcomingCurrency.code}&PurchasedCurrency=${incomingCurrency.code}&FromDateTime=${formattedDate}`;

    try {
      const response = await fetch(apiUrl);
      const data = await response.json();

      if (data && data.length > 0) {
        const rate = data[data.length - 1].price;
        set({
          exchangeData: data,
          exchangeRate: rate,
          outcomingCurrencyAmount: get().incomingCurrencyAmount * rate
        });
      }
    } catch (err) {
      console.error('Could not get exchange rate data from the server', err);
    }
  }
}));
