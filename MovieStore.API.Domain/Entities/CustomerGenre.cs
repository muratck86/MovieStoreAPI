using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class CustomerGenre : BaseEntity
    {
        public Customer Customer { get; set; }
        public Genre Genre { get; set; }
    }
}