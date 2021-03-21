using System.Threading.Tasks;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;

namespace Bakery.Services.Domain.Customer
{
    public interface ICustomerService
    {
        Task<Result<CustomerDto>> CreateCustomer(CreateCustomer request);
    }
}