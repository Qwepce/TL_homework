import Avatar from "../../assets/images/avatar.jpg";
import styles from "./UserReview.module.css";
import type { ReviewData } from "../../types/types";

interface ReviewProps {
  review: ReviewData;
}

export default function UserReview({ review }: ReviewProps) {
  return (
    <div className={styles.container}>
      <img
        className={styles.avatar}
        src={Avatar}
        alt={"Изображение профиля пользователя: " + review.username}
        width={50}
        height={50}
      />
      <p className={styles.username}>{review.username}</p>
      <p className={styles.text}>{review.text}</p>
      <p className={styles.rating}>{review.rating + "/5"}</p>
    </div>
  );
}
