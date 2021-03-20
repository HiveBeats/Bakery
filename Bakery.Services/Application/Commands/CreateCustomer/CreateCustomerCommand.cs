using Bakery.Services.Application.Models.Customer;
using MediatR;

namespace Bakery.Services.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CustomerDto>
    {
        
    }
}