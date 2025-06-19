import { useLocation, useNavigate } from "react-router-dom";
import type { Word } from "../../../types/types";
import WordForm from "../../wordForm/WordForm";
import useDictionaryStore from "../../../store/useDictionaryStore";
import GoBackButton from "../../GoBackButton/GoBackButton";
import styles from "./EditWord.module.scss";

const EditWord = () => {
  const { words, updateWord } = useDictionaryStore();
  const location = useLocation();
  const navigate = useNavigate();
  const word: Word | undefined = location.state?.word;

  if (!word) {
    return <div>Отсутствует слово для редактирования</div>;
  }

  const handleSave = (russian: string, english: string): void => {
    const isExists: boolean = words.some(
      (w) =>
        w.russian.toLowerCase() === russian.trim().toLowerCase() &&
        w.id !== word.id
    );
    if (isExists) {
      navigate("/dictionary");
      return;
    }
    updateWord(word.id, russian, english);
    navigate("/dictionary");
  };

  return (
    <>
      <div className={styles.title}>
        <GoBackButton onClick={() => navigate(`/dictionary`)} />
        <h1>Редактирование слова</h1>
      </div>
      <WordForm
        defaultWordValue={word.russian}
        defaultTranslationValue={word.english}
        onSave={handleSave}
      />
    </>
  );
};

export default EditWord;
