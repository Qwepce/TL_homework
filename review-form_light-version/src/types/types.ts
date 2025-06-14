export type ReviewData = {
  rating: number;
  username: string;
  review: string;
}

export type EmojiType = {
  src: string;
  alt: string;
};

export type EmojiButtonsList = {
  id: number;
  emoji: EmojiType;
};
