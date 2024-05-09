using Moq;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.Service.Services;
using System.Text.Json;

namespace Parking_Zone.Test.Services;

public class ParkingSlotServiceTests
{
    private readonly Mock<IParkingSlotRepository> _slotRepositoryMock;
    private readonly IParkingSlotService _slotService;
    private readonly ParkingSlot _slotTest;
    private readonly long Id = 1;

    public ParkingSlotServiceTests()
    {
        _slotRepositoryMock = new Mock<IParkingSlotRepository>();
        _slotService = new ParkingSlotService(_slotRepositoryMock.Object);
        _slotTest = new()
        {
            Id = Id,
            Number = 5,
            IsAvailable = true,
            Category = SlotCategory.Premium,
            FeePerHour = 10,
            ParkingZoneId = Id,
            Reservations = new[]
            {
                new Reservation
                {
                    Id = Id,
                    VehicleNumber = "30A132AAuz",
                    StartTime = DateTime.Now,
                    Duration = 3,
                    ParkingSlotId = Id,
                    AppUserId = Id.ToString()
                }
            }
        };
    }

    #region Insert
    [Fact]
    public void GivenParkingSlotModel_WhenInsertIsCalled_ThenRepositoryCreateIsCalled()
    {
        //Arrange 
        _slotRepositoryMock.Setup(x => x.Create(_slotTest));

        //Act
        _slotService.Insert(_slotTest);

        //Assert
        _slotRepositoryMock.Verify(x => x.Create(_slotTest), Times.Once());
    }
    #endregion

    #region Update
    [Fact]
    public void GivenParkingSlotModel_WhenUpdateIsCalled_ThenRepositoryUpdateIsCalled()
    {
        //Arrange
        _slotRepositoryMock.Setup(x => x.Update(_slotTest));

        //Act
        _slotService.Update(_slotTest);

        //Act
        _slotRepositoryMock.Verify(x => x.Update(_slotTest), Times.Once());
    }
    #endregion

    #region Remove
    [Fact]
    public void GivenId_WhenRemoveIsCalled_ThenRepositoryDeleteIsCalled()
    {
        //Arrange
        _slotRepositoryMock.Setup(x => x.Delete(Id));

        //Act 
        _slotService.Remove(Id);

        //Assert
        _slotRepositoryMock.Verify(x => x.Delete(Id), Times.Once());
    }
    #endregion

    #region FreeSlot
    [Fact]
    public void GivenSlot_StartTime_Duration_WhenFreeSlotIsCalled_ThenReturnsFalse()
    {
        //Arrange
        DateTime startTime = DateTime.Now.AddHours(1);
        int duration = 2;

        //Act
        var result = _slotService.FreeSlot(_slotTest, startTime, duration);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenSlot_StartTime_Duration_WhenFreeSlotIsCalled_ThenReturnsTrue()
    {
        //Arrange
        DateTime startTime = DateTime.Now.AddHours(4);
        int duration = 6;

        //Act
        var result = _slotService.FreeSlot(_slotTest, startTime, duration);

        //Assert
        Assert.True(result);
    }
    #endregion

    #region RetrieveAll
    [Fact]
    public void GivenNothing_WhenRetrieveAllIsCalled_ThenReturnsListOfParkingSlots()
    {
        //Arrange
        IEnumerable<ParkingSlot> expectedParkingSlots = new List<ParkingSlot>() { _slotTest };

        _slotRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(expectedParkingSlots);

        //Act
        var result = _slotService.RetrieveAll();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<ParkingSlot>>(result);
        Assert.Equal(JsonSerializer.Serialize(expectedParkingSlots), JsonSerializer.Serialize(result));
        _slotRepositoryMock.Verify(x => x.GetAll(), Times.Once());
    }
    #endregion

    #region RetrieveById
    [Fact]
    public void GivenId_WhenRetrieveByIdIsCalled_ThenReturnsParkingSlotModel()
    {
        //Arrange
        _slotRepositoryMock
            .Setup(x => x.Get(Id))
            .Returns(_slotTest);

        //Act
        var result = _slotService.RetrieveById(Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<ParkingSlot>(result);
        Assert.Equal(JsonSerializer.Serialize(_slotTest), JsonSerializer.Serialize(result));
        _slotRepositoryMock.Verify(x => x.Get(Id), Times.Once());
    }
    #endregion

    #region RetrieveByZoneId
    [Fact]
    public void GivenZoneId_WhenRetrieveByZoneIdIsCalled_ThenReturnsParkingSlotsInOneParkingZone()
    {
        //Arrange
        IEnumerable<ParkingSlot> expectedParkingSlots = new List<ParkingSlot>() { _slotTest };
        _slotRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(expectedParkingSlots);

        //Act
        var result = _slotService.RetrieveByZoneId(Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ParkingSlot>>(result);
        Assert.Equal(JsonSerializer.Serialize(expectedParkingSlots), JsonSerializer.Serialize(result));
        _slotRepositoryMock.Verify(x => x.GetAll(), Times.Once());
    }
    #endregion

    #region SlotIsFoundWithThisNumber
    [Fact]
    public void GivenSlotNumberAndZoneId_WhenSlotIsFoundWithThisNumberIsCalled_ThenReturnsTrue()
    {
        //Arrange
        int slotNumber = 5;
        IEnumerable<ParkingSlot> expectedParkingSlots = new List<ParkingSlot>() { _slotTest };

        _slotRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(expectedParkingSlots);

        //Act
        var result = _slotService.SlotIsFoundWithThisNumber(slotNumber, Id);

        //Assert
        Assert.True(result);
        _slotRepositoryMock.Verify(x => x.GetAll(), Times.Once());
    }

    [Fact]
    public void GivenSlotNumberAndZoneId_WhenSlotIsFoundWithThisNumberIsCalled_ThenReturnsFalse()
    {
        //Arrange
        int slotNumber = 4;
        IEnumerable<ParkingSlot> expectedParkingSlots = new List<ParkingSlot>() { _slotTest };

        _slotRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(expectedParkingSlots);

        //Act
        var result = _slotService.SlotIsFoundWithThisNumber(slotNumber, Id);

        //Assert
        Assert.False(result);
        _slotRepositoryMock.Verify(x => x.GetAll(), Times.Once());
    }
    #endregion

    #region GetFreeSlotsByZoneIdAndPeriod
    [Fact]
    public void GivenZoneId_StartTime_Duration_WhenGetFreeSlotsByZoneIdAndPeriodIsCalled_ThenReturnsParkingSlots()
    {
        //Arrange
        IEnumerable<ParkingSlot> expectedSlots = [_slotTest];

        _slotRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(expectedSlots);

        //Act
        var result = _slotService.GetFreeSlotsByZoneIdAndPeriod(Id, DateTime.UtcNow.AddHours(1), 2);

        //Assert
        Assert.Equal(JsonSerializer.Serialize(expectedSlots), JsonSerializer.Serialize(result));

        _slotRepositoryMock.Verify(x => x.GetAll(), Times.Once);
    }
    #endregion
}
