import { useNavigate } from "react-router-dom";
import WordForm from "../../wordForm/WordForm";
import useDictionaryStore from "../../../store/useDictionaryStore";
import GoBackButton from "../../GoBackButton/GoBackButton";
import styles from './AddWord.module.scss';

const AddWord = () => {
  const { addWord, words } = useDictionaryStore();
  const navigate = useNavigate();

  const handleSave = (russian: string, english: string): void => {
    const isExists: boolean = words.some(
      (word) => word.russian.toLowerCase() === russian.trim().toLowerCase()
    );
    if (isExists) {
      navigate("/dictionary");
      return;
    }
    addWord(russian, english);
    navigate("/dictionary");
  };

  return (
    <>
      <div className={styles.title}>
        <GoBackButton onClick={() => navigate(`/dictionary`)} />
        <h1>Добавление слова</h1>
      </div>
      <WordForm onSave={handleSave} />
    </>
  );
};

export default AddWord;
