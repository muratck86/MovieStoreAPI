using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MovieStore.API.Business.TokenOperations;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IRepository<Customer> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _repository = repository;
        }

        public CreateTokenModel Model { get; set; }
        public Token Handle()
        {
            var customer = _repository.Get(c => c.Email == Model.Email && c.Password == Model.Password);
            if(customer is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(customer);
                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _unitOfWork.Commit();
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