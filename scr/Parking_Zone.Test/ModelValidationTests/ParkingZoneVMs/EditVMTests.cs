using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingZoneVMs;

public class EditVMTests
{
    public static IEnumerable<object[]> editVMTestData =>
       new List<object[]>
       {
            new object[] { null, "Parking Zone", "Wall Street", "New York", "New York", "10005", "United States" },
            new object[] { 1, null, "Wall Street", "New York", "New York", "10005", "United States" },
            new object[] { 1,"Zone",  null, "New York", "New York", "10005", "United States" },
            new object[] { 1,"Zone", "Wall Street", null, "New York", "10005", "United States" },
            new object[] { 1,"Zone", "Wall Street", "New York", null, "10005", "United States"  },
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", null, "United States"  },
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", "10005", null},
       };

    [Theory]
    [MemberData(nameof(editVMTestData))]
    public void GivenInvalidEditVM_WhenAnyPropertyIsNull_ThenCannotPassFromValidation
        (
            Int32? id,
            string zoneName,
            string street,
            string city,
            string province,
            string postalCode,
            string country
        )
    {
        //Arrange

        EditVM editVM = new()
        {
            Id = id,
            Name = zoneName,
            Street = street,
            City = city,
            Province = province,
            PostalCode = postalCode,
            Country = country
        };

        var validationContext = new ValidationContext(editVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act
        var isValidResults = Validator.TryValidateObject(editVM, validationContext, validationResults);

        //Assert
        Assert.NotEmpty(validationResults);
        Assert.False(isValidResults);
    }

}
