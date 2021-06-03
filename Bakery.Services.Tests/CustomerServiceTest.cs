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
            var resultId = result.Value.CustomerId;
            
            Assert.True(result.IsSuccessful);
            Assert.NotNull(_context.Customer.FirstOrDefault(x => x.CustomerId == resultId));
        }

        [Fact]
        public void TestUpdate()
        {
            var id = _context.Customer.FirstOrDefault()?.CustomerId;
            var updateCustomer = new UpdateCustomer() {CustomerId = id, CustomerName = "Bye", CustomerDesc = "hello"};
            var result = _customerService.UpdateCustomer(updateCustomer).Result;
    
            var updatedCustomer = _context.Customer.FirstOrDefault(c => c.CustomerId == id);
            
            if (!result.IsSuccessful)
                _testOutputHelper.WriteLine(result.Exception);
            
            Assert.True(result.IsSuccessful);
            Assert.NotNull(updatedCustomer);
            Assert.Equal("Bye", updatedCustomer.CustomerName);
            Assert.Equal("hello", updatedCustomer.CustomerDescription);
        }

        [Fact]
        public void TestGet()
        {
            var id = _context.Customer.FirstOrDefault()?.CustomerId;
            var customerDetail = new GetCustomerDetail() {CustomerId = id};
            var result = _customerService.GetCustomer(customerDetail).Result;
            
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.Value);
            Assert.Equal(id, result.Value.CustomerId);
        }
        
        [Fact]
        public void TestClose()
        {
            var id = _context.Customer.FirstOrDefault()?.CustomerId;
            var closeCustomer = new CloseCustomer() {CustomerId = id};
            var result = _customerService.CloseCustomer(closeCustomer).Result;
            
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.Value.DateEnd);
        }
    }
}