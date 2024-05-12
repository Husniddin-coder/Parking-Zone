using Microsoft.AspNetCore.Mvc.Rendering;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using Parking_Zone.MVC.ViewModels.ReservationVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ReservationVMs;

public class FreeSlotsVMTests
{
    public static IEnumerable<object[]> TestData
        => [[1, 1, "zone", null, DateTime.UtcNow, null, true]];

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingFreeSlotsVM_ThenValidationIsPerformed
        (
            long zoneId,
            int duration,
            string zoneName,
            SelectList zones,
            DateTime startTime,
            List<ListOfSlotVMs> freeSlots,
            bool expectedValidationResult
        )
    {
        //Arrange
        FreeSlotsVM FreeSlots = new()
        {
            Zones = zones,
            ZoneId = zoneId,
            Duration = duration,
            ZoneName = zoneName,
            StartTime = startTime,
            FreeSlots = freeSlots
        };

        var validationContext = new ValidationContext(FreeSlots, null, null);
        var validationResults = new List<ValidationResult>();

        //Act
        var result = Validator.TryValidateObject(FreeSlots, validationContext, validationResults);

        //Assert
        Assert.Equal(result, expectedValidationResult);
    }
}
