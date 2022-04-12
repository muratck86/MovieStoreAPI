using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Movie> Movies { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public virtual List<CustomerGenre> CustomerGenres { get; set; }
    }
}