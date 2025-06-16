import { memo } from 'react';
import type { Currency } from '../../types/types';
import styles from './AboutCurrency.module.css';

const AboutCurrency = memo((currency: Currency) => {
  return (
    <div className={styles.container}>
      <h3 className={styles.title}>{currency.name + ' - ' + currency.code + ' - ' + currency.symbol}</h3>
      <p className={styles.subtitle}>{currency.description}</p>
    </div>
  );
});

export default AboutCurrency;