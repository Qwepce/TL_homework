import styles from "./emojiButton.module.css";

interface EmojiButtonProps {
  src: string;
  alt: string;
  isActive: boolean;
  onClick: () => void;
}

export default function EmojiButton({
  src,
  alt,
  isActive,
  onClick,
}: EmojiButtonProps) {
  return (
    <button
      className={`${styles.button} ${isActive ? `${styles.active}` : ""}`}
      onClick={onClick}
      type="button"
    >
      <img src={src} alt={alt} />
    </button>
  );
}
