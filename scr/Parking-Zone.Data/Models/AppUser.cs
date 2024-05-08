using Microsoft.AspNetCore.Identity;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.Models
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
