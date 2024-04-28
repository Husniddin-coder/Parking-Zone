using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingZoneVMs;

public class EditVMTests
{
    public static IEnumerable<object[]> TestData =>
       new List<object[]>
       {
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", "10005", null, false},
            new object[] { 1,"Zone",  null, "New York", "New York", "10005", "United States", false },
            new object[] { 1,"Zone", "Wall Street", null, "New York", "10005", "United States", false },
            new object[] { 1,"Zone", "Wall Street", "New York", null, "10005", "United States", false  },
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", null, "United States", false },
            new object[] { 1, null, "Wall Street", "New York", "New York", "10005", "United States", false },
            new object[] { 1, "Zone", "Wall Street", "New York", "New York", "10005", "United States", true }
       };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenInvalidEditVM_WhenAnyPropertyIsNull_ThenCannotPassFromValidation
        (
            long id,
            string zoneName,
            string street,
            string city,
            string country,
            string province,
            string postalCode,
            bool expectedValidation
        )
    {
        //Arrange
        EditVM editVM = new()
        {
            Id = id,
            City = city,
            Name = zoneName,
            Street = street,
            Country = country,
            Province = province,
            PostalCode = postalCode,
        };

        var validationContext = new ValidationContext(editVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act
        var result = Validator.TryValidateObject(editVM, validationContext, validationResults);

        //Assert
        Assert.Equal(result, expectedValidation);
    }
}
