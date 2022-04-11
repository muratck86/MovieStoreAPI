namespace MovieStore.API.Domain.Entities
{
    public class PersonRole : BaseEntity
    {
        public Person Person { get; set; }
        public MovieRole MovieRole { get; set; }
    }
}