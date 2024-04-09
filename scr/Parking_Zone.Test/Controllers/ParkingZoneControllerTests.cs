using Moq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.MVC.Areas.Admin.Controllers;
using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;

namespace Parking_Zone.Test.Controllers;

public class ParkingZoneControllerTests
{
    private readonly Mock<IParkingZoneService> _parkingServiceMock;
    private readonly Mock<IAddressService> _addressServiceMock;
    private readonly ParkingZonesController _parkingController;
    private readonly ParkingZone _parkingZoneTest;
    private readonly long Id = 1;

    public ParkingZoneControllerTests()
    {
        _addressServiceMock = new Mock<IAddressService>();
        _parkingServiceMock = new Mock<IParkingZoneService>();
        _parkingController = new ParkingZonesController(_parkingServiceMock.Object, _addressServiceMock.Object);
        _parkingZoneTest = new()
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

    #region Index
    [Fact]
    public void GivenNothing_WhenIndexIsCalled_ThenReturnsViewResultWithListOfVMs()
    {
        //Arrange
        var expectedParkingZones = new List<ParkingZone>() { _parkingZoneTest };
        var expectedLIstOfVMs = new List<ListOfItemsVM>() { new(_parkingZoneTest) };

        _parkingServiceMock
            .Setup(x => x.RetrieveAll())
            .Returns(expectedParkingZones);

        //Act
        var viewResult = _parkingController.Index();

        //Assert
        var model = Assert.IsType<ViewResult>(viewResult).Model;
        Assert.NotNull(model);
        Assert.IsAssignableFrom<IEnumerable<ListOfItemsVM>>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedLIstOfVMs), JsonSerializer.Serialize(model));
        _parkingServiceMock.Verify(x => x.RetrieveAll(), Times.Once);
    }
    #endregion

    #region Details
    [Fact]
    public void GivenId_WhenDetailsIsCalled_ThenReturnsViewResultWithVM()
    {
        //Arrange
        _parkingServiceMock
            .Setup(x => x.RetrieveById(Id))
            .Returns(_parkingZoneTest);

        DetailsVM expectedDetailsVM = new DetailsVM(_parkingZoneTest);

        //Act
        var viewResult = _parkingController.Details(Id);

        //Assert
        var model = Assert.IsType<ViewResult>(viewResult).Model;
        Assert.NotNull(model);
        Assert.IsAssignableFrom<DetailsVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedDetailsVM), JsonSerializer.Serialize(model));
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once);
    }

    [Fact]
    public void GivenId_WhenDetailsIsCalled_ThenReturnsNotFound()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id));

        //Act
        var result = _parkingController.Details(Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    #endregion

    #region Create

    [Fact]
    public void GivenNothing_WhenCreateIsCalled_ThenReturnsViewResult()
    {
        //Arrange

        //Act
        var viewResult = _parkingController.Create();

        //Assert
        Assert.IsType<ViewResult>(viewResult);
    }

    [Fact]
    public void GivenCreateVM_WhenPostCreateIsCalled_ThenModelStateIsFalseAndReturnsViewResultVM()
    {
        //Arrange
        CreateVM createVM = new();
        _parkingController.ModelState.AddModelError("Name", "Required");

        //Act
        var viewResult = _parkingController.Create(createVM);

        //Assert
        Assert.NotNull(viewResult);
        Assert.IsType<ViewResult>(viewResult);
    }

    [Fact]
    public void GivenCreateVM_WhenPostCreateIsCalled_ThenModelStateIsTrueAndReturnsRedirectToActionResult()
    {
        //Arrange
        CreateVM createVM = new();
        _parkingServiceMock.Setup(x => x.Insert(It.IsAny<ParkingZone>()));

        //Act
        var viewResult = _parkingController.Create(createVM);

        //Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(viewResult);
        Assert.Equivalent(redirectResult.ActionName, "Index");
        _parkingServiceMock.Verify(x => x.Insert(It.IsAny<ParkingZone>()), Times.Once());
    }

    #endregion

    #region Edit

    [Fact]
    public void GivenId_WhenEditIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id));

        //Act
        var result = _parkingController.Edit(Id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenId_WhenEditIsCalled_ThenReturnsViewResultWithVM()
    {
        //Arrange
        EditVM expectedEditVM = new(_parkingZoneTest);

        _parkingServiceMock
            .Setup(x => x.RetrieveById(Id))
            .Returns(_parkingZoneTest);

        //Act
        var viewResult = _parkingController.Edit(Id);

        //Assert
        var model = Assert.IsType<ViewResult>(viewResult).Model;
        Assert.IsAssignableFrom<EditVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedEditVM), JsonSerializer.Serialize(model));
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenIdAndEditVM_WhenPostEditIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        EditVM editVM = new();
        ParkingZone existingParkingZone = new() { Id = 2 };

        _parkingServiceMock
            .Setup(x => x.RetrieveById(Id))
            .Returns(existingParkingZone);

        //Act
        var result = _parkingController.Edit(Id, editVM);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Theory]
    [InlineData("Name", "Required")]
    [InlineData("Address", "Required")]
    public void GivenIdAndEditVM_WhenPostEditIsCalled_ThenModelStateIsFalseAndReturnsViewResult(string key, string errorMessage)
    {
        //Arrange
        EditVM editVM = new(_parkingZoneTest);
        EditVM expectedEditVM = new(_parkingZoneTest);

        _parkingController.ModelState.AddModelError(key, errorMessage);
        _parkingServiceMock
            .Setup(x => x.RetrieveById(Id))
            .Returns(_parkingZoneTest);

        //Act
        var result = _parkingController.Edit(Id, editVM);

        //Assert
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<EditVM>(model);
        Assert.Equal(JsonSerializer.Serialize(expectedEditVM), JsonSerializer.Serialize(model));
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenIdAndEditVM_WhenEditIsCalled_ThenModelStateIsTrueAndReturnsRedirectToActionResult()
    {
        //Arrange
        EditVM editVM = new();
        _parkingServiceMock.Setup(x => x.Update(_parkingZoneTest));
        _parkingServiceMock
            .Setup(x => x.RetrieveById(Id))
            .Returns(_parkingZoneTest);

        //Act
        var result = _parkingController.Edit(Id, editVM);

        //Assert
        var resModel = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent(resModel.ActionName, "Index");
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
        _parkingServiceMock.Verify(x => x.Update(_parkingZoneTest), Times.Once());
    }

    #endregion

    #region Delete
    [Fact]
    public void GivenId_WhenDeleteIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id));

        //Act
        var result = _parkingController.Delete(Id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenId_WhenDeleteIsCalled_ThenReturnsViewResult()
    {
        //Arrange
        _parkingServiceMock
            .Setup(x => x.RetrieveById(Id))
            .Returns(_parkingZoneTest);

        //Act
        var result = _parkingController.Delete(Id);

        //Assert
        var model = Assert.IsType<ViewResult>(result).Model;
        Assert.IsAssignableFrom<ParkingZone>(model);
        Assert.Equal(JsonSerializer.Serialize(_parkingZoneTest), JsonSerializer.Serialize(model));
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenId_WhenDeleteConfirmedIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id));

        //Act
        var result = _parkingController.DeleteConfirmed(Id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }

    [Fact]
    public void GivenId_WhenDeleteConfirmedIsCalled_ThenReturnsRedirectToActionResult()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.Remove(Id));
        _addressServiceMock.Setup(x => x.Remove(_parkingZoneTest.AddressId));
        _parkingServiceMock
            .Setup(x => x.RetrieveById(Id))
            .Returns(_parkingZoneTest);

        //Act
        var result = _parkingController.DeleteConfirmed(Id);

        //Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent(viewResult.ActionName, "Index");
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
        _parkingServiceMock.Verify(x => x.Remove(Id), Times.Once());
        _addressServiceMock.Verify(x => x.Remove(_parkingZoneTest.AddressId), Times.Once());
    }

    #endregion
}