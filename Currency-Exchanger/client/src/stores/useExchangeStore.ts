import { create } from 'zustand';
import type { Currency, ExchangeRate } from '../types/types';
import CurrencyService from '../api/CurrencyService';

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

  getCurrencies: () => Promise<void>;
  getExchangeRates: () => Promise<void>;
}

const currencyService = new CurrencyService();

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

  getCurrencies: async () => {
    set({ loading: true, error: null });
    try {
      const data = await currencyService.fetchCurrencies();

      set({
        currencies: data,
        incomingCurrency: data[0],
        outcomingCurrency: data[1],
        loading: false
      });

      await get().getExchangeRates();
    } catch (err) {
      set({ error: 'Could not get data from the server', loading: false });
    }
  },

  setIncomingCurrency: (currency) => {
    set({ incomingCurrency: currency });
    get().getExchangeRates();
  },

  setOutcomingCurrency: (currency) => {
    set({ outcomingCurrency: currency });
    get().getExchangeRates();
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

  getExchangeRates: async () => {
    const { incomingCurrency, outcomingCurrency } = get();

    if (!incomingCurrency?.code || !outcomingCurrency?.code) {
      return;
    }

    try {
      const data = await currencyService.fetchExchangeData(incomingCurrency.code, outcomingCurrency.code);

      if (data && data.length > 0) {
        const rate = data[data.length - 1].price;
        set({
          exchangeData: data,
          exchangeRate: rate,
          outcomingCurrencyAmount: get().incomingCurrencyAmount * rate
        });
      }
    } catch (err) {
      set({ error: 'Could not get exchange rates from the server', loading: false });
    }
  }
}));
