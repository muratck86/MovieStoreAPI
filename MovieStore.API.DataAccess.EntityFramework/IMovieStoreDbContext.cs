using Microsoft.EntityFrameworkCore;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.DataAccess.EntityFramework
{
    public interface IMovieStoreDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<Purchase> Purchases { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Director> Directors { get; set; }
        int SaveChanges();
    }
}