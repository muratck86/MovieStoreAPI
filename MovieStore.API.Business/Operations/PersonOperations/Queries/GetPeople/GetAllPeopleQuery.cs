using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PersonOperations.Queries.GetPeople
{
    public class GetAllPeopleQuery
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<PersonRole> _personRoleRepository;
        private readonly IMapper _mapper;

        public GetAllPeopleQuery(IRepository<Person> personRepository, IRepository<PersonRole> personRoleRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _personRoleRepository = personRoleRepository;
            _mapper = mapper;
        }

        public List<PersonModel> Handle()
        {
            var people = _personRepository.GetAll(null).ToList<Person>();
            var peopleModels = _mapper.Map<List<PersonModel>>(people);
            foreach (var person in peopleModels)
            {
                var roles = _personRoleRepository.GetAll(r => r.Person.Id == person.Id);
                var strRoles = _mapper.Map<List<string>>(roles);
                person.Roles = strRoles;
            }
            return peopleModels;
        }
    }

    public class PersonModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
        public List<string> Roles {get; set; }
    }
}