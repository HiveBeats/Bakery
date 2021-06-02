using System;
using System.Linq;
using AutoMapper;
using Bakery.Core;
using Bakery.Services.Application.Models.Customer;
using Bakery.Services.Domain.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.X509;
using Xunit;
using Xunit.Abstractions;

namespace Bakery.Services.Tests
{
    public class CustomerServiceTest : BaseTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private AppDbContext _context;
        private ICustomerService _customerService;

        public CustomerServiceTest(ITestOutputHelper testOutputHelper) : base(
            new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Filename=Test.db")
                .Options)
        {
            _testOutputHelper = testOutputHelper;
            _context = new AppDbContext(ContextOptions);

            var mapper = ServiceProvider.GetService<IMapper>();
            _customerService = new CustomerService(_context, mapper);
        }

        [Fact]
        public void TestCreate()
        {
                var customer = new CreateCustomer() {Name = "Hello", Desc = "wtf", AddressName = "ftw"};
                var result = _customerService.CreateCustomer(customer).Result;
                
                Assert.True(result.IsSuccessful);
                Assert.NotNull(_context.Customer.FirstOrDefault(c => c.CustomerId == 2));
        }

        [Fact]
        public void TestUpdate()
        {
                var updateCustomer = new UpdateCustomer() {CustomerId = 1, CustomerName = "Bye", CustomerDesc = "hello"};
                var result = _customerService.UpdateCustomer(updateCustomer).Result;
        
                var updatedCustomer = _context.Customer.FirstOrDefault(c => c.CustomerId == 1);
                
                _testOutputHelper.WriteLine(result.Exception);
                
                Assert.True(result.IsSuccessful);
                Assert.NotNull(updatedCustomer);
                Assert.Equal("Bye", updatedCustomer.CustomerName);
                Assert.Equal("hello", updatedCustomer.CustomerDescription);
        }

        [Fact]
        public void TestGet()
        {
            var customerDetail = new GetCustomerDetail() {CustomerId = 1};
            var result = _customerService.GetCustomer(customerDetail).Result;
            
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.CustomerId);
        }
        
        [Fact]
        public void TestClose()
        {
            var closeCustomer = new CloseCustomer() {CustomerId = 1};
            var result = _customerService.CloseCustomer(closeCustomer).Result;
            
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.Value.DateEnd);
        }
    }
}