import { useState } from "react";
import { Button } from "@mui/material";
import useDictionaryStore from "../../../store/useDictionaryStore";
import {
  TextField,
  Select,
  MenuItem,
  Checkbox,
  ListItemText,
} from "@mui/material";
import styles from "./CheckWords.module.scss";
import { useNavigate } from "react-router-dom";
import GoBackButton from "../../GoBackButton/GoBackButton";
import type { Result, Word } from "../../../types/types";

const CheckWords = () => {
  const { words } = useDictionaryStore();
  const [selectedTranslation, setSelectedTranslation] = useState<string>("");
  const [currentWordIndex, setCurrentWordIndex] = useState<number>(0);
  const [currentResult, setCurrentResult] = useState<Result>({
    correct: 0,
    incorrect: 0,
  });
  const [isChecked, setIsChecked] = useState(false);
  const navigate = useNavigate();

  const getRandomOptions = (): string[] => {
    const correctOption: string = words[currentWordIndex].english;
    const otherOptions: string[] = words
      .filter((_, index) => index !== currentWordIndex)
      .map((word) => word.english)
      .sort(() => Math.random() - 0.5)
      .slice(0, 3);

    return [correctOption, ...otherOptions].sort(() => Math.random() - 0.5);
  };

  const options: string[] = getRandomOptions();
  const currentWord: Word = words[currentWordIndex];
  const isAnswerCorrect: boolean = selectedTranslation === currentWord.english;

  const getUpdatedResult = (
    isCorrect: boolean,
    previousResult: Result
  ): Result => {
    return {
      ...previousResult,
      correct: isCorrect ? previousResult.correct + 1 : previousResult.correct,
      incorrect: isCorrect
        ? previousResult.incorrect
        : previousResult.incorrect + 1,
    };
  };

  const handleCheck = (): void => {
    const result: Result = getUpdatedResult(isAnswerCorrect, currentResult);
    const isLastWord: boolean = currentWordIndex + 1 === words.length;

    setCurrentResult(result);
    setIsChecked(true);

    if (isLastWord) {
      navigate("/results", { state: { result: result } });
    } else {
      handleNext();
    }
  };

  const handleNext = (): void => {
    setSelectedTranslation("");
    setIsChecked(false);
    setCurrentWordIndex((prev) => (prev + 1) % words.length);
  };

  return (
    <>
      <div className={styles.title}>
        <GoBackButton onClick={() => navigate(`/`)} />
        <h1>Проверка знаний</h1>
      </div>
      <p>
        {`Слово ${currentWordIndex + 1} из ${words.length}`}
      </p>
      <form className={styles.checkForm}>
        <div className={styles.inputContainer}>
          <label htmlFor="rusWord">Русское слово</label>
          <TextField
            id="rusWord"
            variant="outlined"
            value={currentWord.russian}
            disabled
            sx={{
              "& .MuiInputBase-input.Mui-disabled": {
                WebkitTextFillColor: "rgba(0, 0, 0, 0.8)",
              },
            }}
          />
        </div>
        <div className={styles.inputContainer}>
          <label htmlFor="translation-select">Перевод на английский язык</label>
          <Select
            id="translation-select"
            value={selectedTranslation}
            onChange={(e) => setSelectedTranslation(e.target.value)}
            sx={{ minWidth: "40%" }}
            disabled={isChecked}
            displayEmpty
            renderValue={(selected) => {
              if (!selected) {
                return <span>Не выбрано</span>;
              }
              return selected;
            }}
          >
            {options.map((option) => (
              <MenuItem key={option} value={option}>
                <Checkbox
                  checked={selectedTranslation === option}
                  disabled={isChecked}
                />
                <ListItemText primary={option} />
              </MenuItem>
            ))}
          </Select>
        </div>
      </form>
      <div style={{ marginTop: `30px` }}>
        <Button
          variant="contained"
          onClick={handleCheck}
          disabled={!selectedTranslation}
        >
          Проверить
        </Button>
      </div>
    </>
  );
};

export default CheckWords;
