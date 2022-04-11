using System;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PersonOperations.Commands.DeletePerson
{
    public class DeletePersonCommand
    {
        private readonly IRepository<Person> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePersonCommand(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Person> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public int PersonId { get; set; }
        public void Handle()
        {
            var person = _repository.Get(e => e.Id == PersonId && e.IsDeleted == false);
            if(person is null)
                throw new InvalidOperationException($"Person id {PersonId} not found.");
            person.IsDeleted = true;
            _repository.Update(person);
            _unitOfWork.Commit();
        }
    }
}