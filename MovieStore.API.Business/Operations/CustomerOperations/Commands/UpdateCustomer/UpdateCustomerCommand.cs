using System;
using System.Collections.Generic;
using AutoMapper;
using MovieStore.API.Business.Common;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public int CustomerId { get; set; }
        public UpdateCustomerModel Model { get; set; }

        public UpdateCustomerCommand(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Customer> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public void Handle()
        {
            var customer = _repository.Get(e => e.Id == CustomerId && e.IsDeleted == false);
            if(customer is null)
                throw new InvalidOperationException($"Customer id {CustomerId} not found.");
            PropertyUpdateHelper.Update(customer, Model);
            _repository.Update(customer);
            _unitOfWork.Commit();
        }
    }

    public class UpdateCustomerModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BirthCity { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<int> FavouriteGenreIds { get; set; }

    }
}