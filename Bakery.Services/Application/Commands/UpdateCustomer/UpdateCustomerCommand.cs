using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using MediatR;

namespace Bakery.Services.Application.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Result<CustomerDto>>
    {
        public Models.Customer.UpdateCustomer Request { get; set; }
    }
}