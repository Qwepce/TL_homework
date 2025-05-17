import styles from "./submitReviewButton.module.css";

interface SubmitButtonProps {
  isDisabled: boolean;
}

export default function SubmitReviewButton({ isDisabled }: SubmitButtonProps) {
  return (
    <button className={styles.button} type="submit" disabled={isDisabled}>
      Отправить
    </button>
  );
}
