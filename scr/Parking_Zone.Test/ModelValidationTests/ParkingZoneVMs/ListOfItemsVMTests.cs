using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingZoneVMs;

public class ListOfItemsVMTests
{
    public static IEnumerable<object[]> listOfItemsVMTestData =>
        new List<object[]>
        {
            new object[] { null, "Parking Zone", "Wall Street", "New York", "New York", "10005", "United States", DateTime.Now },
            new object[] { 1, null, "Wall Street", "New York", "New York", "10005", "United States", DateTime.Now },
            new object[] { 1,"Zone",  null, "New York", "New York", "10005", "United States", DateTime.Now },
            new object[] { 1,"Zone", "Wall Street", null, "New York", "10005", "United States", DateTime.Now },
            new object[] { 1,"Zone", "Wall Street", "New York", null, "10005", "United States", DateTime.Now  },
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", null, "United States", DateTime.Now  },
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", "10005", null ,DateTime.Now },
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", "10005", "United States", null },
        };

    [Theory]
    [MemberData(nameof(listOfItemsVMTestData))]
    public void GivenInvalidListOfItemsVM_WhenAnyPropertyIsNull_ThenCannotPassFromValidation
     (
        Int32? id,
        string zoneName,
        string street,
        string city,
        string province,
        string postalCode,
        string country,
        DateTime? createdAt
     )
    {
        //Arrange 
        ListOfItemsVM listOfItemsVM = new()
        {
            Id = id,
            Name = zoneName,
            Street = street,
            City = city,
            Province = province,
            PostalCode = postalCode,
            Country = country,
            CreatedAt = createdAt
        };

        var varlidationContext = new ValidationContext(listOfItemsVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act
        var isValidResult = Validator.TryValidateObject(listOfItemsVM, varlidationContext, validationResults);

        //Assert
        Assert.NotEmpty(validationResults);
        Assert.False(isValidResult);
    }
}
