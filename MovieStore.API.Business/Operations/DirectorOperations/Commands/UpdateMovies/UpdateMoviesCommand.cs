using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.DirectorOperations.Commands.UpdateMovies
{
    public class UpdateMoviesCommand
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<DirectorMovie> _directorMovieRepository;
        private readonly IRepository<Movie> _movieRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DirectorMovieUpdateModel Model { get; set; }

        public UpdateMoviesCommand(
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
            var director = _personRepository.Get(x => x.Id == Model.DirectorId && x.IsDeleted == false);
            if(director is null)
                throw new InvalidOperationException($"Director {Model.DirectorId} does not exist.");

            var directorMovies = _directorMovieRepository.GetAll(x => x.Director.Id == Model.DirectorId);
            if (directorMovies != null && directorMovies.Count() > 0)
            foreach (var movie in directorMovies)
            {
                var directorMovie = _directorMovieRepository.Get(x => x.Director == director && x.DirectedMovie == movie.DirectedMovie);
                _directorMovieRepository.Delete(directorMovie);
            }
            var movies = _movieRepository.GetAll(x => Model.MovieIds.Contains(x.Id));
            foreach (var movieId in Model.MovieIds)
            {
                var movie = _movieRepository.Get(x => x.Id == movieId && x.IsDeleted == false);
                if (movie is null)
                    throw new InvalidOperationException($"Movie {movie.Id} does not exist.");
                _directorMovieRepository.Add(new DirectorMovie {Director=director, DirectedMovie=movie});
            }
            _unitOfWork.Commit();
        }
    }

    public class DirectorMovieUpdateModel
    {
        public int DirectorId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}