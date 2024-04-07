using Moq;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Services;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Test.Services;

public class AddressServiceTests
{
    private readonly Mock<IAddressRepository> _addressRepositoryMock;
    private readonly IAddressService _addressService;
    private readonly Address _addressTest;
    private readonly long id = 1;

    public AddressServiceTests()
    {
        _addressRepositoryMock = new Mock<IAddressRepository>();
        _addressService = new AddressService(_addressRepositoryMock.Object);
        _addressTest = new();
    }

    #region Insert
    [Fact]
    public void GivenAddressModel_WhenInsertIsCalled_ThenRepositoryCreateIsCalled()
    {
        //Arrange
        _addressRepositoryMock.Setup(x=> x.Create(_addressTest)).Returns(_addressTest);

        //Act 
        _addressService.Insert(_addressTest);

        //Assert
        _addressRepositoryMock.Verify(x=> x.Create(_addressTest), Times.Once());
    }
    #endregion

    #region Modify
    [Fact]
    public void GivenAddressModel_WhenModifyIsCalled_ThenRepositoryUpdateIsCalled()
    {
        //Arrange
        _addressRepositoryMock.Setup(x => x.Update(_addressTest)).Returns(_addressTest);

        //Act 
        _addressService.Modify(_addressTest);

        //Assert
        _addressRepositoryMock.Verify(x => x.Update(_addressTest), Times.Once());
    }
    #endregion

    #region Remove
    [Fact]
    public void GivenIdOfAddress_WhenRemoveIsCalled_ThenRepositoryDeleteIsCalled()
    {
        //Arrange
        _addressRepositoryMock.Setup(x=> x.Delete(id)).Returns(true);

        //Act
        _addressService.Remove(id);

        //Assert
        _addressRepositoryMock.Verify(x=> x.Delete(id), Times.Once());
    }
    #endregion

    #region RetrieveAll
    [Fact]
    public void GivenNothing_WhenRetrieveAllIsCalled_ThenReturnsListOfAddresses()
    {
        //Arrange
        IEnumerable<Address> addresses = Enumerable.Empty<Address>();
        _addressRepositoryMock.Setup(x=> x.GetAll()).Returns(addresses);

        //Act
        var result = _addressService.RetrieveAll();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<Address>>(result);
        _addressRepositoryMock.Verify(x=> x.GetAll(), Times.Once());
    }
    #endregion

    #region RetrieveById
    [Fact]
    public void GivenIdOfAddress_WhenRetrieveByIdIsCalled_ThenReturnsAddress()
    {
        //Arrange
        _addressRepositoryMock.Setup(x => x.Get(id)).Returns(_addressTest);

        //Act
        var result = _addressService.RetrieveById(id);

        //Assert
        Assert.IsType<Address>(result);
        _addressRepositoryMock.Verify(x => x.Get(id), Times.Once());
    }
    #endregion
}
