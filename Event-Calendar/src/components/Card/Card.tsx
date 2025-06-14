import type { CardType } from "../../types/types";
import styles from "./Card.module.css";

interface CardProps {
  card: CardType;
  onDragStart: (card: CardType, columnId: string) => void;
  onDragEnd: () => void;
  isDragging: boolean;
  isPreview: boolean;
  columnId: string;
}

export const Card = ({
  card,
  onDragStart,
  onDragEnd,
  isDragging,
  isPreview,
  columnId,
}: CardProps) => {
  let className = styles.card;
  
  if (!isPreview && isDragging) {
    className += ` ${styles.dragging}`;
  }

  if (!isPreview && !isDragging) {
    className += ` ${styles.default}`;
  }

  return (
    <div
      draggable
      onDragStart={() => onDragStart(card, columnId)}
      onDragEnd={onDragEnd}
      className={className}
    >
      {card.title}
    </div>
  );
};
