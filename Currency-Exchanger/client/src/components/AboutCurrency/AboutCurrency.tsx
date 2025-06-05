import { memo } from 'react';
import type { Currency } from '../../types/types';
import styles from './AboutCurrency.module.css';

const AboutCurrency = memo(({ name, code, symbol, description }: Currency) => {
  return (
    <div className={styles.container}>
      <h3 className={styles.title}>{name + ' - ' + code + ' - ' + symbol}</h3>
      <p className={styles.subtitle}>{description}</p>
    </div>
  );
});

export default AboutCurrency;