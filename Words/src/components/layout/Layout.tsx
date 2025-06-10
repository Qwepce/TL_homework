import { useLocation, useNavigate } from "react-router-dom";
import GoBackButton from "./GoBackButton/GoBackButton";
import usePageLocation from "../../hooks/usePageLocation";
import styles from "./Layout.module.scss";
import { memo } from "react";

const Layout = memo(() => {
  const navigate = useNavigate();
  const location = useLocation();
  let titleText;

  try {
    titleText = usePageLocation();
  } catch {
    return (
      <h1 style={{ display: `flex`, justifyContent: `center` }}>
        Страница с таким адресом не была найдена
      </h1>
    );
  }

  const isMainPageOrResult =
    location.pathname === "/" || location.pathname === "/result";

  return (
    <div className={styles.layout}>
      {!isMainPageOrResult && <GoBackButton onClick={() => navigate(`/`)} />}
      <h1>{titleText}</h1>
    </div>
  );
});

export default Layout;
