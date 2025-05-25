import Avatar from "../../assets/images/avatar.png";
import styles from "./userReview.module.css";
import type { ReviewData } from "../../types/ReviewData";

export default function UserReview({ username, review, rating }: ReviewData) {
  const averageRating =
    rating.reduce((acc, val) => acc + (val / 25 + 1), 0) / rating.length;

  const formattedRating = Number.isInteger(averageRating)
    ? averageRating.toString()
    : averageRating.toFixed(2);

  return (
    <div className={styles.container}>
      <img
        className={styles.avatar}
        src={Avatar}
        alt={"Изображение профиля пользователя: " + username}
        width={50}
        height={50}
      />
      <p className={styles.username}>{username}</p>
      <p className={styles.review}>{review}</p>
      <p className={styles.rating}>{formattedRating + "/5"}</p>
    </div>
  );
}
