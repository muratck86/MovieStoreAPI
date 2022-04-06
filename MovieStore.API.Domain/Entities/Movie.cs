using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public Director Director { get; set; }
        public List<Player> Players { get; set; }
        public double Price { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
