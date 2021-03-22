using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bakery.Core;
using Bakery.Core.Entities;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Application.Models.CustomerAddress;
using Bakery.Services.Domain.Address;
using Bakery.Services.Domain.Customer;
using MediatR;

namespace Bakery.Services.Application.Commands.InsertCustomerAddress
{
    public class CreateCustomerAddressHandler : IRequestHandler<CreateCustomerAddressCommand, Result<CustomerAddressDto>>
    {
        private AppDbContext _db;
        private IAddressService _addressService;
        private ICustomerService _customerService;
        private IMapper _mapper;
        
        public CreateCustomerAddressHandler(AppDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        
        public async Task<Result<CustomerAddressDto>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var createRequest = request.Request;
            
            await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            var customer = await _db.Customer.FindAsync(request.Request.CustomerId);
            if (customer == null)
                return Result<CustomerAddressDto>.Fail(Exceptions.NotFoundException);
            
            try
            {
                var address = new CustomerAddress(customer, createRequest.Latitude, createRequest.Longitude, createRequest.AddressName);
                await _db.CustomerAddress.AddAsync(address, cancellationToken);

                await _db.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return Result<CustomerAddressDto>.Create(_mapper.Map<CustomerAddressDto>(address));
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<CustomerAddressDto>.Fail(ex.Message);
            }
        }
    }
}