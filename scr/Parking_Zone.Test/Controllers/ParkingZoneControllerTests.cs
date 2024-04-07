using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking_Zone.Domain.Entities;
using Parking_Zone.MVC.Areas.Admin.Controllers;
using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Test.Controllers;

public class ParkingZoneControllerTests
{
    private readonly Mock<IParkingZoneService> _parkingServiceMock;
    private readonly Mock<IAddressService> _addressServiceMock;
    private readonly ParkingZonesController _parkingController;
    private readonly ParkingZone _parkingZoneTest;
    private readonly CreateVM createVM = new();
    private readonly ParkingZone? nullPZ = null;
    private readonly long Id = 1;

    public ParkingZoneControllerTests()
    {
        _addressServiceMock = new Mock<IAddressService>();
        _parkingServiceMock = new Mock<IParkingZoneService>();
        _parkingController = new ParkingZonesController(_parkingServiceMock.Object, _addressServiceMock.Object);
        _parkingZoneTest = new() { Address = new() };
    }

    #region Index
    [Fact]
    public void GivenNothing_WhenIndexIsCalled_ThenReturnsViewResultWithListOfVMs()
    {
        //Arrange
        IEnumerable<ParkingZone> parkingZones = new List<ParkingZone>();
        IEnumerable<ListOfItemsVM>? listOfItemsVMs = Enumerable.Empty<ListOfItemsVM>();
        _parkingServiceMock.Setup(x => x.RetrieveAll()).Returns(parkingZones);

        //Act
        var viewResult = _parkingController.Index() as ViewResult;
        listOfItemsVMs = viewResult.Model as IEnumerable<ListOfItemsVM>;

        //Assert
        Assert.NotNull(viewResult);
        Assert.IsAssignableFrom<IEnumerable<ListOfItemsVM>>(listOfItemsVMs);
        _parkingServiceMock.Verify(x => x.RetrieveAll(), Times.Once);
    }
    #endregion

    #region Details
    [Fact]
    public void GivenIdOfPakingZone_WhenDetailsIsCalled_ThenReturnsViewResultWithVM()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(_parkingZoneTest);
        DetailsVM detailsVM = new DetailsVM();

        //Act
        var viewResult = _parkingController.Details(Id) as ViewResult;

        //Assert
        Assert.NotNull(viewResult);
        Assert.IsType<ViewResult>(viewResult);
        Assert.IsAssignableFrom<DetailsVM>(viewResult.Model);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once);
    }
    #endregion

    #region Details Notfound
    [Fact]
    public void GivenIdOfPakingZone_WhenDetailsIsCalled_ThenReturnsNotFound()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(nullPZ);

        //Act
        var result = _parkingController.Details(Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region Create Get
    [Fact]
    public void GivenNothing_WhenCreateIsCalled_ThenReturnsViewResult()
    {
        //Arrange

        //Act
        var viewResult = _parkingController.Create();

        //Assert
        Assert.IsType<ViewResult>(viewResult);
    }
    #endregion

    #region Create Post MS Invalid
    [Fact]
    public void GivenCreateVM_WhenCreateIsCalled_AndModelStateIsInvalid_ThenReturnsViewResultVM()
    {
        //Arrange
        _parkingController.ModelState.AddModelError("Name", "Required");

        //Act
        var viewResult = _parkingController.Create(createVM) as ViewResult;

        //Assert
        Assert.NotNull(viewResult);
        Assert.IsType<ViewResult>(viewResult);
    }
    #endregion

    #region Create Post MS Valid
    [Fact]
    public void GivenCreateVM_WhenCreateIsCalled_AndMSIsValid_ThenReturnsRedirectToActionResult()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.Insert(It.IsAny<ParkingZone>()));

        //Act
        var viewResult = _parkingController.Create(createVM);

        //Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(viewResult);
        Assert.Equivalent(redirectResult.ActionName, "Index");
        _parkingServiceMock.Verify(x => x.Insert(It.IsAny<ParkingZone>()), Times.Once());
    }
    #endregion

    #region Edit Get NotFound
    [Fact]
    public void GivenIdOfParkingZone_WhenEditIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(nullPZ);

        //Act
        var result = _parkingController.Edit(Id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region Edit Get
    [Fact]
    public void GivenIdOfParkingZone_WhenEditIsCalled_ThenReturnsViewResultWithVM()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(_parkingZoneTest);
        //Act
        var viewResult = _parkingController.Edit(Id);

        //Assert
        var resModel = Assert.IsType<ViewResult>(viewResult);
        Assert.IsAssignableFrom<EditVM>(resModel.Model);
    }
    #endregion

    #region Edit Post NotFound
    [Fact]
    public void GivenIdOfParkingZoneAndEditVM_WhenEditIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        ParkingZone existParkZone = new() { Id = 2 };
        EditVM editVM = new();
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(existParkZone);

        //Act
        var result = _parkingController.Edit(Id, editVM);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region Edit Post MS Invalid
    [Fact]
    public void GivenIdOfParkingZoneAndEditVM_WhenEditIsCalled_AndMSInvalid_ThenReturnsViewResult()
    {
        //Arrange
        _parkingZoneTest.Id = 1;
        EditVM editVM = new();
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(_parkingZoneTest);
        _parkingController.ModelState.AddModelError("Name", "Required");

        //Act
        var result = _parkingController.Edit(Id, editVM);

        //Assert
        var resModel = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<EditVM>(resModel.Model);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region Edit Post MS Valid
    [Fact]
    public void GivenIdOfParkingZoneAndEditVM_WhenEditIsCalled_AndMSIsValid_ThenReturnsRedirectToActionResult()
    {
        //Arrange
        _parkingZoneTest.Id = 1;
        EditVM editVM = new();
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(_parkingZoneTest);
        _parkingServiceMock.Setup(x => x.Modify(_parkingZoneTest));

        //Act
        var result = _parkingController.Edit(Id, editVM);

        //Assert
        var resModel = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent(resModel.ActionName, "Index");
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
        _parkingServiceMock.Verify(x => x.Modify(_parkingZoneTest), Times.Once());
    }
    #endregion

    #region Delete Get NotFound
    [Fact]
    public void GivenIdOfParkingZone_WhenDeleteIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(nullPZ);

        //Act
        var result = _parkingController.Delete(Id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region Delete Get
    [Fact]
    public void GivenIdOfParkingZone_WhenDeleteIsCalled_ThenReturnsViewResult()
    {
        //Arrange
        _parkingZoneTest.Id = 1;
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(_parkingZoneTest);

        //Act
        var result = _parkingController.Delete(Id);

        //Assert
        var resModel = Assert.IsType<ViewResult>(result);
        Assert.IsAssignableFrom<ParkingZone>(resModel.Model);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region DeleteConfirmed NotFound
    [Fact]
    public void GivenIdOfParkingZone_WhenDeleteConfirmedIsCalled_ThenReturnsNotFoundResult()
    {
        //Arrange
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(nullPZ);

        //Act
        var result = _parkingController.DeleteConfirmed(Id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
    }
    #endregion

    #region DeleteConfirmed
    [Fact]
    public void GivenIdOfParkingZone_WhenDeleteConfirmedIsCalled_ThenReturnsRedirectToActionResult()
    {
        //Arrange
        _parkingZoneTest.Id = 1;
        _parkingServiceMock.Setup(x => x.RetrieveById(Id)).Returns(_parkingZoneTest);
        _parkingServiceMock.Setup(x => x.Remove(Id));
        _addressServiceMock.Setup(x => x.Remove(_parkingZoneTest.AddressId));

        //Act
        var result = _parkingController.DeleteConfirmed(Id);

        //Assert
        var res = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equivalent(res.ActionName, "Index");
        _parkingServiceMock.Verify(x => x.RetrieveById(Id), Times.Once());
        _parkingServiceMock.Verify(x => x.Remove(Id), Times.Once());
        _addressServiceMock.Verify(x => x.Remove(_parkingZoneTest.AddressId), Times.Once());
    }
    #endregion
}
