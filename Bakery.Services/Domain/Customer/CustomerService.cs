using System.Threading.Tasks;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;

namespace Bakery.Services.Domain.Customer
{
    public class CustomerService : ICustomerService
    {
        public Task<Result<CustomerDto>> CreateCustomer(CreateCustomer request)
        {
            throw new System.NotImplementedException();
        }
    }
}