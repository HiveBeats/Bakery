using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
    public class GetNearestCustomersHandler : IRequestHandler<GetNearestCustomersRequest, Result<IEnumerable<AddressableCustomerDto>>>
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
        
        public async Task<Result<IEnumerable<AddressableCustomerDto>>> Handle(GetNearestCustomersRequest request, CancellationToken cancellationToken)
        {
            var location = new Location(){Latitude = request.Request.Latitude, Longitude = request.Request.Longitude};
            var addresses = await _addressService.GetNearest(location, request.Request.Distance);
            var dictionary = addresses.Value
                .GroupBy(x => x.CustomerId)
                .ToDictionary(x => x.Key, g => g.ToList());
            
            //todo: переместить в сервис  и разобраться с маппингами
            var customers = await _db.CustomerAddress.Include(x => x.Customer)
                .Where(l => dictionary.Keys.Contains(l.CustomerId))
                .AsNoTracking()
                .Select(x => new
                {
                    CustomerId = x.CustomerId, 
                    CustomerName = x.Customer.CustomerName, 
                    CustomerDescription = x.Customer.CustomerDescription, 
                    DateStart = x.Customer.DateStart, 
                    DateEnd = x.Customer.DateEnd, 
                    AddressId = x.AddressId
                })
                .ToListAsync(cancellationToken);

            var result = customers.Select(x => new AddressableCustomerDto()
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerDescription = null,
                DateStart = null,
                DateEnd = null,
                Address = dictionary[x.CustomerId].FirstOrDefault(a => a.AddressId == x.AddressId)
            });

            return Result<IEnumerable<AddressableCustomerDto>>.Create(result);
        }
    }
}