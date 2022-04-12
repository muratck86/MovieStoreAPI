using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.DeleteCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.UpdateCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomerDetail;
using MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomers;
using MovieStore.API.DataAccess.EntityFramework;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CustomerController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("getall")]
        [HttpGet]
        public IActionResult GetCustomers()
        {
            GetCustomersQuery query = new GetCustomersQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [Route("get/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = id;
            var result = query.Handle();
            return Ok(result);
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add([FromBody] CreateCustomerModel model)
        {
            CreateCustomerCommand comand = new CreateCustomerCommand(_context, _mapper);
            comand.Model = model;
            comand.Handle();
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context, _mapper);
            command.CustomerId = id;
            command.Handle();
            return Ok();
        }

        [Route("update/{id}")]
        [HttpPost]
        public IActionResult Update(int id, [FromBody] UpdateCustomerModel model)
        {
            UpdateCustomerCommand command = new UpdateCustomerCommand(_context, _mapper);
            command.CustomerId = id;
            command.Model = model;
            command.Handle();
            return Ok();
        }
    }
}