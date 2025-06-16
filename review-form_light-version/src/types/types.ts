export type ReviewData = {
  rating: number;
  username: string;
  text: string;
}

export type EmojiType = {
  src: string;
  alt: string;
};

export type EmojiButtonsList = {
  id: number;
  emoji: EmojiType;
};
