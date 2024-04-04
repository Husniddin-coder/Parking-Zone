using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.ViewModels.ParkingZoneVMs;

public class ListOfItemsVM
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Street { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public IEnumerable<ListOfItemsVM> MapToVM(IEnumerable<ParkingZone> parkingZones)
    {
        return parkingZones.Select(parkingZone => new ListOfItemsVM()
        {
            Id = parkingZone.Id,
            Name = parkingZone.Name,
            Street = parkingZone.Address.Street,
            City = parkingZone.Address.City,
            Province = parkingZone.Address.Province,
            Country = parkingZone.Address.Country,
            PostalCode = parkingZone.Address.PostalCode,
            CreatedAt = parkingZone.CreatedAt,
            UpdatedAt = parkingZone.UpdateAt
        });
    }

}
