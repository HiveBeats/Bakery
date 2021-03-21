using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using MediatR;

namespace Bakery.Services.Application.Commands.CloseCustomer
{
    public class CloseCustomerCommand : IRequest<Result<CustomerDto>>
    {
        public Models.Customer.CloseCustomer Request { get; set; }
    }
}