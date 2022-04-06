using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Director : Person
    {
        public List<Movie> DirectedMovies { get; set; }
    }
}
