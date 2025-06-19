import styles from './ExchangeForm.module.css';
import getFormattedDate from '../../utils/FormattedDate';
import CurrencySelector from '../CurrencySelector/CurrencySelector';
import AboutCurrency from '../AboutCurrency/AboutCurrency';
import { useEffect, useState } from 'react';
import { ShowInfoButton } from '../ShowInfoButton/ShowInfoButton';
import CurrencyChart from '../CurrencyChart/CurrencyChart';
import { useExchangeStore } from '../../stores/useExchangeStore';
import ExchangeFilter from '../ExchangeFilter/ExchangeFilter';
import { SelectorType } from '../../types/enums';

const ExchangeForm = () => {
  const [toggle, setToggle] = useState<boolean>(false);
  const {
    incomingCurrency,
    outcomingCurrency,
    incomingCurrencyAmount,
    outcomingCurrencyAmount,
    exchangeData,
    getExchangeRates
  } = useExchangeStore();

  useEffect(() => {
    getExchangeRates();

    const interval: number = setInterval(() => {
      getExchangeRates();
    }, 10_000);

    return () => clearInterval(interval);
  }, [getExchangeRates]);

  const renderCurrencyInformation = (): JSX.Element => {
    if (incomingCurrency.code === outcomingCurrency.code) {
      return <AboutCurrency {...incomingCurrency} />;
    }

    return (
      <>
        {incomingCurrency && <AboutCurrency {...incomingCurrency} />}
        {outcomingCurrency && <AboutCurrency {...outcomingCurrency} />}
      </>
    );
  };

  const handleShowMoreInfoClick = (e: React.MouseEvent<HTMLButtonElement>): void => {
    e.preventDefault();
    setToggle(!toggle);
  };

  return (
    <form className={styles.exchangeForm}>
      <div className={styles.container}>
        <div className={styles.formInfo}>
          <div className={styles.formTitle}>
            <p className={styles.incomingCurrency}>{`${incomingCurrencyAmount} ${incomingCurrency.name} is`}</p>
            <h1 className={styles.outcomingCurrency}>{`${outcomingCurrencyAmount.toFixed(2)} ${
              outcomingCurrency.name
            }`}</h1>
          </div>
          <div className={styles.date}>{getFormattedDate(new Date())}</div>
          <div className={styles.selectors}>
            <CurrencySelector key={`${incomingCurrency.code}-incoming`} selectorType={SelectorType.Incoming} />
            <CurrencySelector key={`${outcomingCurrency.code}-outcoming`} selectorType={SelectorType.Outcoming} />
          </div>
        </div>
        <div className={styles.chart}>
          <ExchangeFilter />
          <CurrencyChart data={exchangeData} />
        </div>
      </div>
      <div className={styles.buttonContainer}>
        <ShowInfoButton
          currencies={`${incomingCurrency.code}/${outcomingCurrency.code}`}
          isToggle={toggle}
          onClick={handleShowMoreInfoClick}
        />
        <div className={styles.separatorLine}></div>
      </div>
      {toggle && <div className={styles.currenciesInfo}>{renderCurrencyInformation()}</div>}
    </form>
  );
};

export default ExchangeForm;
