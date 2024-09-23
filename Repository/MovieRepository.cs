using CineCritique.Models;
using Microsoft.EntityFrameworkCore;

namespace CineCritique.Repository
{
    public class MovieRepository : IMovieRepository
    {
        protected readonly IMovieReviewContext _context;

        public MovieRepository(IMovieReviewContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.Include(m => m.Reviews).ToList();
        }

        public Movie GetMovieById(int movieId)
        {
            var movie = _context.Movies.Include(m => m.Reviews).FirstOrDefault(m => m.MovieId == movieId)
            ?? throw new KeyNotFoundException($"Movie with ID {movieId} not found.");
            return movie;
        }

        public Movie AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie UpdateMovie(Movie movie)
        {
            var existingMovie = _context.Movies.Find(movie.MovieId)
            ?? throw new KeyNotFoundException("Filme não encontrado.");

            existingMovie.Title = movie.Title;
            existingMovie.Genre = movie.Genre;
            existingMovie.Image = movie.Image;
            existingMovie.ReleaseDate = movie.ReleaseDate;

            _context.Movies.Update(existingMovie);
            _context.SaveChanges();

            return existingMovie;
        }

        public void DeleteMovie(int movieId)
        {
            var movie = _context.Movies.Find(movieId)
            ?? throw new KeyNotFoundException("Filme não encontrado.");

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}