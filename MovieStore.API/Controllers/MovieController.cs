using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.API.Business.Operations.MovieOperations.Commands.CreateMovie;
using MovieStore.API.Business.Operations.MovieOperations.Commands.DeleteMovie;
using MovieStore.API.Business.Operations.MovieOperations.Commands.UpdateMovie;
using MovieStore.API.Business.Operations.MovieOperations.Queries.GetMovieDetail;
using MovieStore.API.Business.Operations.MovieOperations.Queries.GetMovies;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IRepository<Movie> _repository;
        private readonly IRepository<MovieGenre> _genreRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieController(IRepository<Movie> repository, IRepository<MovieGenre> genreRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _genreRepo = genreRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("getall")]
        [HttpGet]
        public IActionResult GetMovies()
        {
            GetMoviesQuery query = new GetMoviesQuery(_repository, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [Route("get/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            GetMovieDetailQuery query = new GetMovieDetailQuery(_repository, _mapper);
            query.MovieId = id;
            var result = query.Handle();
            return Ok(result);
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] CreateMovieModel model)
        {
            CreateMovieCommand comand = new CreateMovieCommand(_unitOfWork, _mapper, _repository, _genreRepo);
            comand.Model = model;
            comand.Handle();
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_unitOfWork, _mapper, _repository);
            command.MovieId = id;
            command.Handle();
            return Ok();
        }

        [Route("update/{id}")]
        [HttpPost]
        public IActionResult Update(int id, [FromBody] UpdateMovieModel model)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_unitOfWork, _mapper, _repository);
            command.MovieId = id;
            command.Model = model;
            command.Handle();
            return Ok();
        }

        // [Route("adddirector")]
        // [HttpPost]
        // public IActionResult AddDirector([FromBody] AddDirectingModel model)
        // {
        //     AddDirectingCommand addDirectingCommand = new AddDirectingCommand(_repository, _directorRepo, _unitOfWork);
        //     addDirectingCommand.Model = model;
        //     addDirectingCommand.Handle();
        //     return Ok();
        // }

        [Route("recover/{id}")]
        [HttpGet]
        public IActionResult Recover(int id)
        {
            //_repository.Undelete(id);
            return Ok();
        }
    }
}