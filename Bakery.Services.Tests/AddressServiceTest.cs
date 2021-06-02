using System.Linq;
using AutoMapper;
using Bakery.Core;
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

            var addressRepo = new AddressRepository(string.Empty);
            var mapper = ServiceProvider.GetService<IMapper>();
            _addressService = new AddressService(_context, addressRepo, mapper);
        }

        [Fact]
        public void TestCreate()
        {
            var customer = _context.Customer.FirstOrDefault(m => m.CustomerId == 1);
            var testAddress = new CreateCustomerAddress() {CustomerId = 1, Latitude = 12, Longitude = 12, AddressName = "Home"};

            var result = _addressService.Create(customer, testAddress).Result;
            var customerAddress = _context.CustomerAddress.FirstOrDefault(c => c.AddressId == 1);
            
            Assert.True(result.IsSuccessful);
            Assert.NotNull(customerAddress);
            Assert.Equal("Home", customerAddress.AddressName);
        }
    }
}