import { create } from 'zustand';
import { v4 as uuidv4 } from 'uuid';

interface TextPassageState {
  textToRead: string;
  textPassageId: string;
  isTextSaved: boolean;
  setText: (text: string) => void;
  markAsSaved: () => void;
  reset: () => void;
}

const DEFAULT_TEXT = 'London is the capital of Great Britain.'

export const useTextPassageStore = create<TextPassageState>((set) => ({
  textToRead: DEFAULT_TEXT,
  textPassageId: uuidv4(),
  isTextSaved: false,

  setText: (text: string) => set({ textToRead: text, textPassageId: uuidv4(), isTextSaved: false }),

  markAsSaved: () => set({ isTextSaved: true }),

  reset: () => set({
    textToRead: DEFAULT_TEXT,
    textPassageId: uuidv4(),
    isTextSaved: false,
  })
}));
