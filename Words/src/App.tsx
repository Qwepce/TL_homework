import { Routes, Route } from "react-router-dom";
import "./App.scss";
import MainPage from "./components/pages/MainPage/MainPage";
import Dictionary from "./components/pages/dictionary/Dictionary";
import AddWord from "./components/pages/AddWord/AddWord";
import EditWord from "./components/pages/EditWord/EditWord";
import CheckWords from "./components/pages/CheckWords/CheckWords";
import Results from "./components/pages/Results/Results";

function App() {
  return (
    <>
      <Routes>
        <Route index element={<MainPage />} />
        <Route path="/dictionary" element={<Dictionary />} />
        <Route path="/new-word" element={<AddWord />} />
        <Route path="/edit-word" element={<EditWord />} />
        <Route path="/check" element={<CheckWords />} />
        <Route path="/results" element={<Results />} />
      </Routes>
    </>
  );
}

export default App;
