using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingZoneVMs;

public class CreateVMTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { "Zone", "Wall Street", "New York", "New York", "10005", null , false },
            new object[] { "Zone", null, "New York", "New York", "10005", "United States", false },
            new object[] { "Zone", "Wall Street", null, "New York", "10005", "United States", false },
            new object[] { "Zone", "Wall Street", "New York", null, "10005", "United States" , false },
            new object[] { "Zone", "Wall Street", "New York", "New York", null, "United States", false },
            new object[] { null, "Wall Street", "New York", "New York", "10005", "United States", false },
            new object[] { "Zone", "Wall Street", "New York", "New York", "10005", "Unitet States" , true},
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingCreateVM_ThenValidationIsPerformed
     (
        string zoneName,
        string street,
        string city,
        string country,
        string postalCode,
        string province,
        bool expectedValidation
     )
    {
        //Arrange
        CreateVM createVM = new()
        {
            City = city,
            Street = street,
            Name = zoneName,
            Country = country,
            Province = province,
            PostalCode = postalCode
        };

        var validationContext = new ValidationContext(createVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act 
        var result = Validator.TryValidateObject(createVM, validationContext, validationResults);

        //Assert
        Assert.Equal(result, expectedValidation);
    }
}
