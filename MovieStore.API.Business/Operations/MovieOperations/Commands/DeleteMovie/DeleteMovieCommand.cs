using System;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly IRepository<Movie> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteMovieCommand(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Movie> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public int MovieId { get; set; }
        public void Handle()
        {
            var movie = _repository.Get(e => e.Id == MovieId && e.IsDeleted == false);
            if(movie is null)
                throw new InvalidOperationException($"Movie id {MovieId} not found.");
            movie.IsDeleted = true;
            _repository.Update(movie);
            _unitOfWork.Commit();
        }
    }
}