using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Director : Person
    {
        public virtual List<Movie> DirectedMovies { get; set; }
    }
}
