using System;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.API.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}