using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using MediatR;

namespace Bakery.Services.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Result<CustomerDto>>
    {
        public Models.Customer.CreateCustomer Request { get; set; }
    }
}