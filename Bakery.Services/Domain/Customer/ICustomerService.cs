using System.Threading.Tasks;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;

namespace Bakery.Services.Domain.Customer
{
    public interface ICustomerService
    {
        Task<Result<CustomerDto>> CreateCustomer(CreateCustomer request);
        Task<Result<CustomerDto>> UpdateCustomer(UpdateCustomer request);
        Task<Result<CustomerDto>> CloseCustomer(CloseCustomer request);
        Task<Result<CustomerDto>> GetCustomer(GetCustomerDetail request);
    }
}