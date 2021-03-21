using System;

namespace Bakery.Services.Application.Models.CustomerAddress
{
    public class Location
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class NearestLocation : Location
    {
        public float Distance { get; set; }
        public DateTime Today { get; set; }
    }
}