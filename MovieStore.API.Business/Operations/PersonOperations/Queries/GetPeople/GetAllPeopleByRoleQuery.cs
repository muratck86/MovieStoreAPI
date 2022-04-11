using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PersonOperations.Queries.GetPeople
{
    public class GetAllPeopleByRoleQuery
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<PersonRole> _personRoleRepository;
        private readonly IMapper _mapper;
        public int RoleId { get; set; }
        public GetAllPeopleByRoleQuery(IRepository<Person> personRepository, IRepository<PersonRole> personRoleRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _personRoleRepository = personRoleRepository;
            _mapper = mapper;
        }

        public List<PeopleByRoleModel> Handle()
        {
            var peopleRoles = _personRoleRepository.GetAll(x => x.MovieRole.Id == RoleId);
            var people = _personRepository.GetAll(x => peopleRoles.Any(a => a.Id == x.Id));
            return _mapper.Map<List<PeopleByRoleModel>>(people);
        }
    }
        public class PeopleByRoleModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BirthCity { get; set; }
        public string BirthDate { get; set; }
    }
}