using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MovieStore.API.Business.TokenOperations;
using MovieStore.API.DataAccess.EntityFramework;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IMovieStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public string RefreshToken { get; set; }
        public Token Handle()
        {
            var customer = _context.Customers.SingleOrDefault(e => e.RefreshToken == RefreshToken && e.RefreshTokenExpireDate > DateTime.Now);
            if(customer is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(customer);
                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
            }
            throw new InvalidOperationException("Invalid or Expired Refresh Token.");
        }
    }
}