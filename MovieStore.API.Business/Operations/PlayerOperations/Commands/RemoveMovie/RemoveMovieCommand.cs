using System;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.PlayerOperations.Commands.DeleteMovie
{
    public class RemoveMovieCommand
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<PlayerMovie> _playerMovieRepository;
        private readonly IRepository<Movie> _movieRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public int PlayerId { get; set; }
        public int MovieId { get; set; }

        public RemoveMovieCommand(
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
            var player = _personRepository.Get(x => x.Id == PlayerId && x.IsDeleted == false);
            var movie = _movieRepository.Get(x => x.Id == MovieId && x.IsDeleted == false);
            if(player is null)
                throw new InvalidOperationException($"Player {PlayerId} does not exist.");
            if (movie is null)
                throw new InvalidOperationException($"Movie {MovieId} does not exist.");
            var playerMovie = _playerMovieRepository.Get(x => x.Player.Id == PlayerId && x.ActedMovie.Id == MovieId);
            if (playerMovie is null)
                throw new InvalidOperationException($"Player already does not have movie.");
            _playerMovieRepository.Delete(playerMovie);
            _unitOfWork.Commit();
        }
    }
}