using System;
using AutoMapper;
using MovieStore.API.Common.Enums;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommand
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<PersonRole> _personroleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerCommand(
            IRepository<Customer> customerRepository,
            IRepository<Person> personRepository,
            IRepository<PersonRole> personroleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _personRepository = personRepository;
            _personroleRepository = personroleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CreateCustomerModel Model { get; set; }
        public void Handle()
        {
            var customer = _customerRepository.Get(x => x.Email == Model.Email);
            if(customer is not null)
                throw new InvalidOperationException("This email is already bind to a Customer. Forgot your password?");
            var person = _personRepository.Get(x => 
                x.Name == Model.Name && 
                x.LastName == Model.LastName && 
                x.BirthDate == Model.BirhDate && 
                x.BirthCity == Model.BirthCity);
            if(person is not null)
            {
                customer = _mapper.Map<Customer>(Model);
                customer.Id = person.Id;
                _customerRepository.Update(customer);
            }
            else
                _customerRepository.Add(customer);
            _personroleRepository.Add(new PersonRole{MovieRole= new MovieRole{Name = MovieRoles.Customer.ToString()}, Person=customer});
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
    }
}