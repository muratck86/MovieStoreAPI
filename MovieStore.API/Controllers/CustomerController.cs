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
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(
            IRepository<Customer> repository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("getall")]
        [HttpGet]
        public IActionResult GetCustomers()
        {
            GetCustomersQuery query = new GetCustomersQuery(_repository, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [Route("get/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_repository, _mapper);
            query.CustomerId = id;
            var result = query.Handle();
            return Ok(result);
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add([FromBody] CreateCustomerModel model)
        {
            CreateCustomerCommand comand = new CreateCustomerCommand(_repository,_unitOfWork, _mapper);
            comand.Model = model;
            comand.Handle();
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_unitOfWork, _mapper, _repository);
            command.CustomerId = id;
            command.Handle();
            return Ok();
        }

        [Route("update/{id}")]
        [HttpPost]
        public IActionResult Update(int id, [FromBody] UpdateCustomerModel model)
        {
            UpdateCustomerCommand command = new UpdateCustomerCommand(_unitOfWork, _mapper, _repository);
            command.CustomerId = id;
            command.Model = model;
            command.Handle();
            return Ok();
        }
    }
}