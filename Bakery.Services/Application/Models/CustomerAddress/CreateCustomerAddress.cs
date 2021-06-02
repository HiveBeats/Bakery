namespace Bakery.Services.Application.Models.CustomerAddress
{
    public class CreateCustomerAddress
    {
        public string CustomerId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string AddressName { get; set; }
    }
}