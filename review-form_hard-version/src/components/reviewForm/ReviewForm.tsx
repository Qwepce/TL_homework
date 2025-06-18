import { useEffect, useState } from "react";
import { useRef } from "react";
import type { ReviewData } from "../../types/types";
import SubmitReviewButton from "../submitReviewButton/SubmitReviewButton";
import ScaleRatingList from "../scaleRatingList/ScaleRatingList";
import styles from "./ReviewForm.module.css";

interface ReviewFormProps {
  onSubmit: (data: ReviewData) => void;
}

export default function ReviewForm({ onSubmit }: ReviewFormProps) {
  const [username, setUsername] = useState<string>("");
  const [text, setText] = useState<string>("");
  const [rating, setRating] = useState<number[]>(Array(5).fill(-1));
  const [resetFlag, setResetFlag] = useState<boolean>(false);
  const textAreaRef = useRef<HTMLTextAreaElement>(null);

  const handleRatingChange = (id: number, value: number): void => {
    setRating((prev: number[]) => {
      const newRatings: number[] = [...prev];
      newRatings[id - 1] = value;
      return newRatings;
    });
  };

  useEffect(() => {
    if (resetFlag) {
      setResetFlag(false);
    }
  }, [resetFlag]);

  const adjustTextAreaHeight = (): void => {
    if (textAreaRef.current) {
      textAreaRef.current.style.height = "auto";
      textAreaRef.current.style.height = `${textAreaRef.current.scrollHeight}px`;
    }
  };

  useEffect(() => {
    adjustTextAreaHeight();
  }, [text]);

  const allRatingsSelected: boolean = rating.length === 5 && rating.every((rate) => rate !== -1);
  const isFormValid: boolean = !!username.trim() && !!text.trim() && allRatingsSelected;

  const handleSubmit = (e: React.FormEvent): void => {
    e.preventDefault();
    if (isFormValid) {
      onSubmit({
        rating,
        username: username.trim(),
        text: text.trim(),
      });
      setUsername("");
      setText("");
      setRating(Array(5).fill(-1));
      setResetFlag(true);
    }
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <fieldset className={styles.fieldSet}>
        <legend className={styles.title}>
          Помогите нам сделать процесс бронирования лучше
        </legend>
        <div className={styles.scalesContainer}>
          <ScaleRatingList
            onRatingChange={handleRatingChange}
            reset={resetFlag}
          />
        </div>
        <div className={styles.usernameContainer}>
          <label htmlFor="username">*Имя</label>
          <input
            type="text"
            id="username"
            placeholder="Как вас зовут?"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>
        <textarea
          ref={textAreaRef}
          id="review"
          placeholder="Напишите, что понравилось, что было непонятно"
          value={text}
          onChange={(e) => setText(e.target.value)}
        ></textarea>
      </fieldset>
      <div className={styles.submitButton}>
        <SubmitReviewButton isDisabled={!isFormValid} />
      </div>
    </form>
  );
}
