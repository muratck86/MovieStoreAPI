using System;

namespace MovieStore.API.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCity { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
