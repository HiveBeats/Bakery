using FluentValidation;

namespace Bakery.Services.Application.Models.Customer
{
    public class GetNearestCustomersValidator : AbstractValidator<GetNearestCustomers>
    {
        public GetNearestCustomersValidator()
        {
            RuleFor(x => x.Latitude).NotNull()
                .GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);

            RuleFor(x => x.Longitude).NotNull()
                .GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);

            RuleFor(x => x.Distance).NotNull();
        }
    }
}