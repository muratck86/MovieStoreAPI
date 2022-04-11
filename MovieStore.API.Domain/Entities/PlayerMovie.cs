using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class PlayerMovie : BaseEntity
    {
        public Person Player { get; set; }
        public Movie ActedMovie { get; set; }
    }
}
