using System;
using AutoMapper;
using MovieStore.API.Common.Enums;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PersonOperations.Commands.CreatePerson
{
    public class CreatePersonCommand
    {
        private readonly IRepository<Person> _repository;
        private readonly IRepository<PersonRole> _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePersonCommand(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Person> repository, IRepository<PersonRole> roleRepository)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public CreatePersonModel Model { get; set; }
        public void Handle()
        {
            var person = _repository.Get(x => 
                x.Name == Model.Name &&
                x.LastName == Model.LastName &&
                x.BirthDate == Model.BirthDate &&
                x.BirthCity == Model.BirthCity);
            if(person is not null)
                throw new InvalidOperationException($"Person {Model.Name} {Model.LastName} already exists.");
            person = _mapper.Map<Person>(Model);
            _repository.Add(person);
            _roleRepository.Add(new PersonRole
            {
                MovieRole = new MovieRole
                {
                    Name = ((MovieRoles)Model.Role).ToString()
                }, Person = person
            });
            _unitOfWork.Commit();
        }
    }

    public class CreatePersonModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCity { get; set; }
        public int Role { get; set; }
    }
}