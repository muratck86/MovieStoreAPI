namespace MovieStore.API.Domain.Entities
{
    public class CustomerGenre : IEntity
    {
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}