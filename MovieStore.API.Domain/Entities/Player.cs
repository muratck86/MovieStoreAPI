using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Player : Person
    {
        public virtual List<Movie> Movies { get; set; }

    }
}
