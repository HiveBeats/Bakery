using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bakery.Core;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Domain.Customer;
using MediatR;

namespace Bakery.Services.Application.Requests.GetCustomerDetail
{
    public class GetCustomerDetailHandler : IRequestHandler<GetCustomerDetailRequest, Result<CustomerDetailDto>>
    {
        private ICustomerService _customerService;
        private AppDbContext _db;
        private IMapper _mapper;
        public GetCustomerDetailHandler(ICustomerService customerService, AppDbContext db, IMapper mapper)
        {
            _customerService = customerService;
            _db = db;
            _mapper = mapper;
        }


        public async Task<Result<CustomerDetailDto>> Handle(GetCustomerDetailRequest request, CancellationToken cancellationToken)
        {
            var customerResult = await _customerService.GetCustomer(request.Request);
            if (!customerResult.IsSuccessful)
                return Result<CustomerDetailDto>.Fail(customerResult.Exception);
            
            var result = _mapper.Map<CustomerDetailDto>(customerResult.Value);
            //todo: addresses, discounts
            return Result<CustomerDetailDto>.Create(result);
        }
    }
}