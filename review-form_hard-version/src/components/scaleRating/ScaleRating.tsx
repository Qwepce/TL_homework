import { useEffect, useState, type JSX } from "react";
import styles from "./ScaleRating.module.css";
import { colors, emojis } from "../../types/constants";

interface ScaleRatingProps {
  id: number;
  scaleTitle: string;
  reset: boolean;
  onRatingChange: (id: number, value: number) => void;
}

export default function ScaleRating({
  scaleTitle,
  id,
  onRatingChange,
  reset,
}: ScaleRatingProps) {
  const [selectedValue, setSelectedValue] = useState<number | null>(null);
  const inputName: string = `progress-${id}`;

  useEffect(() => {
    if (reset) {
      setSelectedValue(null);
    }
  }, [reset]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const value: number = parseInt(e.target.value);
    setSelectedValue(value);
    onRatingChange(id, value);
  };

  const getColorIndex = (rating: number): number => {
    return Math.floor(rating / 25);
  };

  const getDotColor = (emojiRating: number, selectedValue: number | null): string | null => {
    if (selectedValue === null) {
      return colors[getColorIndex(emojiRating)];
    }

    return emojiRating <= selectedValue ? selectedColor : "#FFF";
  };

  const renderEmojis = (): JSX.Element[] => {
    return emojis.map((emoji) => {
          const emojiColor: string = colors[getColorIndex(emoji.rating)];
          const isSelected: boolean = selectedValue === emoji.rating;
          const dotColor: string | null = getDotColor(emoji.rating, selectedValue);

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
        });
  }

  const selectedColor: string | null =
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

        {renderEmojis()}
      </div>

      <div className={styles.scaleTitle}>{scaleTitle}</div>
    </div>
  );
}