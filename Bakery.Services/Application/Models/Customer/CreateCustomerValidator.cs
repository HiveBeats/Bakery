using FluentValidation;

namespace Bakery.Services.Application.Models.Customer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomer>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name).NotNull().MaximumLength(255);
        }
    }
}