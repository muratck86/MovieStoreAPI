using System;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework;

namespace MovieStore.API.Business.Operations.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int CustomerId { get; set; }
        public DeleteCustomerCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(e => e.Id == CustomerId);
            if(customer is null)
                throw new InvalidOperationException($"Customer id {CustomerId} not found.");
            customer.IsDeleted = true;
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
    }
}