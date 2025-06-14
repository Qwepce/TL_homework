import type { ScaleRatingType } from "../../types/types";
import ScaleRating from "../scaleRating/ScaleRating";
import styles from "./scaleRatingList.module.css";

interface ScaleRatingListProps {
  onRatingChange: (id: number, value: number) => void;
  reset: boolean;
}

const scaleRatings: ScaleRatingType[] = [
  { id: 1, scaleTitle: "Чистенько" },
  { id: 2, scaleTitle: "Сервис" },
  { id: 3, scaleTitle: "Скорость" },
  { id: 4, scaleTitle: "Место" },
  { id: 5, scaleTitle: "Культура речи" },
];

export default function ScaleRatingList({
  onRatingChange,
  reset,
}: ScaleRatingListProps) {
  return (
    <ul className={styles.container}>
      {scaleRatings.map((scaleRating) => (
        <li key={scaleRating.id}>
          <ScaleRating
            scaleTitle={scaleRating.scaleTitle}
            id={scaleRating.id}
            onRatingChange={onRatingChange}
            reset={reset}
          />
        </li>
      ))}
    </ul>
  );
}
