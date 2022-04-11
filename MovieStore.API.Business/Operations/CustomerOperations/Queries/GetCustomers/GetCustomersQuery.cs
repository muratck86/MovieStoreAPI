using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        public int CustomerId { get; set; }
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public GetCustomersQuery(IRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<CustomersModel> Handle()
        {
            var customers = _repository.GetAll(x => x.IsDeleted == false).ToList<Customer>();
            return _mapper.Map<List<CustomersModel>>(customers);
        }
    }

    public class CustomersModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public List<string> FavouriteGenres { get; set; }
    }
}