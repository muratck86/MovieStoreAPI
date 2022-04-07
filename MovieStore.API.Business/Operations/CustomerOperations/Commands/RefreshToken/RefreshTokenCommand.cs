using System;
using Microsoft.Extensions.Configuration;
using MovieStore.API.Business.TokenOperations;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IUnitOfWork unitOfWork, IConfiguration configuration, IRepository<Customer> repository)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _repository = repository;
        }
        public string RefreshToken { get; set; }
        public Token Handle()
        {
            var customer = _repository.Get(e => e.RefreshToken == RefreshToken && e.RefreshTokenExpireDate > DateTime.Now);
            if(customer is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(customer);
                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _unitOfWork.Commit();
                return token;
            }
            throw new InvalidOperationException("Invalid or Expired Refresh Token.");
        }
    }
}