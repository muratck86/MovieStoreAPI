using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.Business.Common;
using MovieStore.API.DataAccess.EntityFramework;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int CustomerId { get; set; }
        public UpdateCustomerModel Model { get; set; }


        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(e => e.Id == CustomerId && e.IsDeleted == false);
            if(customer is null)
                throw new InvalidOperationException($"Customer id {CustomerId} not found.");
            PropertyUpdateHelper.Update(customer, Model);
            _context.Customers.Update(customer);
            _context.SaveChanges();
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
        public List<Int32> FavouriteGenreIds { get; set; }

    }
}