import { useNavigate } from "react-router-dom";
import WordForm from "../../wordForm/WordForm";
import useDictionaryStore from "../../../store/useDictionaryStore";
import GoBackButton from "../../GoBackButton/GoBackButton";

const AddWord = () => {
  const { addWord, words } = useDictionaryStore();
  const navigate = useNavigate();

  const handleSave = (russian: string, english: string) => {
    const isExists = words.some(
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
      <div style={{ display: `flex`, alignItems: `center`, columnGap: `10px` }}>
        <GoBackButton onClick={() => navigate(`/dictionary`)} />
        <h1>Добавление слова</h1>
      </div>
      <WordForm onSave={handleSave} />
    </>
  );
};

export default AddWord;
