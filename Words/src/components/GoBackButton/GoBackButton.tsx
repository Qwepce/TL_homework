import { Button } from "@mui/material";
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';

interface GoBackButtonProps {
  onClick: () => void;
}

const GoBackButton = ({ onClick }: GoBackButtonProps) => {
  return (
    <Button
      variant="outlined"
      startIcon={<ChevronLeftIcon />}
      size="large"
      onClick={onClick}
      sx={{
        display: "flex",
        alignItems: "center",
        width: "80px",
        height: "60px",
        justifyContent: "center",
        "& .MuiButton-startIcon": {
          margin: 0,
        },
      }}
    />
  );
};

export default GoBackButton;
