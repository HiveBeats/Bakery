using FluentValidation;

namespace Bakery.Services.Application.Models.Customer
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomer>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.CustomerId).NotNull();
            RuleFor(x => x.CustomerName).NotNull().MaximumLength(255);
        }
    }
}