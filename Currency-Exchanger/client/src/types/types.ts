export type Filter = {
  name: string;
  incomingCurrencyCode: string;
  outcomingCurrencyCode: string;
};

export type ExchangeRate = {
  purchasedCurrencyCode: string;
  paymentCurrencyCode: string;
  price: number;
  dateTime: Date;
}

export type Currency = {
  code: string;
  name: string;
  description: string;
  symbol: string;
};