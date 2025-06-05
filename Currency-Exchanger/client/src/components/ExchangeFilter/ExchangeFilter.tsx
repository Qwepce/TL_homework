import styles from './ExchangeFilter.module.css';
import { useFiltersStore } from '../../stores/useFilterStore';
import { useExchangeStore } from '../../stores/useExchangeStore';
import { memo } from 'react';

const ExchangeFilter = memo(() => {
  const { addFilter } = useFiltersStore();
  const { incomingCurrency, outcomingCurrency } = useExchangeStore();

  const handleSaveFilterClick = () => {
    const newFilter = {
      name: `${incomingCurrency.code} / ${outcomingCurrency.code}`,
      incomingCurrencyCode: incomingCurrency.code,
      outcomingCurrencyCode: outcomingCurrency.code,
    };

    addFilter(newFilter);
  };

  return (
    <button className={styles.commonBtn} type="button" onClick={handleSaveFilterClick}>
      + Save Filter
    </button>
  );
});

export default ExchangeFilter;