using CineCritique.Models;

namespace CineCritique.Repository
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> GetReviews();
        public Review GetReviewById(int reviewId);
        public Review AddReview(Review review);
        public Review UpdateReview(Review review);
        public void DeleteReview(int reviewId);
    }
}