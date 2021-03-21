using System.Collections.Generic;
using System.Threading.Tasks;
using Bakery.Services.Application.Models.CustomerAddress;

namespace Bakery.Services.Domain.Address
{
    public interface IAddressRepository
    {
        Task<IEnumerable<NearestAddressDto>> GetNearest(NearestLocation location);
    }
}