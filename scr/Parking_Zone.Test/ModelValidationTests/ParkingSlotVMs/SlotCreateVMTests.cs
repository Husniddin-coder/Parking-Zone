using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;

namespace Parking_Zone.Test.ModelValidationTests.ParkingSlotVMs;

public class SlotCreateVMTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { 5,1, true, 40.54M, SlotCategory.Standart, null, false },
            new object[] { 5,1, true, 40.54M, SlotCategory.Standart, "Zone", true },
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingSlotCreateVM_ThenValidationIsPerformed
     (
        int number,
        long zoneId,
        bool isBooked,
        decimal feePerHour,
        SlotCategory category,
        string zoneName,
        bool expectedValidation
     )
    {
        //Arrange
        SlotCreateVM slotCreateVM = new()
        {
            Number = number,
            IsAvailable = isBooked,
            Category = category,
            ParkingZoneId = zoneId,
            FeePerHour = feePerHour,
            ParkingZoneName = zoneName
        };

        var validationContext = new ValidationContext(slotCreateVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act 
        var result = Validator.TryValidateObject(slotCreateVM, validationContext, validationResults);

        //Assert
        Assert.Equal(result, expectedValidation);
    }
}
