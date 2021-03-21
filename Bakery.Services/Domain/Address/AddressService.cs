using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bakery.Core;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Application.Models.CustomerAddress;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Services.Domain.Address
{
    public class AddressService : IAddressService
    {
        private AppDbContext _db;
        private IAddressRepository _repository;
        private IMapper _mapper;
        public AddressService(AppDbContext db, IAddressRepository repository, IMapper mapper)
        {
            _db = db;
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<Result<IEnumerable<NearestAddressDto>>> GetNearest(Location location, float distance)
        {
            if (location == null)
                return Result<IEnumerable<NearestAddressDto>>.Fail("Incorrect Input");
            try
            {
                var result = await _repository.GetNearest(new NearestLocation()
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    Distance = distance
                });
                
                return Result<IEnumerable<NearestAddressDto>>.Create(result);
            }
            catch(Exception ex)
            {
                return Result<IEnumerable<NearestAddressDto>>.Fail(ex.Message);
            }
            
        }

        public async Task<Result<AddressDto>> Create(Core.Entities.Customer customer,CreateCustomerAddress request)
        {
            var address = new Core.Entities.CustomerAddress(customer, request.Latitude, request.Longitude, request.AddressName);
            try
            {
                _db.CustomerAddress.Add(address);
                await _db.SaveChangesAsync();
                
                return Result<AddressDto>.Create(_mapper.Map<AddressDto>(address)); 
            }
            catch (Exception ex)
            {
                return Result<AddressDto>.Fail(ex.Message);
            }
        }
    }
}