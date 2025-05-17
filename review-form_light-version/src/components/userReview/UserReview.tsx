import Avatar from "../../assets/images/avatar.jpg";
import styles from "./userReview.module.css";
import type { ReviewData } from "../../types/ReviewData";

export default function UserReview({ username, review, rating }: ReviewData) {
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
      <p className={styles.rating}>{rating + "/5"}</p>
    </div>
  );
}
