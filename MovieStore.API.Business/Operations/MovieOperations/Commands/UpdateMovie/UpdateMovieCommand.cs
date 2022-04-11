using System;
using AutoMapper;
using MovieStore.API.Business.Common;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly IRepository<Movie> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public int MovieId { get; set; }
        public UpdateMovieModel Model { get; set; }

        public UpdateMovieCommand(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Movie> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public void Handle()
        {
            var movie = _repository.Get(e => e.Id == MovieId && e.IsDeleted == false);
            if(movie is null)
                throw new InvalidOperationException($"Movie id {MovieId} not found.");
            PropertyUpdateHelper.Update(movie, Model);
            _repository.Update(movie);
            _unitOfWork.Commit();
        }
    }

    public class UpdateMovieModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
    }
}