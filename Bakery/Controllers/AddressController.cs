using System.Threading.Tasks;
using Bakery.Services.Application.Commands.InsertCustomerAddress;
using Bakery.Services.Application.Models.CustomerAddress;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCustomerAddress(CreateCustomerAddress request)
        {
            if (request == null || !ModelState.IsValid)
                return BadRequest("Incorrect input");
            
            var result =  await _mediator.Send(new CreateCustomerAddressCommand
            {
                Request = request
            });

            if (!result.IsSuccessful)
                return BadRequest(result.Exception);

            return Ok(result.Value);
        }
    }
}