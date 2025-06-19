import styles from './CurrencySelector.module.css';
import { useExchangeStore } from '../../stores/useExchangeStore';
import { Currency } from '../../types/types';
import { SelectorType } from '../../types/enums';

interface CurrencySelectorProps {
  selectorType: SelectorType;
}

const CurrencySelector = ({ selectorType }: CurrencySelectorProps) => {
  const currencies: Currency[] = useExchangeStore((state) => state.currencies);

  const selectedCurrency: Currency = useExchangeStore((state) =>
    selectorType === SelectorType.Incoming ? state.incomingCurrency : state.outcomingCurrency
  );

  const value: number = useExchangeStore((state) =>
    selectorType === SelectorType.Incoming ? state.incomingCurrencyAmount : state.outcomingCurrencyAmount
  );

  const setCurrency: (currency: Currency) => void = useExchangeStore((state) =>
    selectorType === SelectorType.Incoming ? state.setIncomingCurrency : state.setOutcomingCurrency
  );

  const setValue: (amount: number) => void = useExchangeStore((state) =>
    selectorType === SelectorType.Incoming ? state.setIncomingAmount : state.setOutcomingAmount
  );

  const handleValueChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const inputValue: string = e.target.value;
    if (inputValue === '') {
      setValue(0);
    } else {
      const newValue: number = parseFloat(inputValue);
      if (!isNaN(newValue)) {
        const roundedValue: number = Math.round(newValue * 100) / 100;
        setValue(roundedValue);
      }
    }
  };

  const handleCurrencySelect = (e: React.ChangeEvent<HTMLSelectElement>): void => {
    const selectedCode: string = e.target.value;
    const currency: Currency | undefined = currencies.find((c) => c.code === selectedCode);
    if (currency) {
      setCurrency(currency);
    }
  };

  return (
    <div className={styles.container}>
      <input
        type="number"
        min="0"
        id={`${selectorType}-currency-amount`}
        className={styles.currencyInput}
        value={value}
        onChange={handleValueChange}
      />
      <div className={styles.frontBorder}></div>
      <select
        className={styles.selector}
        value={selectedCurrency?.code || ''}
        onChange={handleCurrencySelect}
        id={`${selectorType}-currency`}
      >
        {currencies.map((currency) => (
          <option key={currency.code} value={currency.code}>
            {currency.code}
          </option>
        ))}
      </select>
    </div>
  );
};

export default CurrencySelector;