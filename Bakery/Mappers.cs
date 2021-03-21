using AutoMapper;
using Bakery.Core.Entities;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Application.Models.CustomerAddress;

namespace Bakery
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDto, CustomerDetailDto>();
            CreateMap<CustomerAddress, CustomerAddressDto>().ReverseMap();
            CreateMap<Customer, AddressableCustomerDto>();
            
            CreateMap<GetNearestCustomers, Location>();
        }
    }
}