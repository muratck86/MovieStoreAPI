using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CreateCustomerModel Model { get; set; }



        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Email == Model.Email);
            if (customer is not null)
                throw new InvalidOperationException("This email is already bind to a Customer. Forgot your password?");

            customer = _mapper.Map<Customer>(Model);
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }

    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCity { get ; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<int> FavouriteGenreIds { get; set; }
    }
}