using System;
using System.Threading.Tasks;
using Bakery.Services.Application.Commands.CloseCustomer;
using Bakery.Services.Application.Commands.CreateCustomer;
using Bakery.Services.Application.Commands.UpdateCustomer;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Application.Requests.GetCustomerDetail;
using Bakery.Services.Application.Requests.GetNearestCustomers;
using Bakery.Services.Domain.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Get/{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            if (id == null)
                return BadRequest("Incorrect input");
            
            var request = new GetCustomerDetail() { CustomerId = id };
            var result =  await _mediator.Send(new GetCustomerDetailRequest
            {
                Request = request
            });
            
            if (!result.IsSuccessful)
                return BadRequest(result.Exception);

            return Ok(result.Value);
        }
        
        [HttpGet]
        [Route("GetNearest")]
        public async Task<IActionResult> GetNearest([FromQuery]GetNearestCustomers request)
        {
            if (request == null || !ModelState.IsValid)
                return BadRequest("Incorrect input");
            
            var result =  await _mediator.Send(new GetNearestCustomersRequest
            {
                Request = request
            });
            
            if (!result.IsSuccessful)
                return BadRequest(result.Exception);
            
            return Ok(result.Value);
        }
        
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddCategory(CreateCustomer request)
        {
            if (request == null || !ModelState.IsValid)
                return BadRequest("Incorrect input");

            var result =  await _mediator.Send(new CreateCustomerCommand
            {
                Request = request
            });

            if (!result.IsSuccessful)
                return BadRequest(result.Exception);

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomer request)
        {
            if (request == null || !ModelState.IsValid)
                return BadRequest("Incorrect input");
            
            var result =  await _mediator.Send(new UpdateCustomerCommand
            {
                Request = request
            });
            
            if (!result.IsSuccessful)
                return BadRequest(result.Exception);

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("Close/{id:long}")]
        public async Task<IActionResult> CloseCustomer(long id)
        {
            var request = new CloseCustomer() { CustomerId = id };
            var result =  await _mediator.Send(new CloseCustomerCommand
            {
                Request = request
            });
            
            if (!result.IsSuccessful)
                return BadRequest(result.Exception);

            return Ok(result.Value);
        }
    }
}