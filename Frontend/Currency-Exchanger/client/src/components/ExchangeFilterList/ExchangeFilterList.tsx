import styles from '../ExchangeFilter/ExchangeFilter.module.css';
import { useFiltersStore } from '../../stores/useFilterStore';
import { useExchangeStore } from '../../stores/useExchangeStore';
import { Currency, Filter } from '../../types/types';

const ExchangeFilterList = () => {
  const { filters, activeFilter, setActiveFilter, clearFilters } = useFiltersStore();
  const { currencies, setIncomingCurrency, setOutcomingCurrency } = useExchangeStore();

  const handleFilterClick = (filter: Filter): void => {
    setActiveFilter(filter);

    const incoming: Currency | undefined = currencies.find((c) => c.code === filter?.incomingCurrencyCode);
    const outcoming: Currency | undefined = currencies.find((c) => c.code === filter?.outcomingCurrencyCode);

    if (incoming && outcoming) {
      setIncomingCurrency(incoming);
      setOutcomingCurrency(outcoming);
    }
  };

  return (
    <>
      <div className={styles.filtersContainer}>
        {filters && (
          <div className={styles.filtersList}>
            {filters.map((filter) => (
              <button
                key={filter.name}
                type="button"
                onClick={() => handleFilterClick(filter)}
                className={`${styles.commonBtn} ${activeFilter?.name === filter.name ? styles.active : ``}`}
              >
                {filter.name}
              </button>
            ))}
          </div>
        )}
      </div>
      <div>
        {filters.length > 0 && (
          <button type="button" onClick={clearFilters} className={styles.cleanFiltersBtn}>
            Remove Filters
          </button>
        )}
      </div>
    </>
  );
};

export default ExchangeFilterList;
