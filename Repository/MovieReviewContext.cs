using Microsoft.EntityFrameworkCore;
using CineCritique.Models;

namespace CineCritique.Repository
{
    public class MovieReviewContext : DbContext, IMovieReviewContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public MovieReviewContext(DbContextOptions<MovieReviewContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("MOVIE_REVIEW_CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A variável de ambiente 'MOVIE_REVIEW_CONNECTION_STRING' não está definida.");
            }
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(user => user.Reviews)
                        .WithOne(review => review.User)
                        .HasForeignKey(review => review.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>()
                        .HasMany(movie => movie.Reviews)
                        .WithOne(review => review.Movie)
                        .HasForeignKey(review => review.MovieId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}