using System;
using System.Collections.Generic;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        private readonly IRepository<Movie> _repository;
        private readonly IRepository<MovieGenre> _genreRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateMovieModel Model { get; set; }

        public CreateMovieCommand(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IRepository<Movie> repository,
            IRepository<MovieGenre> genreRepo
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _genreRepo = genreRepo;
        }
        public void Handle()
        {
            var movie = _repository.Get(x => x.Title == Model.Title && x.Year == Model.Year);
            if(movie is not null)
                throw new InvalidOperationException($"Movie {Model.Title} already exists.");
            movie = _mapper.Map<Movie>(Model);
            // var director = _directorRepo.Get(dir => dir.Id == Model.DirectorId);
            // if(director is null)
            //     throw new InvalidOperationException("Director not found.");
            // movie.Director = director;
            _repository.Add(movie);
            foreach (var genre in Model.Genres)
            {
                _genreRepo.Add(new MovieGenre{Genre = new Genre{Id = genre.GenreId}, Movie = movie});
            }
            _unitOfWork.Commit();
        }
    }

    public class CreateMovieModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public IEnumerable<GenreModel> Genres { get; set; }
    }

    public class GenreModel
    {
        public int GenreId { get; set; }
    }
}