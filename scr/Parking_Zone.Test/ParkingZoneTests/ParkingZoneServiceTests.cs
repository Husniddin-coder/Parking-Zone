using Moq;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.Service.Services;

namespace Parking_Zone.Test.ParkingZoneTests;

public class ParkingZoneServiceTests
{
    private readonly Mock<IParkingZoneRepository> _parkingRepository;
    private readonly IParkingZoneService _parkingZoneService;

    public ParkingZoneServiceTests()
    {
        _parkingRepository = new Mock<IParkingZoneRepository>();
        _parkingZoneService = new ParkingZoneService(_parkingRepository.Object);
    }

    [Fact]
    public void ParkingZoneService_Insert_ReturnsParkingZone()
    {
        //Arrange 

        var newParkingZone = new ParkingZone()
        {
            Name = "New Parking Zone",
            Address = new Address()
            {
                City = "new City",
                Street = "new Street",
                Province = "new Province",
                PostalCode = "new PostalCode",
                Country = "new Country"
            }
        };

        var expectedParkingZone = new ParkingZone()
        {
            Name = "New Parking Zone",
            Address = new Address()
            {
                City = "new City",
                Street = "new Street",
                Province = "new Province",
                PostalCode = "new PostalCode",
                Country = "new Country"
            }
        };

        _parkingRepository.Setup(x => x.Create(It.IsAny<ParkingZone>()))
            .Returns(newParkingZone);

        //Act

        _parkingZoneService.Insert(newParkingZone);

        //Assert

        _parkingRepository.Verify(x => x.Create(newParkingZone), Times.Once());
    }

    //[Fact]
    //public void ParkingZone_Modify_ReturnsModifiedParkingZone()
    //{
    //    //Arrange

    //    var modifiedParkingZone = new ParkingZone()
    //    {

    //    }
    //}
}
