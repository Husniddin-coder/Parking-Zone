using Parking_Zone.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ParkingZoneVMs;

public class CreateVM
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Province { get; set; }

    [Required]
    public string Country { get; set; }

    [Required]
    public string PostalCode { get; set; }

    public ParkingZone MapToModel()
    {
        return new ParkingZone
        {
            Name = Name,
            Address = new Address
            {
                Street = Street,
                City = City,
                Province = Province,
                Country = Country,
                PostalCode = PostalCode,
            }
        };
    }
}
