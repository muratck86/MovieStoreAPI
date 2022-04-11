using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.API.Business.Operations.PersonOperations.Commands.CreatePerson;
using MovieStore.API.Business.Operations.PersonOperations.Commands.DeletePerson;
using MovieStore.API.Business.Operations.PersonOperations.Commands.UpdatePerson;
using MovieStore.API.Business.Operations.PersonOperations.Queries.GetPeople;
using MovieStore.API.Business.Operations.PersonOperations.Queries.GetPersonDetail;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class CrewController : ControllerBase
    {
        private readonly IRepository<Person> _repository;
        private readonly IRepository<PersonRole> _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CrewController(IRepository<Person> repository, IRepository<PersonRole> roleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            GetAllPeopleQuery query = new GetAllPeopleQuery(_repository,_roleRepository, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [Route("getallbyrole/{id}")]
        [HttpGet]
        public IActionResult GetAllByRole(int id)
        {
            GetAllPeopleByRoleQuery query = new GetAllPeopleByRoleQuery(_repository,_roleRepository, _mapper);
            query.RoleId = id;
            var result = query.Handle();
            return Ok(result);
        }

        [Route("get/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            GetPersonDetailQuery query = new GetPersonDetailQuery(_repository, _mapper);
            query.PersonId = id;
            var result = query.Handle();
            return Ok(result);
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] CreatePersonModel model)
        {
            CreatePersonCommand comand = new CreatePersonCommand(_unitOfWork, _mapper, _repository, _roleRepository);
            comand.Model = model;
            comand.Handle();
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeletePersonCommand command = new DeletePersonCommand(_unitOfWork, _mapper, _repository);
            command.PersonId = id;
            command.Handle();
            return Ok();
        }

        [Route("update/{id}")]
        [HttpPost]
        public IActionResult Update(int id, [FromBody] UpdatePersonModel model)
        {
            UpdatePersonCommand command = new UpdatePersonCommand(_unitOfWork, _mapper, _repository);
            command.PersonId = id;
            command.Model = model;
            command.Handle();
            return Ok();
        }

        [Route("recover/{id}")]
        [HttpGet]
        public IActionResult Recover(int id)
        {
            //_repository.Undelete(id);
            return Ok();
        }
    }
}