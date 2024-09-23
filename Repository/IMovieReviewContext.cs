using Microsoft.EntityFrameworkCore;
using CineCritique.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CineCritique.Repository
{
    public interface IMovieReviewContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}