using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.DeleteCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.UpdateCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomerDetail;
using MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomers;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<PersonRole> _personroleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(
            IRepository<Customer> customerRepository, 
            IRepository<Person> personRepository, 
            IRepository<PersonRole> personroleRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _personRepository = personRepository;
            _personroleRepository = personroleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("getall")]
        [HttpGet]
        public IActionResult GetCustomers()
        {
            GetCustomersQuery query = new GetCustomersQuery(_customerRepository, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [Route("get/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_customerRepository, _mapper);
            query.CustomerId = id;
            var result = query.Handle();
            return Ok(result);
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerModel model)
        {
            CreateCustomerCommand comand = new CreateCustomerCommand(_customerRepository,_personRepository,_personroleRepository,_unitOfWork, _mapper);
            comand.Model = model;
            comand.Handle();
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_unitOfWork, _mapper, _customerRepository);
            command.CustomerId = id;
            command.Handle();
            return Ok();
        }

        [Route("update/{id}")]
        [HttpPost]
        public IActionResult Update(int id, [FromBody] UpdateCustomerModel model)
        {
            UpdateCustomerCommand command = new UpdateCustomerCommand(_unitOfWork, _mapper, _customerRepository);
            command.CustomerId = id;
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