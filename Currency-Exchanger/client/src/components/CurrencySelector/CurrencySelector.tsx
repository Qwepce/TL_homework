import styles from './CurrencySelector.module.css';
import { useExchangeStore } from '../../stores/useExchangeStore';

interface CurrencySelectorProps {
  selectorType: 'incoming' | 'outcoming';
}

const CurrencySelector = ({ selectorType }: CurrencySelectorProps) => {
  const currencies = useExchangeStore((state) => state.currencies);

  const selectedCurrency = useExchangeStore((state) =>
    selectorType === 'incoming' ? state.incomingCurrency : state.outcomingCurrency
  );

  const value = useExchangeStore((state) =>
    selectorType === 'incoming' ? state.incomingCurrencyAmount : state.outcomingCurrencyAmount
  );

  const setCurrency = useExchangeStore((state) =>
    selectorType === 'incoming' ? state.setIncomingCurrency : state.setOutcomingCurrency
  );

  const setValue = useExchangeStore((state) =>
    selectorType === 'incoming' ? state.setIncomingAmount : state.setOutcomingAmount
  );

  const handleValueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const inputValue = e.target.value;
    if (inputValue === '') {
      setValue(0);
    } else {
      const newValue = parseFloat(inputValue);
      if (!isNaN(newValue)) {
        const roundedValue = Math.round(newValue * 100) / 100;
        setValue(roundedValue);
      }
    }
  };

  const handleCurrencySelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedCode = e.target.value;
    const currency = currencies.find((c) => c.code === selectedCode);
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