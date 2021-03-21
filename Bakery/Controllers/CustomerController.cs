using System.Threading.Tasks;
using Bakery.Services.Application.Commands.CreateCustomer;
using Bakery.Services.Application.Models.Customer;
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
    }
}