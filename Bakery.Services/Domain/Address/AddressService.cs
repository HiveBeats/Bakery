using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bakery.Core;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.CustomerAddress;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Services.Domain.Address
{
    public class AddressService : IAddressService
    {
        private AppDbContext _db;
        private IAddressRepository _repository;
        public AddressService(AppDbContext db, IAddressRepository repository)
        {
            _db = db;
            _repository = repository;
        }


        public async Task<Result<IEnumerable<NearestAddressDto>>> GetNearest(Location location, float distance)
        {
            if (location == null)
                return Result<IEnumerable<NearestAddressDto>>.Fail("Incorrect Input");

            var result = await _repository.GetNearest(new NearestLocation()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Distance = distance
            });
            
            return Result<IEnumerable<NearestAddressDto>>.Create(result);
        }
    }
}