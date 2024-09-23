using CineCritique.Models;

namespace CineCritique.Repository
{
    public interface IMovieRepository
    {
        public IEnumerable<Movie> GetMovies();
        public Movie GetMovieById(int movieId);
        public Movie AddMovie(Movie movie);
        public Movie UpdateMovie(Movie movie);
        public void DeleteMovie(int movieId);
    }
}