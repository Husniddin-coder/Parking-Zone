using Parking_Zone.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.ViewModels.ParkingZoneVMs;

public class DetailsVM
{
    [Required]
    public long Id { get; set; }

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

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DetailsVM()
    { }

    public DetailsVM (ParkingZone parkingZone)
    {
        Id = parkingZone.Id;
        Name = parkingZone.Name;
        Street = parkingZone.Address.Street;
        City = parkingZone.Address.City;
        Country = parkingZone.Address.Country;
        Province = parkingZone.Address.Province;
        PostalCode = parkingZone.Address.PostalCode;
        CreatedAt = parkingZone.CreatedAt;
        UpdatedAt = parkingZone.UpdateAt;
    }
}
