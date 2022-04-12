using Microsoft.EntityFrameworkCore;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.DataAccess.EntityFramework
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options) {}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<CustomerGenre> CustomerGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Customer>()
            //     .HasMany(c => c.Genres)
            //     .WithMany(g => g.Customers)
            //     .UsingEntity(j => j.ToTable("CustomerGenre")
                    // j => j
                    //     .HasOne(cg => cg.Genre)
                    //     .WithMany(g => g.CustomerGenres)
                    //     .HasForeignKey(cg => cg.GenreId),
                    //     j => j
                    //     .HasOne(cg => cg.Customer)
                    //     .WithMany(c => c.CustomerGenres)
                    //     .HasForeignKey(cg => cg.CustomerId),
                    //     j =>
                    //     {
                    //         j.HasKey(k => new {k.CustomerId, k.GenreId});
                    //     }
                    // );
            modelBuilder.Entity<CustomerGenre>()
                .HasKey(k => new {k.CustomerId, k.GenreId});
            // modelBuilder.Entity<Customer>()
            //     .HasMany(e => e.Purchases)
            //     .WithOne(c => c.Customer);
            // modelBuilder.Entity<Customer>()
            //     .Ignore(c => c.Genres);
            // modelBuilder.Entity<Genre>()
            //     .Ignore(c => c.Customers);
            // modelBuilder.Entity<Purchase>()
            //     .Ignore(p => p.Customer);
            // modelBuilder.Entity<CustomerGenre>()
            //     .HasKey(x => new {x.CustomerId, x.GenreId});
            // modelBuilder.Entity<MovieGenre>()
            //     .HasKey(x => new {x.MovieId, x.GenreId});
            // modelBuilder.Entity<DirecorMovie>()
            //     .HasKey(x => new {x.DirectorId, x.MovieId});
            // modelBuilder.Entity<PlayerMovie>()
            //     .HasKey(x => new {x.PlayerId, x.MovieId});
            base.OnModelCreating(modelBuilder);
        }
    }
}