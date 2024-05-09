using Moq;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.Service.Services;
using System.Text.Json;

namespace Parking_Zone.Test.Services;

public class ReservationServiceTests
{
    private readonly Mock<IReservationRepository> _reservationRepoMock;
    private readonly IReservationService _reservationServiceMock;
    private readonly Reservation _reservationTest;
    private readonly long Id = 1;
    public ReservationServiceTests()
    {
        _reservationRepoMock = new Mock<IReservationRepository>();
        _reservationServiceMock = new ReservationService(_reservationRepoMock.Object);
        _reservationTest = new()
        {
            Id = Id,
            StartTime = DateTime.UtcNow,
            Duration = 2,
            VehicleNumber = "30A333AAUZ",
            ParkingSlotId = Id,
            ParkingSlot = new(),
            AppUserId = Id.ToString()
        };
    }

    #region Insert
    [Fact]
    public void GivenReservation_WhenInsertIsCalled_ThenRepositoryCreateIsCalled()
    {
        //Arrange
        _reservationRepoMock.Setup(x => x.Create(_reservationTest));

        //Act
        _reservationServiceMock.Insert(_reservationTest);

        //Assert
        _reservationRepoMock.Verify(x => x.Create(_reservationTest), Times.Once);
    }
    #endregion

    #region Update
    [Fact]
    public void GivenReservation_WhenUpdateIsCalled_ThenRepositoryUpdateIsCalled()
    {
        //Arrange
        _reservationRepoMock.Setup(x => x.Update(_reservationTest));

        //Act
        _reservationServiceMock.Update(_reservationTest);

        //Assert
        _reservationRepoMock.Verify(x => x.Update(_reservationTest), Times.Once);
    }
    #endregion

    #region Remove
    [Fact]
    public void GivenId_WhenRemoveIsCalled_ThenRepositoryDeleteIsCalled()
    {
        //Arrange
        _reservationRepoMock.Setup(x => x.Delete(Id));

        //Act
        _reservationServiceMock.Remove(Id);

        //Assert
        _reservationRepoMock.Verify(x => x.Delete(Id), Times.Once);
    }
    #endregion

    #region RetrieveAll
    [Fact]
    public void GivenNothing_WhenRetrieveAllIsCalled_ThenReturnsListOfReservations()
    {
        //Arrange
        IEnumerable<Reservation> expectedReservations = [_reservationTest];

        _reservationRepoMock
            .Setup(x => x.GetAll())
            .Returns(expectedReservations);

        //Act
        var result = _reservationServiceMock.RetrieveAll();

        //Assert
        Assert.Equal(JsonSerializer.Serialize(expectedReservations),JsonSerializer.Serialize(result));
        _reservationRepoMock.Verify(x => x.GetAll(), Times.Once);
    }
    #endregion

    #region RetrieveById
    [Fact]
    public void GivenId_WhenRetrieveByIdIsCalled_ThenReturnsReservation()
    {
        //Arrange
        _reservationRepoMock
            .Setup(x => x.Get(Id))
            .Returns(_reservationTest);

        //Act
        var result = _reservationServiceMock.RetrieveById(Id);

        //Assert
        Assert.Equal(JsonSerializer.Serialize(_reservationTest), JsonSerializer.Serialize(result));
        _reservationRepoMock.Verify(x => x.Get(Id), Times.Once);
    }
    #endregion
}
