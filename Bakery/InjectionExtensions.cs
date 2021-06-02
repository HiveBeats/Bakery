using Bakery.Services.Application;
using Bakery.Services.Domain.Address;
using Bakery.Services.Domain.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace Bakery
{
    public static class InjectionExtensions
    {
        public static void RegisterUserServices(this IServiceCollection services)
        {
            services.AddTransient<IDbConnectionResolver, ProductionDbConnectionResolver>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAddressService, AddressService>();
            //services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped<IAttachmentService, AttachmentService>();
            //services.AddScoped<ICategoryService, CategoryService>();

            //services.AddScoped<ITokenCacheWorker, TokenCacheWorker>();
            //services.AddHostedService<ConsumeTokenCacheWorker>();
        }
    }
}