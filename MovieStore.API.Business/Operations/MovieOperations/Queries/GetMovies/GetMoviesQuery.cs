using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        public int MovieId { get; set; }
        private readonly IRepository<Movie> _repository;
        private readonly IMapper _mapper;

        public GetMoviesQuery(IRepository<Movie> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<MoviesModel> Handle()
        {
            var movies = _repository.GetAll(null).ToList<Movie>();
            return _mapper.Map<List<MoviesModel>>(movies);
        }
    }

    public class MoviesModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public List<string> Genres { get; set; }
        public double Price { get; set; }
    }
}