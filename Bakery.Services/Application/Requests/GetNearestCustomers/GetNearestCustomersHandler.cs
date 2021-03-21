using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bakery.Core;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Domain.Customer;
using MediatR;

namespace Bakery.Services.Application.Requests.GetNearestCustomers
{
    public class GetNearestCustomersHandler : IRequestHandler<GetNearestCustomersRequest, Result<CustomerDetailDto>>
    {
        private ICustomerService _customerService;
        private AppDbContext _db;
        private IMapper _mapper;
        
        public GetNearestCustomersHandler(ICustomerService customerService, AppDbContext db, IMapper mapper)
        {
            _customerService = customerService;
            _db = db;
            _mapper = mapper;
        }
        
        public Task<Result<CustomerDetailDto>> Handle(GetNearestCustomersRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}