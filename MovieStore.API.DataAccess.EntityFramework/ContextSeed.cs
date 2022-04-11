using System;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.API.Common.Enums;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.DataAccess.EntityFramework
{
    public class ContextSeed
    {
        public static async Task SeedGenresAsync(MovieStoreDbContext context)
        {
            if (context.Genres.Count() == 0)
            {
                foreach (var genre in Enum.GetValues(typeof(Genres)))
                {
                    await context.Genres.AddAsync(new Genre{Name=genre.ToString()});
                }
                await context.SaveChangesAsync();
            }
        }
        public static async Task SeedMovieRolesAsync(MovieStoreDbContext context)
        {
            if (context.MovieRoles.Count() == 0)
            {
                foreach (var role in Enum.GetValues(typeof(MovieRoles)))
                {
                    await context.MovieRoles.AddAsync(new MovieRole{Name=role.ToString()});
                }
                await context.SaveChangesAsync();
            }
        }
    }
}