export type ReviewData = {
  rating: number[];
  username: string;
  text: string;
};

export type ScaleOptionType = {
  src: string;
  alt: string;
  rating: number;
};

export type ScaleRatingType = {
  id: number;
  scaleTitle: string;
}