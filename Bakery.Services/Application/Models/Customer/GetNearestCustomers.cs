namespace Bakery.Services.Application.Models.Customer
{
    public class GetNearestCustomers
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Distance { get; set; }
    }
}