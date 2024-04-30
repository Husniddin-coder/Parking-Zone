using Microsoft.AspNetCore.Identity;

namespace Parking_Zone.Data.Models
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
    }
}
