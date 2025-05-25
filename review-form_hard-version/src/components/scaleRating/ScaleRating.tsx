import { useEffect, useState } from "react";
import AngryFace from "../../assets/icons/angry-face.svg";
import SlightlyFrowningFace from "../../assets/icons/slightly-frowning-face.svg";
import NeutralFace from "../../assets/icons/neutral-face.svg";
import SlightlySmilingFace from "../../assets/icons/slightly-smiling-face.svg";
import GrinningFace from "../../assets/icons/grinning-face.svg";
import styles from "./scaleRating.module.css";

interface ScaleOptionProps {
  src: string;
  alt: string;
  rating: number;
}

interface ScaleRatingProps {
  scaleTitle: string;
  id: number;
  onRatingChange: (id: number, value: number) => void;
  reset: boolean;
}

const emojis: ScaleOptionProps[] = [
  { src: AngryFace, alt: "Angry face", rating: 0 },
  { src: SlightlyFrowningFace, alt: "Slightly frowning face", rating: 25 },
  { src: NeutralFace, alt: "Neutral face", rating: 50 },
  { src: SlightlySmilingFace, alt: "Slightly smiling face", rating: 75 },
  { src: GrinningFace, alt: "Grinning face", rating: 100 },
];

const colors = ["#E31919", "#FF8311", "#FF8311", "#FFC700", "#FFC700"];

export default function ScaleRating({
  scaleTitle,
  id,
  onRatingChange,
  reset,
}: ScaleRatingProps) {
  const [selectedValue, setSelectedValue] = useState<number | null>(null);
  const inputName = `progress-${id}`;

  useEffect(() => {
    if (reset) {
      setSelectedValue(null);
    }
  }, [reset]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseInt(e.target.value);
    setSelectedValue(value);
    onRatingChange(id, value);
  };

  const getColorIndex = (rating: number) => {
    return Math.floor(rating / 25);
  };

  const getDotColor = (emojiRating: number, selectedValue: number | null) => {
    if (selectedValue === null) {
      return colors[getColorIndex(emojiRating)];
    }

    return emojiRating <= selectedValue ? selectedColor : "#FFF";
  };

  const selectedColor =
    selectedValue !== null ? colors[getColorIndex(selectedValue)] : null;

  return (
    <div className={styles.scaleContainer}>
      <input
        type="range"
        min="0"
        max="100"
        step={100 / (emojis.length - 1)}
        value={selectedValue ?? -1}
        onChange={handleChange}
        aria-label={scaleTitle}
        className={styles.hiddenRange}
      />

      <div className={styles.scaleRating}>
        <div
          className={styles.scaleLine}
          style={{
            background: selectedColor
              ? `linear-gradient(to right, ${selectedColor} 0%, ${selectedColor} ${selectedValue}%, transparent ${selectedValue}%, transparent 100%)`
              : undefined,
            borderImage: selectedColor
              ? `linear-gradient(to right, ${selectedColor} 0%, ${selectedColor} ${selectedValue}%, #DCDCDC ${selectedValue}%, #DCDCDC 100%) 1 stretch`
              : undefined,
          }}
        ></div>

        {emojis.map((emoji) => {
          const emojiColor = colors[getColorIndex(emoji.rating)];
          const isSelected = selectedValue === emoji.rating;
          const dotColor = getDotColor(emoji.rating, selectedValue);

          return (
            <label key={emoji.rating} className={styles.scaleOption}>
              <input
                aria-label={`Оценка ${selectedValue} из 100`}
                className={styles.input}
                type="radio"
                name={inputName}
                value={emoji.rating}
                checked={isSelected}
                onChange={handleChange}
              />
              <span
                className={styles.scaleDot}
                style={{ backgroundColor: dotColor ?? emojiColor }}
              >
                <img
                  src={emoji.src}
                  alt={emoji.alt}
                  width={10}
                  height={10}
                  hidden={!isSelected}
                />
              </span>
            </label>
          );
        })}
      </div>

      <div className={styles.scaleTitle}>{scaleTitle}</div>
    </div>
  );
}
