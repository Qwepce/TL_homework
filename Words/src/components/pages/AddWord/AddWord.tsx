import { useNavigate } from "react-router-dom";
import WordForm from "../../wordForm/WordForm";
import useDictionaryStore from "../../../store/useDictionaryStore";

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

  return <WordForm onSave={handleSave} />;
};

export default AddWord;
