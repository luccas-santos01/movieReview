using CineCritique.Models;
using Microsoft.EntityFrameworkCore;

namespace CineCritique.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        protected readonly IMovieReviewContext _context;

        public ReviewRepository(IMovieReviewContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetReviews()
        {
            return _context.Reviews.Include(r => r.User).ToList();
        }

        public Review GetReviewById(int reviewId)
        {
            var review = _context.Reviews.Find(reviewId) ?? throw new KeyNotFoundException($"Review with ID {reviewId} not found.");
            return review;
        }

        public Review AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

        public Review UpdateReview(Review review)
        {
            var existingReview = _context.Reviews.Find(review.ReviewId)
            ?? throw new KeyNotFoundException("Review não encontrada.");

            existingReview.Content = review.Content;
            existingReview.MovieId = review.MovieId;
            existingReview.Movie = review.Movie;

            _context.Reviews.Update(existingReview);
            _context.SaveChanges();

            return existingReview;
        }

        public void DeleteReview(int reviewId)
        {
            var review = _context.Reviews.Find(reviewId)
            ?? throw new KeyNotFoundException("Review não encontrada.");

            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}