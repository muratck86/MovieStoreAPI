using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class MovieGenre : BaseEntity
    {
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}