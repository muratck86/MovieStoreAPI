using System;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.DirectorOperations.Commands.AddMovie
{
    public class AddMovieCommand
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<DirectorMovie> _directorMovieRepository;
        private readonly IRepository<Movie> _movieRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public int DirectorId { get; set; }
        public int MovieId { get; set; }

        public AddMovieCommand(
            IRepository<Person> personRepository,
            IRepository<DirectorMovie> directorMovieRepository,
            IRepository<Movie> movieRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _directorMovieRepository = directorMovieRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public void Handle()
        {
            var director = _personRepository.Get(x => x.Id == DirectorId && x.IsDeleted == false);
            var movie = _movieRepository.Get(x => x.Id == MovieId && x.IsDeleted == false);
            if(director is null)
                throw new InvalidOperationException($"Director {DirectorId} does not exist.");
            if (movie is null)
                throw new InvalidOperationException($"Movie {MovieId} does not exist.");
            var directorMovie = _directorMovieRepository.Get(x => x.Director.Id == DirectorId && x.DirectedMovie.Id == MovieId);
            if (directorMovie is not null)
                throw new InvalidOperationException($"Director already has movie.");
            _directorMovieRepository.Add(directorMovie);
            _unitOfWork.Commit();
        }
    }
}