using Parking_Zone.MVC.ViewModels.ParkingZoneVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingZoneVMs;

public class ListOfItemsVMTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", "10005", null ,DateTime.Now, false },
            new object[] { 1,"Zone",  null, "New York", "New York", "10005", "United States", DateTime.Now, false },
            new object[] { 1,"Zone", "Wall Street", null, "New York", "10005", "United States", DateTime.Now, false },
            new object[] { 1,"Zone", "Wall Street", "New York", null, "10005", "United States", DateTime.Now, false },
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", null, "United States", DateTime.Now, false },
            new object[] { 1, null, "Wall Street", "New York", "New York", "10005", "United States", DateTime.Now, false },
            new object[] { 1,"Zone", "Wall Street", "New York", "New York", "10005", "United States", DateTime.Now, true },
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingListOfItemsVM_ThenValidationIsPerformed
     (
        long id,
        string zoneName,
        string street,
        string city,
        string country,
        string province,
        string postalCode,
        DateTime createdAt,
        bool expectedValidation
     )
    {
        //Arrange 
        ListOfItemsVM listOfItemsVM = new()
        {
            Id = id,
            City = city,
            Street = street,
            Name = zoneName,
            Country = country,
            Province = province,
            CreatedAt = createdAt,
            PostalCode = postalCode
        };

        var varlidationContext = new ValidationContext(listOfItemsVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act
        var result = Validator.TryValidateObject(listOfItemsVM, varlidationContext, validationResults);

        //Assert
        Assert.Equal(result, expectedValidation);
    }
}
