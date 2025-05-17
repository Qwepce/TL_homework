import AngryFace from "../../assets/icons/angry-face.svg";
import SlightlyFrowningFace from "../../assets/icons/slightly-frowning-face.svg";
import NeutralFace from "../../assets/icons/neutral-face.svg";
import SlightlySmilingFace from "../../assets/icons/slightly-smiling-face.svg";
import GrinningFace from "../../assets/icons/grinning-face.svg";
import EmojiButton from "../emojiButton/EmojiButton";
import styles from "./emojiButtonsList.module.css";

interface EmojiButtonProps {
  id: number;
  src: string;
  alt: string;
}

interface EmojiButtonsListProps {
  onRatingSelect: (rating: number) => void;
  selectedRating: number;
}

const buttons: EmojiButtonProps[] = [
  { id: 1, src: AngryFace, alt: "Angry face" },
  { id: 2, src: SlightlyFrowningFace, alt: "Slightly frowning face" },
  { id: 3, src: NeutralFace, alt: "Neutral face" },
  { id: 4, src: SlightlySmilingFace, alt: "Slightly smiling face" },
  { id: 5, src: GrinningFace, alt: "Grinning face" },
];

export default function EmojiButtonsList({
  onRatingSelect,
  selectedRating,
}: EmojiButtonsListProps) {
  const handleClick = (id: number) => {
    const newRating = id === selectedRating ? 0 : id;
    onRatingSelect(newRating);
  };

  return (
    <ul className={styles.buttonsList}>
      {buttons.map((button) => (
        <li key={button.id}>
          <EmojiButton
            src={button.src}
            alt={button.alt}
            isActive={button.id === selectedRating}
            onClick={() => handleClick(button.id)}
          />
        </li>
      ))}
    </ul>
  );
}
