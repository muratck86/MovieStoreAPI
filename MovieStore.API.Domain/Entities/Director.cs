using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Director : Person
    {
        public IEnumerable<int> DirectedMovieIds { get; set; }
        public IEnumerable<Movie> DirectedMovies { get; set; }
    }
}
