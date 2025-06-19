import styles from './ShowInfoButton.module.css';

interface ShowInfoButtonProps {
  currencies: string;
  isToggle: boolean;
  onClick: (e: React.MouseEvent<HTMLButtonElement>) => void;
}

export const ShowInfoButton =({ currencies, isToggle, onClick }: ShowInfoButtonProps) => {
  return (
    <button className={styles.showMoreInfoButton} type="button" onClick={onClick}>
      {`${currencies}: ${isToggle ? 'hide 🠝' : 'about 🠟'}`}
    </button>
  );
};