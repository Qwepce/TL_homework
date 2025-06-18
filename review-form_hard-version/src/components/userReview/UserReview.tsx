import Avatar from "../../assets/images/avatar.png";
import styles from "./UserReview.module.css";
import type { ReviewData } from "../../types/types";

interface UserReviewProps {
  review: ReviewData;
}

export default function UserReview({ review }: UserReviewProps) {
  const averageRating =
    review.rating.reduce((acc, val) => acc + (val / 25 + 1), 0) / review.rating.length;

  const formattedRating = Number.isInteger(averageRating)
    ? averageRating.toString()
    : averageRating.toFixed(2);

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
      <p className={styles.rating}>{formattedRating + "/5"}</p>
    </div>
  );
}
