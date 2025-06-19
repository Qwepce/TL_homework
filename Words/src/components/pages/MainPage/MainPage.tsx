import Stack from "@mui/material/Stack";
import Button from "@mui/material/Button";
import { useNavigate } from "react-router-dom";
import styles from "./MainPage.module.scss";

const MainPage = () => {
  const navigate = useNavigate();

  return (
    <>
      <div className={styles.title}>
        <h1>Выберите режим</h1>
      </div>
      <Stack spacing={2} direction="row">
        <Button variant="contained" onClick={() => navigate("dictionary")}>
          Заполнить словарь
        </Button>
        <Button variant="outlined" onClick={() => navigate("check")}>
          Проверить знания
        </Button>
      </Stack>
    </>
  );
};

export default MainPage;
