import * as React from "react";
import Button from "@mui/material/Button";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import MenuIcon from "@mui/icons-material/Menu";
import DeleteIcon from "@mui/icons-material/Delete";
import CreateIcon from "@mui/icons-material/Create";
import { useNavigate } from "react-router-dom";
import useDictionaryStore from "../../../../store/useDictionaryStore";

interface PositionedMenuProps {
  wordID: string;
}

export default function PositionedMenu({ wordID }: PositionedMenuProps) {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const { deleteWord, getWordById } = useDictionaryStore();
  const navigate = useNavigate();

  const handleEditWord = () => {
    const word = getWordById(wordID);
    navigate(`/edit-word`, { state: { word } });
  };

  return (
    <div>
      <Button
        id="demo-positioned-button"
        aria-controls={open ? "demo-positioned-menu" : undefined}
        aria-haspopup="true"
        aria-expanded={open ? "true" : undefined}
        onClick={handleClick}
        startIcon={<MenuIcon />}
        sx={{ color: `rgba(0, 0, 0, 0.8);` }}
      ></Button>
      <Menu
        id="demo-positioned-menu"
        aria-labelledby="demo-positioned-button"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        anchorOrigin={{
          vertical: "top",
          horizontal: "left",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "left",
        }}
      >
        <MenuItem onClick={() => handleEditWord()}>
          <CreateIcon sx={{ marginRight: 1, color: `#1976d2` }} />
          Редактировать
        </MenuItem>
        <MenuItem onClick={() => deleteWord(wordID)}>
          <DeleteIcon sx={{ marginRight: 1, color: `#1976d2` }} />
          Удалить
        </MenuItem>
      </Menu>
    </div>
  );
}
