using Microsoft.EntityFrameworkCore;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.DataAccess.EntityFramework
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options) {}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CustomerGenre> CustomersGenres { get; set; }
        public DbSet<DirectorMovie> DirectorsMovies { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }
        public DbSet<PlayerMovie> PlayersMovies { get; set; }
        public DbSet<MovieRole> MovieRoles { get; set; }
        public DbSet<PersonRole> PersonRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}