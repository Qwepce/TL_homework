import { useState } from "react";
import "./App.css";
import ReviewForm from "./components/reviewForm/ReviewForm";
import UserReview from "./components/userReview/UserReview";
import type { ReviewData } from "./types/ReviewData";

export default function App() {
  const [dataFromForm, setDataFromForm] = useState<ReviewData | null>(null);

  const handleFormSubmit = ({ username, review, rating }: ReviewData) => {
    setDataFromForm({ username, review, rating });
  };

  return (
    <div className="app-container">
      <div className="review-form-container">
        <ReviewForm onSubmit={handleFormSubmit} />
      </div>
      <div className="review-display">
        {dataFromForm && (
          <UserReview
            rating={dataFromForm.rating}
            username={dataFromForm.username}
            review={dataFromForm.review}
          />
        )}
      </div>
    </div>
  );
}
