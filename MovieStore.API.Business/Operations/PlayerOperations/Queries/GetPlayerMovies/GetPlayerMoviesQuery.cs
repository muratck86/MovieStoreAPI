using System;
using System.Collections.Generic;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PlayerOperations.Queries.GetPlayerDetail
{
    public class GetPlayerMoviesQuery
    {
        public int PersonId { get; set; }
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;

        public GetPlayerMoviesQuery(IRepository<Person> personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public PlayerMoviesModel Handle()
        {
            var person = _personRepository.Get(c => c.Id == PersonId && c.IsDeleted == false);
            if(person is null)
                throw new InvalidOperationException("Person not found.");
            return _mapper.Map<PlayerMoviesModel>(person);
        }
    }

    public class PlayerMoviesModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
    }
}