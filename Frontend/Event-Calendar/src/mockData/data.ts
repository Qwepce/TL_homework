import type { ColumnType } from "../types/types";

export const initialData: ColumnType[] = [
  {
    id: "column-1",
    cards: [
      { id: crypto.randomUUID(), title: "Read a book" },
      { id: crypto.randomUUID(), title: "Play football" },
    ],
  },
  {
    id: "column-2",
    cards: [{ id: crypto.randomUUID(), title: "Do home assignment" }],
  },
  {
    id: "column-3",
    cards: [{ id: crypto.randomUUID(), title: "Wash shirts" }],
  },
  {
    id: "column-4",
    cards: [{ id: crypto.randomUUID(), title: "Go on a date" }],
  },
];
