using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.ViewModels.ParkingZoneVMs
{
    public class ParkingZoneEditVM
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

    }
}
