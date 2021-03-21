using System.Threading;
using System.Threading.Tasks;
using Bakery.Core;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Domain.Customer;
using MediatR;

namespace Bakery.Services.Application.Commands.UpdateCustomer
{
    public class UpdateCustomerHanlder: IRequestHandler<UpdateCustomerCommand, Result<CustomerDto>>
    {
        private readonly ICustomerService _customerService;
        private readonly AppDbContext _ctx;

        public UpdateCustomerHanlder(ICustomerService service, AppDbContext ctx)
        {
            _customerService = service;
            _ctx = ctx;
        }
        
        public async Task<Result<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _ctx.Database.BeginTransactionAsync(cancellationToken);
            var result = await _customerService.UpdateCustomer(request.Request);
            
            if (!result.IsSuccessful)
                transaction.RollbackAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
                
            return result;
        }
    }
}