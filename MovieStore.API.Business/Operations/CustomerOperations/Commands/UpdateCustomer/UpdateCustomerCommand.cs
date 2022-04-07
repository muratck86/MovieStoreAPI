using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            var customer = _repository.Get(e => e.Id == CustomerId);
            if(customer is null)
                throw new InvalidOperationException($"Customer id {CustomerId} not found.");
            customer.Name = PropertyUpdator.Update(customer.Name, Model.Name);
            customer.LastName = PropertyUpdator.Update(customer.LastName, Model.LastName);
            customer.Email = PropertyUpdator.Update(customer.Email, Model.Email);
            customer.Password = PropertyUpdator.Update(customer.Password, Model.Password);
            customer.BirthCity = PropertyUpdator.Update(customer.BirthCity, Model.City);
            var strDate = PropertyUpdator.Update(customer.BirthDate.Date.ToString(), Model.BirthDate.Date.ToString());
            customer.BirthDate = DateTime.Parse(strDate);

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
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
    }
}