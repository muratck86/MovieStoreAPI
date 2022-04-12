using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MovieStore.API.Business.TokenOperations;
using MovieStore.API.DataAccess.EntityFramework;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IMovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public CreateTokenModel Model { get; set; }
        public Token Handle()
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Email == Model.Email && c.Password == Model.Password);
            if(customer is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(customer);
                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
            }
            throw new InvalidOperationException("Invalid Email or Password.");
        }
    }

        public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}