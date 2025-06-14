import { Button } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useNavigate } from "react-router-dom";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
} from "@mui/material";
import ActionMenu from "./ActionMenu/ActionMenu";
import useDictionaryStore from "../../../store/useDictionaryStore";
import GoBackButton from "../../GoBackButton/GoBackButton";

const Dictionary = () => {
  const { words } = useDictionaryStore();
  const navigate = useNavigate();

  return (
    <>
      <div style={{ display: `flex`, alignItems: `center`, columnGap: `10px` }}>
        <GoBackButton onClick={() => navigate(`/`)} />
        <h1>Словарь</h1>
      </div>
      <Button
        startIcon={<AddIcon />}
        variant="contained"
        onClick={() => navigate("/new-word")}
      >
        Добавить слово
      </Button>
      <TableContainer
        component={Paper}
        sx={{
          maxWidth: `100%`,
          marginTop: "40px",
          boxShadow: `none`,
          display: `flex`,
          justifyContent: `space-between`,
        }}
      >
        <Table aria-label="dictionary table">
          <TableHead sx={{ backgroundColor: `#DFE4EC` }}>
            <TableRow>
              <TableCell>Слово на русском языке</TableCell>
              <TableCell>Перевод на английский язык</TableCell>
              <TableCell sx={{ textAlign: `right` }}>Действие</TableCell>
            </TableRow>
          </TableHead>
          {words.length > 0 ? (
            <TableBody>
              {words.map(({ id, russian, english }) => (
                <TableRow key={id}>
                  <TableCell>{russian}</TableCell>
                  <TableCell>{english}</TableCell>
                  <TableCell sx={{ textAlign: `right` }}>
                    <ActionMenu wordID={id} />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          ) : (
            <TableBody>
              <TableRow>
                <TableCell
                  colSpan={3}
                  sx={{ textAlign: "center", padding: "50px" }}
                >
                  В вашем словаре нет ни одного слова
                </TableCell>
              </TableRow>
            </TableBody>
          )}
        </Table>
      </TableContainer>
    </>
  );
};

export default Dictionary;
