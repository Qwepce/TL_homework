import { useEffect, useState } from "react";
import { useRef } from "react";
import type { ReviewData } from "../../types/types";
import EmojiButtonsList from "../emojiButtonsList/EmojiButtonsList";
import SubmitReviewButton from "../submitReviewButton/SubmitReviewButton";
import styles from "./ReviewForm.module.css";

interface ReviewFormProps {
  onSubmit: (data: ReviewData) => void;
}

export default function ReviewForm({ onSubmit }: ReviewFormProps) {
  const [username, setUsername] = useState<string>("");
  const [text, setText] = useState<string>("");
  const [rating, setRating] = useState<number>(0);
  const textAreaRef = useRef<HTMLTextAreaElement>(null);

  const adjustTextAreaHeight = (): void => {
    if (textAreaRef.current) {
      textAreaRef.current.style.height = "auto";
      textAreaRef.current.style.height = `${textAreaRef.current.scrollHeight}px`;
    }
  };

  useEffect(() => {
    adjustTextAreaHeight();
  }, [text]);

  const isFormValid: boolean = !!username.trim() && !!text.trim() && rating > 0;

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
      setRating(0);
    }
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <fieldset className={styles.fieldSet}>
        <legend className={styles.title}>
          Помогите нам сделать процесс бронирования лучше
        </legend>
        <div className={styles.buttonsList}>
          <EmojiButtonsList
            selectedRating={rating}
            onRatingSelect={setRating}
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
      <div className={styles.submitButtonContainer}>
        <SubmitReviewButton isDisabled={!isFormValid} />
      </div>
    </form>
  );
}
