import type { ReviewData } from "../../types/types";
import UserReview from "../userReview/UserReview";

interface UserReviewListProps {
  reviews: ReviewData[];
}

type ReviewWithId = ReviewData & {
  id: string;
};

export default function UserReviewList({ reviews }: UserReviewListProps) {
  const reviewsWithIDs: ReviewWithId[] = reviews.map((review) => ({
    ...review,
    id: crypto.randomUUID(),
  }));

  return (
    <ul>
      {reviewsWithIDs.map((review) => (
        <li key={review.id}>
          <UserReview review={review} />
        </li>
      ))}
    </ul>
  );
}
