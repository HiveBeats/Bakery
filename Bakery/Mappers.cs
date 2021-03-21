using AutoMapper;
using Bakery.Core.Entities;
using Bakery.Services.Application.Models.Customer;

namespace Bakery
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDto, CustomerDetailDto>();
        }
    }
}