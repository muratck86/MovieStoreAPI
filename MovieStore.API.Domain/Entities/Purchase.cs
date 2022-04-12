using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.API.Domain.Entities
{
    public class Purchase : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}