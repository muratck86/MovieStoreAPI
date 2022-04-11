using System;
using System.Collections.Generic;
using AutoMapper;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Operations.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        public int MovieId { get; set; }
        private readonly IRepository<Movie> _repository;
        private readonly IMapper _mapper;

        public GetMovieDetailQuery(IRepository<Movie> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public MovieDetailModel Handle()
        {
            var movie = _repository.Get(c => c.Id == MovieId && c.IsDeleted == false);
            if(movie is null)
                throw new InvalidOperationException("Movie not found.");
            return _mapper.Map<MovieDetailModel>(movie);
        }
    }

    public class MovieDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public List<string> Directors { get; set; }
        public List<string> Cast { get; set; }
        public List<string> Genres { get; set; }


    }
}