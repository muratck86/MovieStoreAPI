using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.Common.Enums;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommand
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCustomerModel Model { get; set; }

        public CreateCustomerCommand(
            IRepository<Customer> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Handle()
        {
            var customer = _repository.Get(x => x.Email == Model.Email);
            if (customer is not null)
                throw new InvalidOperationException("This email is already bind to a Customer. Forgot your password?");
            // if (Model.FavouriteGenreIds.Count() > 0)
            // {

            // }
            _repository.Add(customer);
            _unitOfWork.Commit();
        }
    }

    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirhDate { get; set; }
        public string BirthCity { get ; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<int> FavouriteGenreIds { get; set; }
    }
}