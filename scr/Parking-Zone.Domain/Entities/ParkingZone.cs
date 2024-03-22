using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_Zone.Domain.Entities
{
    public class ParkingZone : Auditable
    {
        public string Name { get; set; }

        [ForeignKey("Address")]
        public long AddressId { get; set; }

        public Address Address { get; set; }
    }
}
