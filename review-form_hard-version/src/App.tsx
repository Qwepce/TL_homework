import { useEffect, useState } from "react";
import type { ReviewData } from "./types/types";
import ReviewForm from "./components/reviewForm/ReviewForm";
import UserReviewList from "./components/userReviewList/UserReviewList";
import "./App.css";

export default function App() {
  const [reviews, setReviews] = useState<ReviewData[]>(() => {
    const savedReviews = localStorage.getItem(`reviews`);
    return savedReviews ? JSON.parse(savedReviews) : [];
  });

  useEffect(() => {
    localStorage.setItem(`reviews`, JSON.stringify(reviews));
  }, [reviews]);

  const handleFormSubmit = (newReview: ReviewData) => {
    setReviews((previousReviews) => [newReview, ...previousReviews]);
  };

  return (
    <div className="app-container">
      <div className="review-form-container">
        <ReviewForm onSubmit={handleFormSubmit} />
      </div>
      {reviews.length > 0 && (
        <div className="review-display">
          <UserReviewList reviews={reviews} />
        </div>
      )}
    </div>
  );
}
