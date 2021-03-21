using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using MediatR;

namespace Bakery.Services.Application.Requests.GetNearestCustomers
{
    public class GetNearestCustomersRequest : IRequest<Result<AddressableCustomerDto>>
    {
        public Models.Customer.GetNearestCustomers Request { get; set; }
    }
}