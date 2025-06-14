import { useState } from "react";
import "./App.css";
import ReviewForm from "./components/reviewForm/ReviewForm";
import UserReview from "./components/userReview/UserReview";
import type { ReviewData } from "./types/types";

export default function App() {
  const [formData, setFormData] = useState<ReviewData | null>(null);

  const handleFormSubmit = ({ username, review, rating }: ReviewData) => {
    setFormData({ username, review, rating });
  };

  return (
    <div className="app-container">
      <div className="review-form-container">
        <ReviewForm onSubmit={handleFormSubmit} />
      </div>
      {formData && (
        <div className="review-display">
          <UserReview
            rating={formData.rating}
            username={formData.username}
            review={formData.review}
          />
        </div>
      )}
    </div>
  );
}
