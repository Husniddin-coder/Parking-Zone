using Moq;
using System.Text.Json;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Services;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Test.Services;

public class AddressServiceTests
{
    private readonly Mock<IAddressRepository> _repositoryMock;
    private readonly IAddressService _service;
    private readonly Address _addressTest;
    private readonly long Id = 1;

    public AddressServiceTests()
    {
        _repositoryMock = new Mock<IAddressRepository>();
        _service = new AddressService(_repositoryMock.Object);
        _addressTest = new()
        {
            Id = Id,
            Street = "Wall Street",
            City = "New York",
            Province = "New York",
            PostalCode = "10005",
            Country = "United States"
        };
    }

    #region Insert
    [Fact]
    public void GivenAddressModel_WhenInsertIsCalled_ThenRepositoryCreateIsCalled()
    {
        //Arrange
        _repositoryMock.Setup(x=> x.Create(_addressTest));

        //Act 
        _service.Insert(_addressTest);

        //Assert
        _repositoryMock.Verify(x=> x.Create(_addressTest), Times.Once());
    }
    #endregion

    #region Update
    [Fact]
    public void GivenAddressModel_WhenUpdateIsCalled_ThenRepositoryUpdateIsCalled()
    {
        //Arrange
        _repositoryMock.Setup(x => x.Update(_addressTest));

        //Act 
        _service.Update(_addressTest);

        //Assert
        _repositoryMock.Verify(x => x.Update(_addressTest), Times.Once());
    }
    #endregion

    #region Remove
    [Fact]
    public void GivenId_WhenRemoveIsCalled_ThenRepositoryDeleteIsCalled()
    {
        //Arrange
        _repositoryMock.Setup(x => x.Delete(Id));

        //Act
        _service.Remove(Id);

        //Assert
        _repositoryMock.Verify(x=> x.Delete(Id), Times.Once());
    }
    #endregion

    #region RetrieveAll
    [Fact]
    public void GivenNothing_WhenRetrieveAllIsCalled_ThenReturnsListOfAddresses()
    {
        //Arrange
        IEnumerable<Address> expectedAdresses = new List<Address>() { _addressTest };

        _repositoryMock
            .Setup(x=> x.GetAll())
            .Returns(expectedAdresses);

        //Act
        var result = _service.RetrieveAll();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<Address>>(result);
        Assert.Equal(JsonSerializer.Serialize(expectedAdresses),JsonSerializer.Serialize(result));
        _repositoryMock.Verify(x=> x.GetAll(), Times.Once());
    }
    #endregion

    #region RetrieveById
    [Fact]
    public void GivenId_WhenRetrieveByIdIsCalled_ThenReturnsAddress()
    {
        //Arrange
        _repositoryMock.Setup(x => x.Get(Id)).Returns(_addressTest);

        //Act
        var result = _service.RetrieveById(Id);

        //Assert
        Assert.IsType<Address>(result);
        Assert.Equal(JsonSerializer.Serialize(_addressTest), JsonSerializer.Serialize(result));
        _repositoryMock.Verify(x => x.Get(Id), Times.Once());
    }
    #endregion
}
