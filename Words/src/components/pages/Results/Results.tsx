import { useLocation, useNavigate } from "react-router-dom";
import styles from "./Results.module.scss";
import HighlightOffIcon from "@mui/icons-material/HighlightOff";
import CheckCircleOutlineRoundedIcon from "@mui/icons-material/CheckCircleOutlineRounded";
import MenuBookRoundedIcon from "@mui/icons-material/MenuBookRounded";
import { Stack, Button } from "@mui/material";

const Results = () => {
  const location = useLocation();
  const stats = location.state?.stats;
  const navigate = useNavigate();

  if (!stats) {
    return <p>Нет данных для отображения результатов.</p>;
  }

  return (
    <>
      <div className={styles.container}>
        <h2>Ответы</h2>
        <div className={styles.result}>
          <CheckCircleOutlineRoundedIcon sx={{ color: `green` }} />{" "}
          {`Правильные: ${stats.correct}`}
        </div>
        <div className={styles.result}>
          <HighlightOffIcon sx={{ color: `red` }} />{" "}
          {`Ошибочные: ${stats.incorrect}`}
        </div>
        <div className={styles.result}>
          <MenuBookRoundedIcon sx={{ color: `purple` }} />{" "}
          {`Всего слов: ${stats.correct + stats.incorrect}`}
        </div>
      </div>
      <div className={styles.btns}>
        <Stack spacing={2} direction="row">
          <Button variant="contained" onClick={() => navigate("/check")}>
            Проверить знания ещё раз
          </Button>
          <Button variant="outlined" onClick={() => navigate("/")}>
            Вернуться в начало
          </Button>
        </Stack>
      </div>
    </>
  );
};

export default Results;
