using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public IEnumerable<int> DirectorIds { get; set; }
        public IEnumerable<Director> Directors { get; set; }
        public IEnumerable<int> PlayerIds { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<int> GenreIds { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
