import { useLocation } from "react-router-dom";

const usePageLocation = () => {
  const location = useLocation();

  switch (location.pathname) {
    case "/":
      return "Выберите режим";

    case "/dictionary":
      return "Словарь";

    case "/new-word":
      return "Добавление слова";

    case "/edit-word":
      return "Редактирование слова";

    case "/check":
      return "Проверка знаний";

    case "/results":
      return "Результат проверки знаний";

    default:
      throw new Error(`Invalid location`);
  }
};

export default usePageLocation;
