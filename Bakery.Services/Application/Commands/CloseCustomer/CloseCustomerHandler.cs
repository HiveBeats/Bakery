using System.Threading;
using System.Threading.Tasks;
using Bakery.Core;
using Bakery.Services.Application.Models;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Domain.Customer;
using MediatR;

namespace Bakery.Services.Application.Commands.CloseCustomer
{
    public class CloseCustomerHandler : IRequestHandler<CloseCustomerCommand, Result<CustomerDto>>
    {
        private ICustomerService _customerService;
        private AppDbContext _db;
        public CloseCustomerHandler(ICustomerService customerService, AppDbContext db)
        {
            _customerService = customerService;
            _db = db;
        }
        
        public async Task<Result<CustomerDto>> Handle(CloseCustomerCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            var result = await _customerService.CloseCustomer(request.Request);
            
            if (!result.IsSuccessful)
                transaction.RollbackAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
                
            return result;
        }
    }
}