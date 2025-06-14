import { useState, useEffect } from "react";
import styles from "./WordForm.module.scss";
import { Stack, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

interface WordFormProps {
  defaultWordValue?: string;
  defaultTranslationValue?: string;
  onSave: (russian: string, english: string) => void;
}

const WordForm = ({
  defaultWordValue = "",
  defaultTranslationValue = "",
  onSave,
}: WordFormProps) => {
  const [russian, setRussian] = useState(defaultWordValue);
  const [english, setEnglish] = useState(defaultTranslationValue);
  const navigate = useNavigate();

  useEffect(() => {
    setRussian(defaultWordValue);
  }, [defaultWordValue]);

  useEffect(() => {
    setEnglish(defaultTranslationValue);
  }, [defaultTranslationValue]);

  const isFormValid = russian.trim() !== "" && english.trim() !== "";

  const handleSave = () => {
    if (!isFormValid) return;
    onSave(russian.trim(), english.trim());
  };

  return (
    <>
      <div className={styles.container}>
        <h2 className={styles.title}>Словарное слово</h2>
        <hr style={{ border: "none", borderTop: "1px solid #D7DDE7" }} />
        <form
          className={styles.inputContainer}
          onSubmit={(e) => {
            e.preventDefault();
            handleSave();
          }}
        >
          <div className={styles.textInputContainer}>
            Слово на русском языке
            <input
              type="text"
              className={styles.input}
              value={russian}
              onChange={(e) => setRussian(e.target.value)}
            />
          </div>
          <div className={styles.textInputContainer}>
            Перевод на английский язык
            <input
              type="text"
              className={styles.input}
              value={english}
              onChange={(e) => setEnglish(e.target.value)}
            />
          </div>
          <div style={{ marginTop: "20px" }}>
            <Stack spacing={2} direction="row">
              <Button variant="contained" disabled={!isFormValid} type="submit">
                Сохранить
              </Button>
              <Button variant="outlined" onClick={() => navigate(-1)}>
                Отменить
              </Button>
            </Stack>
          </div>
        </form>
      </div>
    </>
  );
};

export default WordForm;
