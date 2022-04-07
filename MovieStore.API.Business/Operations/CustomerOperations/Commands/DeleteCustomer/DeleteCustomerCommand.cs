using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCustomerCommand(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Customer> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public int CustomerId { get; set; }
        public void Handle()
        {
            var customer = _repository.Get(e => e.Id == CustomerId);
            if(customer is null)
                throw new InvalidOperationException($"Customer id {CustomerId} not found.");
            
            _repository.Delete(customer);
            _unitOfWork.Commit();
        }
    }

    public class DeleteCustomerModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
    }
}