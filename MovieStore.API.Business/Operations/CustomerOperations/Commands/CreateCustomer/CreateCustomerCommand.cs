using System;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommand
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerCommand(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Customer> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public CreateCustomerModel Model { get; set; }
        public void Handle()
        {
            var customer = _repository.Get(x => x.Email == Model.Email);
            if(customer is not null)
                throw new InvalidOperationException("This email is already used. Forgot your password?");
            customer = _mapper.Map<Customer>(Model);
            _repository.Add(customer);
            _unitOfWork.Commit();
        }
    }

    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}