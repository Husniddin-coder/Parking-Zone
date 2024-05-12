using Moq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Parking_Zone.Domain.Enums;
using Parking_Zone.Domain.Entities;
using Parking_Zone.MVC.Controllers;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using Parking_Zone.MVC.ViewModels.ReservationVMs;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Test.Controllers;

public class ReservationControllerTests
{
    private readonly Mock<IParkingZoneService> _zoneService;
    private readonly Mock<IParkingSlotService> _slotService;
    private readonly IList<ParkingZone> _expectedZones;
    private readonly ReservationController _controller;
    private readonly ParkingZone _zoneTest;
    private readonly ParkingSlot _slotTest;
    private readonly long Id = 1;

    public ReservationControllerTests()
    {
        _zoneService = new Mock<IParkingZoneService>();
        _slotService = new Mock<IParkingSlotService>();
        _controller = new ReservationController(_zoneService.Object, _slotService.Object);
        _zoneTest = new()
        {
            Id = Id,
            Name = "Parking Zone",
            ParkingSlots = [_slotTest],
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
        _slotTest = new()
        {
            Id = Id,
            Number = 5,
            IsAvailable = true,
            Category = SlotCategory.Premium,
            FeePerHour = 10,
            ParkingZoneId = Id,
            ParkingZone = _zoneTest,
            Reservations = [new()]
        };
        _expectedZones = [_zoneTest];
    }

    #region FreeSlots
    [Fact]
    public void GivenNothing_WhenGetFreeSlotsIsCalled_ThenReturnsViewResultWithVM()
    {
        //Arrange
        FreeSlotsVM expectedFreeSlotsVM = new(_expectedZones);
        _zoneService
            .Setup(x => x.RetrieveAll())
            .Returns(_expectedZones);

        //Act
        var result = _controller.FreeSlots();

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<FreeSlotsVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedFreeSlotsVM), JsonSerializer.Serialize(model));
        _zoneService.Verify(x => x.RetrieveAll(), Times.Once());
    }

    [Fact]
    public void GivenNothing_WhenFreeSlotsIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        IEnumerable<ParkingZone> Null = null;
        _zoneService
            .Setup(x => x.RetrieveAll())
            .Returns(Null);

        //Act
        var result = _controller.FreeSlots();

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
        _zoneService.Verify(x => x.RetrieveAll(), Times.Once());
    }

    [Fact]
    public void GivenFreeSlotsVM_WhenPostFreeSlotsIsCalled_ThenReturnsViewResultWithVM()
    {
        //Arrange
        var expectedSlots = new List<ParkingSlot>() { _slotTest };
        DateTime startTime = DateTime.UtcNow;
        FreeSlotsVM expectedFreeSlotsVM = new(_expectedZones)
        {
            ZoneId = Id,
            Duration = 1,
            StartTime = startTime,
            ZoneName = _zoneTest.Name,
            FreeSlots = [new ListOfSlotVMs(_slotTest)]
        };

        _zoneService
            .Setup(x => x.RetrieveAll())
            .Returns(_expectedZones);

        _zoneService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_zoneTest);

        _slotService
            .Setup(x => x.GetFreeSlotsByZoneIdAndPeriod(Id, startTime, 1))
            .Returns(expectedSlots);

        //Act
        var result = _controller.FreeSlots(expectedFreeSlotsVM);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<FreeSlotsVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedFreeSlotsVM), JsonSerializer.Serialize(model));

        _zoneService.Verify(x => x.RetrieveAll(), Times.Once);
        _zoneService.Verify(x => x.RetrieveById(Id), Times.Once);
        _slotService.Verify(x => x.GetFreeSlotsByZoneIdAndPeriod(Id, startTime, 1), Times.Once);
    }

    [Fact]
    public void GivenInvalidFreeSlotsVM_WhenFreeSlotsIsCalled_ThenModelStateIsFalseAndReturnsViewResultWithVM()
    {
        //Arrange
        FreeSlotsVM expectedFreeSlotsVM = new(_expectedZones);
        _controller.ModelState.AddModelError("something", "Invalid VM");

        _zoneService
            .Setup(x => x.RetrieveAll())
            .Returns(_expectedZones);

        //Act
        var result = _controller.FreeSlots(expectedFreeSlotsVM);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<ViewResult>(result);
        Assert.False(_controller.ModelState.IsValid);
    }
    #endregion
}
