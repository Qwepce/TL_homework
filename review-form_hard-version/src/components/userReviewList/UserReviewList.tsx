import type { ReviewData } from "../../types/ReviewData";
import UserReview from "../userReview/UserReview";

interface UserReviewListProps {
  reviews: ReviewData[];
}

export default function UserReviewList({ reviews }: UserReviewListProps) {
  const reviewsWithIDs = reviews.map((review) => ({
    ...review,
    id: crypto.randomUUID(),
  }));

  return (
    <ul>
      {reviewsWithIDs.map(({ id, username, rating, review }) => (
        <li key={id}>
          <UserReview username={username} rating={rating} review={review} />
        </li>
      ))}
    </ul>
  );
}
