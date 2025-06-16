import AngryFace from "../../assets/icons/angry-face.svg";
import SlightlyFrowningFace from "../../assets/icons/slightly-frowning-face.svg";
import NeutralFace from "../../assets/icons/neutral-face.svg";
import SlightlySmilingFace from "../../assets/icons/slightly-smiling-face.svg";
import GrinningFace from "../../assets/icons/grinning-face.svg";
import type { ScaleOptionType } from "./types";

export const emojis: ScaleOptionType[] = [
  { src: AngryFace, alt: "Angry face", rating: 0 },
  { src: SlightlyFrowningFace, alt: "Slightly frowning face", rating: 25 },
  { src: NeutralFace, alt: "Neutral face", rating: 50 },
  { src: SlightlySmilingFace, alt: "Slightly smiling face", rating: 75 },
  { src: GrinningFace, alt: "Grinning face", rating: 100 },
];

export const colors: string[] = ["#E31919", "#FF8311", "#FF8311", "#FFC700", "#FFC700"];