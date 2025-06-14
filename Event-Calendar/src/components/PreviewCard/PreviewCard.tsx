import { Card } from "../Card/Card";
import type { CardType } from "../../types/types";
import styles from "./PreviewCard.module.css";

interface PreviewCardProps {
  card: CardType;
  columnId: string;
  onDragOver: (e: React.DragEvent) => void;
  onDrop: (e: React.DragEvent) => void;
}

export const PreviewCard = ({
  card,
  columnId,
  onDragOver,
  onDrop,
}: PreviewCardProps) => (
  <div className={styles.previewCard} onDragOver={onDragOver} onDrop={onDrop}>
    <Card
      card={card}
      onDragStart={() => {}}
      onDragEnd={() => {}}
      isDragging={false}
      isPreview={true}
      columnId={columnId}
    />
  </div>
);
