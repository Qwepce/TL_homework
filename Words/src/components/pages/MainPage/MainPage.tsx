import Stack from "@mui/material/Stack";
import Button from "@mui/material/Button";
import { useNavigate } from "react-router-dom";

export default function MainPage() {
  const navigate = useNavigate();

  return (
    <Stack spacing={2} direction="row">
      <Button variant="contained" onClick={() => navigate("dictionary")}>
        Заполнить словарь
      </Button>
      <Button variant="outlined" onClick={() => navigate("check")}>
        Проверить знания
      </Button>
    </Stack>
  );
}
