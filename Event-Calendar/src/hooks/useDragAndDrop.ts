import { useState } from "react";
import type { CardType } from "../types/types";

export const useDragAndDrop = () => {
  const [draggingCard, setDraggingCard] = useState<CardType | null>(null);
  const [draggingFromColumn, setDraggingFromColumn] = useState<string | null>(
    null
  );
  const [dragOver, setDragOver] = useState<{
    columnId: string | null;
    cardIndex: number | null;
  }>({
    columnId: null,
    cardIndex: null,
  });
  const [isDraggingActive, setIsDraggingActive] = useState(false);

  const handleDragStart = (card: CardType, columnId: string) => {
    setDraggingCard(card);
    setDraggingFromColumn(columnId);
    setIsDraggingActive(true);
  };

  const handleDragEnd = () => {
    setDraggingCard(null);
    setDraggingFromColumn(null);
    setDragOver({ columnId: null, cardIndex: null });
    setIsDraggingActive(false);
  };

  return {
    draggingCard,
    draggingFromColumn,
    dragOver,
    setDragOver,
    isDraggingActive,
    handleDragStart,
    handleDragEnd,
  };
};
