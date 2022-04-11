using System;
using AutoMapper;
using MovieStore.API.Business.Common;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PersonOperations.Commands.UpdatePerson
{
    public class UpdatePersonCommand
    {
        private readonly IRepository<Person> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public int PersonId { get; set; }
        public UpdatePersonModel Model { get; set; }

        public UpdatePersonCommand(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Person> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public void Handle()
        {
            var person = _repository.Get(e => e.Id == PersonId);
            if(person is null)
                throw new InvalidOperationException($"Person id {PersonId} not found.");
            PropertyUpdateHelper.Update(person, Model);
            _repository.Update(person);
            _unitOfWork.Commit();
        }
    }

    public class UpdatePersonModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthCity { get; set; }
        public DateTime BirthDate { get; set; }
    }
}