using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingZoneVMs;

public class DetailsVMTests
{
    public static IEnumerable<object[]> detailsVMTestData =>
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
    [MemberData(nameof(detailsVMTestData))]
    public void GivenInvalidDetailsVM_WhenAnyPropertyIsNull_ThenCannotPassFromValidation
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
        DetailsVM detailsVM = new()
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

        var varlidationContext = new ValidationContext(detailsVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act
        var isValidResult = Validator.TryValidateObject(detailsVM, varlidationContext, validationResults);

        //Assert
        Assert.NotEmpty(validationResults);
        Assert.False(isValidResult);
    }
}
