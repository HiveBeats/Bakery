using System.Threading;
using System.Threading.Tasks;
using Bakery.Services.Application.Models.Customer;
using MediatR;

namespace Bakery.Services.Application.Commands.CreateCustomer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        public Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}