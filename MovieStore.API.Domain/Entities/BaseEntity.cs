using System;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.API.Domain.Entities
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}