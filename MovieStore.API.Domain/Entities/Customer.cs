using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Customer : Person
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Purchase> Purchases { get; set; }
        public List<Genre> FavouriteGenres { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}