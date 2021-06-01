using System;
using System.Threading.Tasks;
using AutoMapper;
using Bakery.Core;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;

namespace Bakery.Services.Domain.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result<CustomerDto>> CreateCustomer(CreateCustomer request)
        {
            var customer = new Core.Entities.Customer(request.Name, request.Desc);
            try
            {
                _db.Customer.Add(customer);
                await _db.SaveChangesAsync();

                return Result<CustomerDto>.Create(_mapper.Map<CustomerDto>(customer));
            }
            catch (Exception ex)
            {
                return Result<CustomerDto>.Fail(ex.Message);
            }
        }

        public async Task<Result<CustomerDto>> UpdateCustomer(UpdateCustomer request)
        {
            try
            {
                var customer = await _db.Customer.FindAsync(request.CustomerId);
                if (customer == null)
                    return Result<CustomerDto>.Fail(Exceptions.NotFoundException);

                customer.UpdateNameAndDesc(request.CustomerName, request.CustomerDesc);

                return Result<CustomerDto>.Create(_mapper.Map<CustomerDto>(customer));
            }
            catch (Exception e)
            {
                return Result<CustomerDto>.Fail(e.Message);
            }
        }

        public async Task<Result<CustomerDto>> CloseCustomer(CloseCustomer request)
        {
            try
            {
                var customer = await _db.Customer.FindAsync(request.CustomerId);
                if (customer == null)
                    return Result<CustomerDto>.Fail(Exceptions.NotFoundException);

                customer.Close();

                return Result<CustomerDto>.Create(_mapper.Map<CustomerDto>(customer));
            }
            catch (Exception e)
            {
                return Result<CustomerDto>.Fail(e.Message);
            }
        }

        public async Task<Result<CustomerDto>> GetCustomer(GetCustomerDetail request)
        {
            try
            {
                var customer = await _db.Customer.FindAsync(request.CustomerId);
                if (customer == null)
                    return Result<CustomerDto>.Fail(Exceptions.NotFoundException);

                return Result<CustomerDto>.Create(_mapper.Map<CustomerDto>(customer));
            }
            catch (Exception e)
            {
                return Result<CustomerDto>.Fail(e.Message);
            }
        }
    }
}