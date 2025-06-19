import { useEffect } from 'react';
import './App.css';
import ExchangeForm from './components/ExchangerForm/ExchangeForm.tsx';
import { useExchangeStore } from './stores/useExchangeStore.ts';
import ExchangeFilterList from './components/ExchangeFilterList/ExchangeFilterList.tsx';
import { useFiltersStore } from './stores/useFilterStore.ts';

export const App = () => {
  const { error, loading, getCurrencies } = useExchangeStore();
  const { filters } = useFiltersStore();

  useEffect(() => {
    getCurrencies();
  }, []);

  if (loading) {
    return (
      <div className="app-loading">
        <p>Loading...</p>
        <div className="loader" />
      </div>
    );
  }

  if (error) {
    return (
      <div className="fetch-error">
        <p>{error}</p>
      </div>
    );
  }

  return (
    <div className="app-container">
      <div className={`filters-list ${filters.length > 0 ? 'visible' : ''}`}>
        <ExchangeFilterList />
      </div>
      <ExchangeForm />
    </div>
  );
};
