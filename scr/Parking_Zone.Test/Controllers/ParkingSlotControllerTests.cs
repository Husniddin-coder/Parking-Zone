using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.Areas.Admin.Controllers;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using Parking_Zone.Service.Interfaces;
using System.Text.Json;

namespace Parking_Zone.Test.Controllers;

public class ParkingSlotControllerTests
{
    private readonly Mock<IParkingSlotService> _slotService;
    private readonly Mock<IParkingZoneService> _zoneService;
    private readonly ParkingSlotsController _slotsController;
    private readonly ParkingSlot _slotTest;
    private readonly ParkingZone _zoneTest;
    private readonly long Id = 1;

    public ParkingSlotControllerTests()
    {
        _slotService = new Mock<IParkingSlotService>();
        _zoneService = new Mock<IParkingZoneService>();
        _slotsController = new ParkingSlotsController(_slotService.Object, _zoneService.Object);
        _zoneTest = new()
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
        _slotTest = new()
        {
            Id = Id,
            Number = 5,
            IsAvailable = true,
            Category = SlotCategory.Premium,
            FeePerHour = 10,
            ParkingZoneId = Id,
            ParkingZone = _zoneTest
        };

    }

    #region Index
    [Fact]
    public void GivenZoneId_WhenIndexIsCalled_ThenReturnsViewWithVM()
    {
        //Arrange
        List<ParkingSlot> expectedSlots = new List<ParkingSlot>() { _slotTest };
        IndexVM expectedIndexVM = new()
        {
            SlotsVMs = new List<ListOfSlotVMs>() { new ListOfSlotVMs(_slotTest) },
            ZoneName = _zoneTest.Name,
            ZoneId = _zoneTest.Id
        };

        _slotService
            .Setup(x => x.RetrieveByZoneId(Id))
            .Returns(expectedSlots);

        _zoneService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_zoneTest);

        //Act 
        var result = _slotsController.Index(Id);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<IndexVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedIndexVM), JsonSerializer.Serialize(model));

        _slotService.Verify(x => x.RetrieveByZoneId(Id), Times.Once());
        _zoneService.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region Details
    [Fact]
    public void GivenSlotId_WhenDetailsIsCalled_ThenReturnsViewWithVM()
    {
        //Arrange
        SlotDetailsVM expectedDetailsVM = new(_slotTest);

        _slotService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_slotTest);

        //Act
        var result = _slotsController.Details(Id);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<SlotDetailsVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedDetailsVM), JsonSerializer.Serialize(model));

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region Create
    [Fact]
    public void GivenZoneId_WhenGetCreateIsCalled_ThenReturnsVeiwResultWithVM()
    {
        //Arrange
        SlotCreateVM expectedSlotCreateVM = new()
        {
            ParkingZoneId = Id,
            ParkingZoneName = _zoneTest.Name
        };

        _zoneService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_zoneTest);

        //Act
        var result = _slotsController.Create(Id);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<SlotCreateVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedSlotCreateVM), JsonSerializer.Serialize(model));

        _zoneService.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenSlotCreateVMWithNegativeNumber_WhenPostCreateIsCalled_ThenModelStateIsFalseAndReturnsViewResultWithVM()
    {
        //Arrange
        SlotCreateVM slotCreateVM = new()
        {
            Number = -1, // negative number
            IsAvailable = true,
            FeePerHour = 10,
            Category = SlotCategory.Standart,
            ParkingZoneName = _zoneTest.Name,
            ParkingZoneId = _zoneTest.Id
        };

        //Act
        var result = _slotsController.Create(slotCreateVM);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<SlotCreateVM>(model);
        Assert.False(_slotsController.ModelState.IsValid);
        Assert.Equal(JsonSerializer.Serialize(slotCreateVM), JsonSerializer.Serialize(model));
    }


    [Fact]
    public void GivenSlotCreateVMWithExistingNumber_WhenPostCreateIsCalled_ThenModelStateIsFalseAndReturnsViewResultWithVM()
    {
        //Arrange
        int slotNumber = 1;
        SlotCreateVM slotCreateVM = new()
        {
            Number = 1,
            IsAvailable = true,
            FeePerHour = 10,
            Category = SlotCategory.Standart,
            ParkingZoneName = _zoneTest.Name,
            ParkingZoneId = _zoneTest.Id
        };

        _slotService
            .Setup(x => x.SlotIsFoundWithThisNumber(slotNumber, Id))
            .Returns(true);

        //Act
        var result = _slotsController.Create(slotCreateVM);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<SlotCreateVM>(model);
        Assert.False(_slotsController.ModelState.IsValid);
        Assert.Equal(JsonSerializer.Serialize(slotCreateVM), JsonSerializer.Serialize(model));
    }

    [Fact]
    public void GivenValidSlotCreateVM_WhenPostCreateIsCalled_ThenModelStateIsTrueAndReturnsRedirecToActionResultWithActionControllerNamesAndRouteValue()
    {
        //Arrange
        int slotNumber = 2;
        SlotCreateVM slotCreateVM = new()
        {
            Number = 1,
            IsAvailable = true,
            FeePerHour = 10,
            Category = SlotCategory.Standart,
            ParkingZoneName = _zoneTest.Name,
            ParkingZoneId = _zoneTest.Id
        };

        _slotService
            .Setup(x => x.SlotIsFoundWithThisNumber(slotNumber, Id))
            .Returns(false);

        _zoneService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_zoneTest);

        _slotService
            .Setup(x => x.Insert(_slotTest));
        //Act
        var result = _slotsController.Create(slotCreateVM);

        //Assert
        Assert.NotNull(result);
        var returnResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent("Index", returnResult.ActionName);
        Assert.Equivalent("ParkingSlots", returnResult.ControllerName);
        Assert.Equivalent(new Dictionary<string, long>() { { "id", 1 } }, returnResult.RouteValues);
        Assert.True(_slotsController.ModelState.IsValid);
    }

    #endregion

    #region Edit
    [Fact]
    public void GivenSlotId_WhenGetEditIsCalled_ThenReturnsViewResultWithVM()
    {
        //Arrange
        SlotEditVM expectedEditVM = new(_slotTest);

        _slotService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_slotTest);

        //Act
        var result = _slotsController.Edit(Id);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<SlotEditVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedEditVM), JsonSerializer.Serialize(model));
        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenSlotIdAndSlotEdidVMWithNegativeNumber_WhenPostEditIsCalled_ThenModelStateIsFalseAndReturnsViewResultWithVM()
    {
        //Arrange
        SlotEditVM expectedEditVM = new(_slotTest);
        _slotTest.Number = -1;
        expectedEditVM.Number = -1;

        _slotService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_slotTest);

        //Act
        var result = _slotsController.Edit(Id, expectedEditVM);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<SlotEditVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedEditVM), JsonSerializer.Serialize(model));
        Assert.False(_slotsController.ModelState.IsValid);

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenInvalidSlotIdAndSlotEdidVM_WhenPostEditIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        SlotEditVM expectedEditVM = new(_slotTest);

        _slotService
            .Setup(x => x.RetrieveById(Id));

        //Act
        var result = _slotsController.Edit(Id, expectedEditVM);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundObjectResult>(result);

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenSlotIdAndSlotEdidVMExistingNumber_WhenPostEditIsCalled_ThenModelStateIsFalseAndReturnsViewResultWithVM()
    {
        //Arrange
        SlotEditVM expectedEditVM = new(_slotTest);
        expectedEditVM.Number = 2;

        _slotService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_slotTest);

        _slotService
            .Setup(x => x.SlotIsFoundWithThisNumber(expectedEditVM.Number, Id))
            .Returns(true);

        //Act
        var result = _slotsController.Edit(Id, expectedEditVM);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<SlotEditVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedEditVM), JsonSerializer.Serialize(model));
        Assert.False(_slotsController.ModelState.IsValid);

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
        _slotService.Verify(x => x.SlotIsFoundWithThisNumber(expectedEditVM.Number, Id), Times.Once());
    }

    [Fact]
    public void GivenSlotIdAndSlotEdidVM_WhenPostEditIsCalled_ThenModelStateIsTrueAndReturnsRedirectToActionResult()
    {
        //Arrange
        SlotEditVM expectedEditVM = new(_slotTest);

        _slotService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_slotTest);

        _slotService
            .Setup(x => x.Update(_slotTest));

        //Act
        var result = _slotsController.Edit(Id, expectedEditVM);

        //Assert
        Assert.NotNull(result);
        Assert.True(_slotsController.ModelState.IsValid);
        var actionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent("Index", actionResult.ActionName);
        Assert.Equivalent("ParkingSlots", actionResult.ControllerName);
        Assert.Equivalent(new Dictionary<string, long>() { { "id", 1 } }, actionResult.RouteValues);

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
        _slotService.Verify(x => x.Update(_slotTest), Times.Once());
    }
    #endregion

    #region Delete
    [Fact]
    public void GivenInvalidSlotId_WhenGetDeleteIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _slotService
            .Setup(x => x.RetrieveById(Id));

        //Act
        var result = _slotsController.Delete(Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundObjectResult>(result);

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenValidSlotId_WhenGetDeleteIsCalled_ThenReturnsViewResultWithSlot()
    {
        //Arrange 
        _slotService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_slotTest);

        //Act
        var result = _slotsController.Delete(Id);

        //Assert
        Assert.NotNull(result);
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<ParkingSlot>(model);
        Assert.Equal(JsonSerializer.Serialize(_slotTest), JsonSerializer.Serialize(model));

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenInvalidSlotId_WhenDeleteConfirmedIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _slotService
            .Setup(x => x.RetrieveById(Id));

        //Act 
        var result = _slotsController.DeleteConfirmed(Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundObjectResult>(result);

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenValidSlotId_WhenDeleteConfirmedIsCalled_ThenReturnsRedirectToActionResult()
    {
        //Arrange
        _slotService
            .Setup(x => x.RetrieveById(Id))
            .Returns(_slotTest);

        _slotService
            .Setup(x => x.Remove(Id));

        //Act 
        var result = _slotsController.DeleteConfirmed(Id);

        //Assert
        Assert.NotNull(result);
        var actionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent("Index", actionResult.ActionName);
        Assert.Equivalent("ParkingSlots", actionResult.ControllerName);
        Assert.Equivalent(new Dictionary<string, long>() { { "id", 1 } }, actionResult.RouteValues);

        _slotService.Verify(x => x.RetrieveById(Id), Times.Once());
        _slotService.Verify(x => x.Remove(Id), Times.Once());
    }
    #endregion
}
