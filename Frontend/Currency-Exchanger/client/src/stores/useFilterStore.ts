import { create } from 'zustand';
import type { Filter } from '../types/types';

interface FiltersState {
  filters: Filter[];
  activeFilter: Filter | null;
  setFilters: (filters: Filter[]) => void;
  setActiveFilter: (filter: Filter | null) => void;
  addFilter: (filter: Filter) => void;
  clearFilters: () => void;
}

export const useFiltersStore = create<FiltersState>((set) => ({
  filters: [],
  activeFilter: null,
  setFilters: (filters) => set({ filters }),
  setActiveFilter: (filter) => set({ activeFilter: filter }),
  addFilter: (filter) =>
    set((state) => {
      if (state.filters.find((f) => f.name === filter.name)) {
        return {};
      }
      return { filters: [...state.filters, filter] };
    }),
  clearFilters: () => set({ filters: [], activeFilter: null })
}));
