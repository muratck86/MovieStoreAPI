using System;
using System.Collections.Generic;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        public int CustomerId { get; set; }
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public GetCustomerDetailQuery(IRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public CustomerDetailModel Handle()
        {
            var customer = _repository.Get(c => c.Id == CustomerId);
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