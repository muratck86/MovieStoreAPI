using Microsoft.EntityFrameworkCore;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.DataAccess.EntityFramework
{
    public interface IMovieStoreDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Director> Directors { get; set; }


        int SaveChanges();
    }
}