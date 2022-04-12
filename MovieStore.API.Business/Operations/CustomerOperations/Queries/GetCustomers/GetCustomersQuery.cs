using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<CustomersModel> Handle()
        {
            var customers = _context.Customers.ToList<Customer>();
            return _mapper.Map<List<CustomersModel>>(customers);
        }
    }

    public class CustomersModel
    {
        public string FullName { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
    }
}