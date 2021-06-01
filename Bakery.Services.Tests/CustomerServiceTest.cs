using System;
using System.Linq;
using AutoMapper;
using Bakery.Core;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Domain.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.X509;
using Xunit;

namespace Bakery.Services.Tests
{
    public class TestTest : TestHere
    {
        public TestTest() : base(
            new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Filename=Test.db")
                .Options)
        {
            
        }
        
        [Fact]
        public void TestCreate()
        {
            using (var ctx = new AppDbContext(ContextOptions))
            {
                var mapper = ServiceProvider.GetService<IMapper>();
                var customerService = new CustomerService(ctx, mapper);

                var customer = new CreateCustomer() {Name = "Hello", Desc = "wtf", AddressName = "ftw"};
                var result = customerService.CreateCustomer(customer).Result;

                Assert.True(result.IsSuccessful);
                Assert.NotNull(ctx.Customer.FirstOrDefault(c => c.CustomerId == 1));
            }
        }

        [Fact]
        public void TestUpdate()
        {
            using (var ctx = new AppDbContext(ContextOptions))
            {
                var mapper = ServiceProvider.GetService<IMapper>();
                var customerService = new CustomerService(ctx, mapper);
                
                var customer = new CreateCustomer() {Name = "Hello", Desc = "wtf", AddressName = "ftw"};
                customerService.CreateCustomer(customer);

                var updateCustomer = new UpdateCustomer() {CustomerId = 1, CustomerName = "Bye", CustomerDesc = "hello"};
                var result = customerService.UpdateCustomer(updateCustomer).Result;
            }
        }
    }
}