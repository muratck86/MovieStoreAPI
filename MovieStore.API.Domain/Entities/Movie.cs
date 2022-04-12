using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public virtual List<Director> Directors { get; set; }
        public virtual List<Player> Players { get; set; }
        public virtual List<Genre> Genres { get; set; }
    }
}
