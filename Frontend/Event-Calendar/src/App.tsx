import { useState } from "react";
import type { CardType, ColumnType } from "./types/types";
import { Column } from "./components/Column/Column";
import { initialData } from "./mockData/data";
import { useDragAndDrop } from "./hooks/useDragAndDrop";
import "./App.css";

export const App = () => {
  const [columns, setColumns] = useState<ColumnType[]>(initialData);
  const {
    draggingCard,
    draggingFromColumn,
    dragOver,
    setDragOver,
    isDraggingActive,
    handleDragStart,
    handleDragEnd,
  } = useDragAndDrop();

  const removeCardFromColumn = (
    columns: ColumnType[],
    columnId: string,
    cardId: string
  ): { updatedColumns: ColumnType[]; removedCard: CardType | null } => {
    let removedCard: CardType | null = null;

    const updatedColumns = columns.map((col) => {
      if (col.id === columnId) {
        const filteredCards = col.cards.filter((card) => {
          if (card.id === cardId) {
            removedCard = card;
            return false;
          }
          return true;
        });
        return { ...col, cards: filteredCards };
      }
      return col;
    });

    return { updatedColumns, removedCard };
  };

  const insertCardIntoColumn = (
    columns: ColumnType[],
    columnId: string,
    card: CardType,
    index: number | null
  ): ColumnType[] => {
    return columns.map((col) => {
      if (col.id === columnId) {
        const newCards = [...col.cards];
        if (index === null || index >= newCards.length) {
          newCards.push(card);
        } else {
          newCards.splice(index, 0, card);
        }
        return { ...col, cards: newCards };
      }
      return col;
    });
  };

  const handleCardDrop = (toColumnId: string, toCardIndex: number | null) => {
    if (!draggingCard || !draggingFromColumn) {
      return;
    }

    setColumns((prevColumns) => {
      const { updatedColumns, removedCard } = removeCardFromColumn(
        prevColumns,
        draggingFromColumn,
        draggingCard.id
      );

      if (removedCard === null) {
        return prevColumns;
      }

      const columnsWithInsertedCard = insertCardIntoColumn(
        updatedColumns,
        toColumnId,
        removedCard,
        toCardIndex
      );

      return columnsWithInsertedCard;
    });

    handleDragEnd();
  };

  return (
    <div className="app">
      {columns.map((col) => (
        <Column
          key={col.id}
          id={col.id}
          cards={col.cards}
          onCardDrop={handleCardDrop}
          onDragStart={handleDragStart}
          onDragEnd={handleDragEnd}
          draggingCard={draggingCard}
          draggingFromColumn={draggingFromColumn}
          dragOver={dragOver}
          setDragOver={setDragOver}
          isDraggingActive={isDraggingActive}
        />
      ))}
    </div>
  );
};