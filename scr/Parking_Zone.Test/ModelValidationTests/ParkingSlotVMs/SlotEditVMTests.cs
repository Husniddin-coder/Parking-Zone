using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingSlotVMs;

public class SlotEditVMTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { 1, 5, 1, true, 40.54M, DateTime.Now, SlotCategory.Standart, null, false },
            new object[] { 1, 5, 1, true, 40.54M, DateTime.Now, SlotCategory.Standart, "Zone", true },
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingSlotEditVM_ThenValidationIsPerformed
     (
        long id,
        int number,
        long zoneId,
        bool IsAvailable,
        decimal feePerHour,
        DateTime createdAt,
        SlotCategory category,
        string zoneName,
        bool expectedValidation
     )
    {
        //Arrange
        SlotDetailsVM slotDetails = new()
        {
            Id = id,
            Number = number,
            IsAvailable = IsAvailable,
            Category = category,
            CreatedAt = createdAt,
            ParkingZoneId = zoneId,
            FeePerHour = feePerHour,
            ParkingZoneName = zoneName
        };

        var validationContext = new ValidationContext(slotDetails, null, null);
        var validationResults = new List<ValidationResult>();

        //Act 
        var result = Validator.TryValidateObject(slotDetails, validationContext, validationResults);

        //Assert
        Assert.Equal(result, expectedValidation);
    }
}
