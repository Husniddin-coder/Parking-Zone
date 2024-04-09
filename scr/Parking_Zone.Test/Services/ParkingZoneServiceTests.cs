using Moq;
using System.Text.Json;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Services;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Test.Services;

public class ParkingZoneServiceTests
{
    private readonly Mock<IParkingZoneRepository> _parkingRepository;
    private readonly IParkingZoneService _parkingZoneService;
    private readonly ParkingZone _parkinZoneTest;
    private readonly long Id = 1;

    public ParkingZoneServiceTests()
    {
        _parkingRepository = new Mock<IParkingZoneRepository>();
        _parkingZoneService = new ParkingZoneService(_parkingRepository.Object);
        _parkinZoneTest = new()
        {
            Id = Id,
            Name = "Parking Zone",
            Address = new()
            {
                Id = Id,
                Street = "Wall Street",
                City = "New York",
                Province = "New York",
                PostalCode = "10005",
                Country = "United States"
            }
        };
    }

    #region Insert
    [Fact]
    public void GivenParkingZoneModel_WhenInsertIsCalled_ThenRepositoryCreateIsCalled()
    {
        //Arrange 
        _parkingRepository.Setup(x => x.Create(_parkinZoneTest));

        //Act
        _parkingZoneService.Insert(_parkinZoneTest);

        //Assert
        _parkingRepository.Verify(x => x.Create(_parkinZoneTest), Times.Once());
    }
    #endregion

    #region Update
    [Fact]
    public void GivenParkingZoneModel_WhenUpdateIsCalled_ThenRepositoryUpdateIsCalled()
    {
        //Arrange
        _parkingRepository.Setup(x => x.Update(_parkinZoneTest));

        //Act
        _parkingZoneService.Update(_parkinZoneTest);

        //Assert
        _parkingRepository.Verify(x => x.Update(_parkinZoneTest), Times.Once());
    }
    #endregion

    #region Remove
    [Fact]
    public void GivenId_WhenRemoveIsCalled_ThenRepositoryDeleteIsCalled()
    {
        //Arrange
        _parkingRepository.Setup(x => x.Delete(Id));

        //Act 
        _parkingZoneService.Remove(Id);

        //Assert
        _parkingRepository.Verify(x => x.Delete(Id), Times.Once());
    }
    #endregion

    #region RetrieveAll
    [Fact]
    public void GivenNothing_WhenRetrieveAllIsCalled_ThenReturnsListOfParkingZones()
    {
        //Arrange
        IEnumerable<ParkingZone> expectedParkingZones = new List<ParkingZone>() { _parkinZoneTest };

        _parkingRepository
            .Setup(x => x.GetAll())
            .Returns(expectedParkingZones);

        //Act
        var result = _parkingZoneService.RetrieveAll();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<ParkingZone>>(result);
        Assert.Equal(JsonSerializer.Serialize(expectedParkingZones), JsonSerializer.Serialize(result));
        _parkingRepository.Verify(x => x.GetAll(), Times.Once());
    }
    #endregion

    #region RetrieveById
    [Fact]
    public void GivenId_WhenRetrieveByIdIsCalled_ThenReturnsParkingZoneModel()
    {
        //Arrange
        _parkingRepository
            .Setup(x => x.Get(Id))
            .Returns(_parkinZoneTest);

        //Act
        var result = _parkingZoneService.RetrieveById(Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<ParkingZone>(result);
        Assert.Equal(JsonSerializer.Serialize(_parkinZoneTest), JsonSerializer.Serialize(result));
        _parkingRepository.Verify(x => x.Get(Id), Times.Once());
    }
    #endregion
}
