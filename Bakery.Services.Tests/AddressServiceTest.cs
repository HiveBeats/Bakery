using System.Linq;
using AutoMapper;
using Bakery.Core;
using Bakery.Services.Application;
using Bakery.Services.Application.Models.CustomerAddress;
using Bakery.Services.Domain.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;


namespace Bakery.Services.Tests
{
    public class AddressServiceTest : BaseTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private AppDbContext _context;
        private IAddressService _addressService;
        
        public AddressServiceTest(ITestOutputHelper testOutputHelper) : base(
            new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Filename=Test.db")
                .Options)
        {
            _testOutputHelper = testOutputHelper;
            _context = new AppDbContext(ContextOptions);

            var addressRepo = new AddressRepository("Filename=Test.db", new TestDbConnectionResolver());
            var mapper = ServiceProvider.GetService<IMapper>();
            _addressService = new AddressService(_context, addressRepo, mapper);
        }

        [Fact]
        public void TestCreate()
        {
            var customer = _context.Customer.FirstOrDefault();
            var testAddress = new CreateCustomerAddress() {Latitude = 12, Longitude = 12, AddressName = "Home"};

            var result = _addressService.Create(customer, testAddress).Result;

            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.Value);
            var customerAddress = _context.CustomerAddress.FirstOrDefault(c => c.AddressId == result.Value.AddressId);
            Assert.NotNull(customerAddress);
            Assert.Equal("Home", customerAddress.AddressName);
        }
    }
}