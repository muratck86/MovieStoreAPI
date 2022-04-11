using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PlayerOperations.Commands.UpdateMovies
{
    public class UpdateMoviesCommand
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<PlayerMovie> _playerMovieRepository;
        private readonly IRepository<Movie> _movieRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlayerMovieUpdateModel Model { get; set; }

        public UpdateMoviesCommand(
            IRepository<Person> personRepository,
            IRepository<PlayerMovie> playerMovieRepository,
            IRepository<Movie> movieRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _playerMovieRepository = playerMovieRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public void Handle()
        {
            var player = _personRepository.Get(x => x.Id == Model.PlayerId && x.IsDeleted == false);
            if(player is null)
                throw new InvalidOperationException($"Player {Model.PlayerId} does not exist.");

            var playerMovies = _playerMovieRepository.GetAll(x => x.Player.Id == Model.PlayerId);
            if (playerMovies != null && playerMovies.Count() > 0)
            foreach (var movie in playerMovies)
            {
                var playerMovie = _playerMovieRepository.Get(x => x.Player == player && x.ActedMovie == movie.ActedMovie);
                _playerMovieRepository.Delete(playerMovie);
            }
            var movies = _movieRepository.GetAll(x => Model.MovieIds.Contains(x.Id));
            foreach (var movieId in Model.MovieIds)
            {
                var movie = _movieRepository.Get(x => x.Id == movieId && x.IsDeleted == false);
                if (movie is null)
                    throw new InvalidOperationException($"Movie {movie.Id} does not exist.");
                _playerMovieRepository.Add(new PlayerMovie {Player=player, ActedMovie=movie});
            }
            _unitOfWork.Commit();
        }
    }

    public class PlayerMovieUpdateModel
    {
        public int PlayerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}