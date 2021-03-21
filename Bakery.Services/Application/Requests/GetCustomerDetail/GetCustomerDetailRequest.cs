using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using MediatR;

namespace Bakery.Services.Application.Requests.GetCustomerDetail
{
    public class GetCustomerDetailRequest : IRequest<Result<CustomerDetailDto>>
    {
        public Models.Customer.GetCustomerDetail Request { get; set; }
    }
}