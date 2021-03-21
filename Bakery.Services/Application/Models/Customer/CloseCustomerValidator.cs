using FluentValidation;

namespace Bakery.Services.Application.Models.Customer
{
    public class CloseCustomerValidator : AbstractValidator<CloseCustomer>
    {
        public CloseCustomerValidator()
        {
            RuleFor(x => x.CustomerId).NotNull();
        }
    }
}