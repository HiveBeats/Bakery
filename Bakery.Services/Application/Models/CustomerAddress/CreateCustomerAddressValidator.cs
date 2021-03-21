using FluentValidation;

namespace Bakery.Services.Application.Models.CustomerAddress
{
    public class CreateCustomerAddressValidator : AbstractValidator<CreateCustomerAddress>
    {
        public CreateCustomerAddressValidator()
        {
            RuleFor(x => x.CustomerId).NotNull();
            
            RuleFor(x => x.Latitude).NotNull()
                .GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);

            RuleFor(x => x.Longitude).NotNull()
                .GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);

            RuleFor(x => x.AddressName).NotNull().
                MaximumLength(255);
        }
    }
}