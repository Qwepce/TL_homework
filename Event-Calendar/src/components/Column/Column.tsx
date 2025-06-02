import React from "react";
import type { CardType } from "../../types/types";
import { Card } from "../Card/Card";
import { PreviewCard } from "../PreviewCard/PreviewCard";
import styles from "./Column.module.css";

interface ColumnProps {
  id: string;
  cards: CardType[];
  onCardDrop: (toColumnId: string, toCardIndex: number | null) => void;
  draggingCard: CardType | null;
  draggingFromColumn: string | null;
  dragOver: { columnId: string | null; cardIndex: number | null };
  setDragOver: (over: {
    columnId: string | null;
    cardIndex: number | null;
  }) => void;
  isDraggingActive: boolean;
  onDragStart: (card: CardType, columnId: string) => void;
  onDragEnd: () => void;
}

export const Column = ({
  id,
  cards,
  onCardDrop,
  onDragStart,
  onDragEnd,
  draggingCard,
  draggingFromColumn,
  dragOver,
  setDragOver,
  isDraggingActive,
}: ColumnProps) => {
  const draggingCardIndex = draggingCard
    ? cards.findIndex((card) => card.id === draggingCard.id)
    : -1;

  const isDraggingDifferentPosition =
    dragOver.columnId !== draggingFromColumn ||
    dragOver.cardIndex !== draggingCardIndex;

  const shouldShowPreview =
    isDraggingActive &&
    draggingCard !== null &&
    dragOver.columnId === id &&
    isDraggingDifferentPosition;

  const handleDragOver = (e: React.DragEvent, idx: number) => {
    e.preventDefault();
    if (!isDraggingActive) return;
    if (dragOver.columnId !== id || dragOver.cardIndex !== idx) {
      setDragOver({ columnId: id, cardIndex: idx });
    }
  };

  const handleDragOverEmpty = (e: React.DragEvent) => {
    e.preventDefault();
    if (!isDraggingActive) return;
    if (dragOver.columnId !== id || dragOver.cardIndex !== null) {
      setDragOver({ columnId: id, cardIndex: null });
    }
  };

  const handleDrop = (e: React.DragEvent, idx: number | null) => {
    e.preventDefault();
    onCardDrop(id, idx);
  };

  const renderCard = (card: CardType, idx: number) => {
    const isOldPlace =
      draggingCard?.id === card.id && draggingFromColumn === id;

    return (
      <React.Fragment key={card.id}>
        {shouldShowPreview && dragOver.cardIndex === idx && (
          <PreviewCard
            card={draggingCard!}
            columnId={id}
            onDragOver={(e) => handleDragOver(e, idx)}
            onDrop={(e) => handleDrop(e, idx)}
          />
        )}
        <div
          onDragOver={(e) => handleDragOver(e, idx)}
          onDrop={(e) => handleDrop(e, idx)}
          className={isOldPlace ? styles.oldPlaceHighlight : undefined}
        >
          <Card
            card={card}
            onDragStart={onDragStart}
            onDragEnd={onDragEnd}
            isDragging={draggingCard?.id === card.id}
            isPreview={false}
            columnId={id}
          />
        </div>
      </React.Fragment>
    );
  };

  return (
    <div className={styles.column}>
      {cards.map(renderCard)}

      <div
        className={styles.dropZone}
        onDragOver={handleDragOverEmpty}
        onDrop={(e) => handleDrop(e, null)}
      >
        {shouldShowPreview && dragOver.cardIndex === null && (
          <PreviewCard
            card={draggingCard!}
            columnId={id}
            onDragOver={handleDragOverEmpty}
            onDrop={(e) => handleDrop(e, null)}
          />
        )}
      </div>
    </div>
  );
};
