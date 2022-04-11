using System;
using System.Collections.Generic;

namespace MovieStore.API.Domain.Entities
{
    public class Customer : Person
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        public IEnumerable<int> PurchaseIds { get; set; }
        public IEnumerable<Purchase> Purchases { get; set; }
        public IEnumerable<int> FavouriteGenreIds { get; set; }
        public IEnumerable<Genre> FavouriteGenres { get; set; }
    }
}