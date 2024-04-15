using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingZoneVMs;

public class CreateVMTests
{
    public static IEnumerable<object[]> testCreateVMData =>
        new List<object[]>
        {
            new object[] { null, "Wall Street", "New York", "New York", "10005", "United States" },
            new object[] { "Zone", null, "New York", "New York", "10005", "United States" },
            new object[] { "Zone", "Wall Street", null, "New York", "10005", "United States" },
            new object[] { "Zone", "Wall Street", "New York", null, "10005", "United States" },
            new object[] { "Zone", "Wall Street", "New York", "New York", null, "United States" },
            new object[] { "Zone", "Wall Street", "New York", "New York", "10005", null },
        };

    [Theory]
    [MemberData(nameof(testCreateVMData))]
    public void GivenInvalidCreateVM_WhenAnyPropertyIsNull_ThenCannotPassFromValidation
     (
        string zoneName, 
        string street, 
        string city, 
        string province, 
        string postalCode, 
        string country 
     )
    {
        //Arrange
        CreateVM createVM = new()
        {
            Name = zoneName,
            Street = street,
            City = city,
            Province = province,
            PostalCode = postalCode,
            Country = country
        };

        var validationContext = new ValidationContext(createVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act 
        var isValidResult = Validator.TryValidateObject(createVM, validationContext, validationResults);

        //Assert
        Assert.NotEmpty(validationResults);
        Assert.False(isValidResult);
    }
}
