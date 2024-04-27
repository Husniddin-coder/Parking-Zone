using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingSlotVMs;

public class ListOfSlotVMsTest
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { 1, 5, 1, true, 40.54M, DateTime.Now, SlotCategory.Standart, true }
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingListOfSlotVMs_ThenValidationIsPerformed
     (
        long id,
        int number,
        long zoneId,
        bool isBooked,
        decimal feePerHour,
        DateTime createdAt,
        SlotCategory category,
        bool expectedValidation
     )
    {
        //Arrange 
        ListOfSlotVMs listOfSlotVMs = new()
        {
            Id = id,
            Number = number,
            Category = category,
            IsBooked = isBooked,
            CreatedAt = createdAt,
            ParkingZoneId = zoneId,
            FeePerHour = feePerHour,
        };

        var varlidationContext = new ValidationContext(listOfSlotVMs, null, null);
        var validationResults = new List<ValidationResult>();

        //Act
        var result = Validator.TryValidateObject(listOfSlotVMs, varlidationContext, validationResults);

        //Assert
        Assert.Equal(result, expectedValidation);
    }
}
