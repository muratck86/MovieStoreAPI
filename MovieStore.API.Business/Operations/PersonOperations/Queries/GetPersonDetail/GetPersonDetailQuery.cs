using System;
using System.Collections.Generic;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PersonOperations.Queries.GetPersonDetail
{
    public class GetPersonDetailQuery
    {
        public int PersonId { get; set; }
        private readonly IRepository<Person> _repository;
        private readonly IMapper _mapper;

        public GetPersonDetailQuery(IRepository<Person> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public PersonDetailModel Handle()
        {
            var person = _repository.Get(c => c.Id == PersonId && c.IsDeleted == false);
            if(person is null)
                throw new InvalidOperationException("Person not found.");
            return _mapper.Map<PersonDetailModel>(person);
        }
    }

    public class PersonDetailModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
        public List<string> Roles { get; set; }
    }
}