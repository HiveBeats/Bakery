using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bakery.Core;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Application.Models.CustomerAddress;
using Bakery.Services.Domain.Address;
using Bakery.Services.Domain.Customer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Services.Application.Requests.GetNearestCustomers
{
    public class GetNearestCustomersHandler : IRequestHandler<GetNearestCustomersRequest, Result<AddressableCustomerDto>>
    {
        private ICustomerService _customerService;
        private AppDbContext _db;
        private IMapper _mapper;
        private IAddressService _addressService;
        public GetNearestCustomersHandler(ICustomerService customerService, AppDbContext db, IMapper mapper, IAddressService addressService)
        {
            _customerService = customerService;
            _db = db;
            _mapper = mapper;
            _addressService = addressService;
        }
        
        public async Task<Result<AddressableCustomerDto>> Handle(GetNearestCustomersRequest request, CancellationToken cancellationToken)
        {
            var location = new Location(){Latitude = request.Request.Latitude, Longitude = request.Request.Longitude};
            var addresses = await _addressService.GetNearest(location, request.Request.Distance);
            var ids = addresses.Value.Select(x => x.CustomerId).ToHashSet();
            //todo: переместить в сервис  и разобраться с маппингами
            var customers = await _db.Customer
                .Where(l => ids.Contains(l.CustomerId))
                .ToListAsync();
            
            
            throw new NotImplementedException();
        }
    }
}