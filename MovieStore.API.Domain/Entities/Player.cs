using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Player : Person
    {
        public List<Movie> ActedMovies { get; set; }
    }
}
