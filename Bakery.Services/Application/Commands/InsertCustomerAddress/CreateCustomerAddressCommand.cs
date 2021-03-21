using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.CustomerAddress;
using MediatR;

namespace Bakery.Services.Application.Commands.InsertCustomerAddress
{
    public class CreateCustomerAddressCommand : IRequest<Result<CustomerAddressDto>>
    {
        public Bakery.Services.Application.Models.CustomerAddress.CreateCustomerAddress Request { get; set; }
    }
}