using System;

namespace MovieStore.API.Domain.Entities
{
    public class Purchase : BaseEntity
    {
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}