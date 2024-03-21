using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_Zone.MVC.Models
{
    public class ParkingZone : Auditable
    {
        public string Name { get; set; }

        [ForeignKey("Address")]
        public long AddressId { get; set; }

        public Address Address { get; set; }
    }
}
