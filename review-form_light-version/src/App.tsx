import { useState } from "react";
import "./App.css";
import ReviewForm from "./components/reviewForm/ReviewForm";
import UserReview from "./components/userReview/UserReview";
import type { ReviewData } from "./types/types";

export default function App() {
  const [review, setReview] = useState<ReviewData | null>(null);

  const handleFormSubmit = (review: ReviewData): void => {
    setReview(review);
  };

  return (
    <div className="app-container">
      <div className="review-form-container">
        <ReviewForm onSubmit={handleFormSubmit} />
      </div>
      {review && (
        <div className="review-display">
          <UserReview review={review} />
        </div>
      )}
    </div>
  );
}
