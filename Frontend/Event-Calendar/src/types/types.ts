export interface CardType {
  id: string;
  title: string;
}

export interface ColumnType {
  id: string;
  cards: CardType[];
}
