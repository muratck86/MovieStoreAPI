using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int CustomerId { get; set; }
        public CustomerDetailModel Handle()
        {
            var customer = _context.Customers
                .Include(c => c.CustomerGenres)
                .ThenInclude(cg => cg.Genre)
                .SingleOrDefault(c => c.Id == CustomerId);
            if(customer is null)
                throw new InvalidOperationException("Customer not found.");
            return _mapper.Map<CustomerDetailModel>(customer);
        }
    }

    public class CustomerDetailModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public List<string> FavouriteGenres { get; set; }
    }
}