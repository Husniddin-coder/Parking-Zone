using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.ViewModels.ParkingZoneVMs;

public class EditVM
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Street { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }


    public EditVM()
    { }

    public EditVM(ParkingZone parkingZone)
    {
        Id = parkingZone.Id;
        Name = parkingZone.Name;
        Street = parkingZone.Address.Street;
        City = parkingZone.Address.City;
        Province = parkingZone.Address.Province;
        Country = parkingZone.Address.Country;
        PostalCode = parkingZone.Address.PostalCode;
    }

    public ParkingZone MapToModel(ParkingZone existingParkingZone)
    {
        existingParkingZone.Name = Name;
        existingParkingZone.Address.Street = Street;
        existingParkingZone.Address.City = City;
        existingParkingZone.Address.Province = Province;
        existingParkingZone.Address.Country = Country;
        existingParkingZone.Address.PostalCode = PostalCode;
        existingParkingZone.UpdateAt = DateTime.Now;
        existingParkingZone.Address.UpdateAt = DateTime.Now;

        return existingParkingZone;
    }
}
