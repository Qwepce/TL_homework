import { create } from "zustand";
import type { Word } from "../types/types";
import { createJSONStorage, persist } from "zustand/middleware";
import normalize from "../utils/normalize";

interface DictionaryStore {
  words: Word[];

  addWord: (word: string, translation: string) => void;
  deleteWord: (id: string) => void;
  getWordById: (id: string) => Word | undefined;
  updateWord: (id: string, russian: string, english: string) => void;
}

const useDictionaryStore = create<DictionaryStore>()(
  persist(
    (set, get) => ({
      words: [],

      addWord: (rus: string, eng: string): void => {
        const { words } = get();
        const newWord: Word = {
          id: crypto.randomUUID(),
          russian: normalize(rus),
          english: normalize(eng),
        };
        set({ words: [...words, newWord] });
      },

      deleteWord: (id: string): void => {
        set({ words: get().words.filter((word) => word.id !== id) });
      },

      getWordById: (id: string): Word | undefined => {
        const words: Word[] = get().words;
        return words.find((word) => word.id === id);
      },

      updateWord: (id: string, russian: string, english: string): void =>
        set((state: DictionaryStore) => ({
          words: state.words.map((word) =>
            word.id === id
              ? {
                  ...word,
                  russian: normalize(russian),
                  english: normalize(english),
                }
              : word
          ),
        })),
    }),
    {
      name: "words",
      storage: createJSONStorage(() => localStorage),
    }
  )
);

export default useDictionaryStore;
