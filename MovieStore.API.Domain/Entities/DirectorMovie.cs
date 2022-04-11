using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class DirectorMovie : BaseEntity
    {
        public Person Director { get; set; }
        public Movie DirectedMovie { get; set; }
    }
}
