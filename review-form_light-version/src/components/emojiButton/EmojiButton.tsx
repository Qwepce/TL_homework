import type { EmojiType } from "../../types/types";
import styles from "./EmojiButton.module.css";

interface EmojiButtonProps {
  emoji: EmojiType;
  isActive: boolean;
  onClick: () => void;
}

export default function EmojiButton({
  emoji,
  isActive,
  onClick,
}: EmojiButtonProps) {
  return (
    <button
      className={`${styles.button} ${isActive ? `${styles.active}` : ""}`}
      onClick={onClick}
      type="button"
    >
      <img src={emoji.src} alt={emoji.alt} />
    </button>
  );
}
