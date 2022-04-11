using System;
using System.Collections.Generic;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorMoviesQuery
    {
        public int PersonId { get; set; }
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;

        public GetDirectorMoviesQuery(IRepository<Person> personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public DirectorMoviesModel Handle()
        {
            var person = _personRepository.Get(c => c.Id == PersonId && c.IsDeleted == false);
            if(person is null)
                throw new InvalidOperationException("Person not found.");
            return _mapper.Map<DirectorMoviesModel>(person);
        }
    }

    public class DirectorMoviesModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
    }
}