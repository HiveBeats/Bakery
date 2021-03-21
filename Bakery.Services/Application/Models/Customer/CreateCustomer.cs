namespace Bakery.Services.Application.Models.Customer
{
    public class CreateCustomer
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string AddressName { get; set; }
    }
}