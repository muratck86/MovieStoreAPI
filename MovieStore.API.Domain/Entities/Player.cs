using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Player : Person
    {
        public IEnumerable<int> MovieIds { get; set; }
        public IEnumerable<Movie> Movies { get; set; }

    }
}
