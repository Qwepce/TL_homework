import styles from './ExchangeFilter.module.css';
import { useFiltersStore } from '../../stores/useFilterStore';
import { useExchangeStore } from '../../stores/useExchangeStore';
import { memo } from 'react';
import { Filter } from '../../types/types';

const ExchangeFilter = memo(() => {
  const { addFilter } = useFiltersStore();
  const { incomingCurrency, outcomingCurrency } = useExchangeStore();

  const handleSaveFilterClick = (): void => {
    const newFilter: Filter = {
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