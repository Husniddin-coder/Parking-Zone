using Parking_Zone.Domain.Enums;
using Parking_Zone.MVC.ViewModels.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Test.ModelValidationTests.ParkingSlotVMs;

public class IndexVMTests
{
    public static IEnumerable<object[]> TestData
        => new List<object[]>()
        {
            new object[] { 1, null, new List<ListOfSlotVMs>()
            { new(
                new()
                {
                    Id = 1,
                    Number = 5,
                    IsBooked = true,
                    Category = SlotCategory.Premium,
                    FeePerHour = 10,
                    ParkingZoneId = 1,
                    ParkingZone = new()
                    {
                        Id = 1,
                        Name = "Parking Zone",
                        Address = new()
                        {
                            Id = 1,
                            Street = "Wall Street",
                            City = "New York",
                            Province = "New York",
                            PostalCode = "10005",
                            Country = "United States"
                        }
                    }
                })
            }, false },
            new object[] { 1, "Zone", new List<ListOfSlotVMs>()
            { new(
                new()
                {
                    Id = 1,
                    Number = 5,
                    IsBooked = true,
                    Category = SlotCategory.Premium,
                    FeePerHour = 10,
                    ParkingZoneId = 1,
                    ParkingZone = new()
                    {
                        Id = 1,
                        Name = "Parking Zone",
                        Address = new()
                        {
                            Id = 1,
                            Street = "Wall Street",
                            City = "New York",
                            Province = "New York",
                            PostalCode = "10005",
                            Country = "United States"
                        }
                    }
                })
            }, true },
            new object[] { 1, "Zone", null, false }
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void GivenItemToBeValidated_WhenCreatingIndexVM_ThenValidationIsPerformed
        (
            long id,
            string zoneName,
            IEnumerable<ListOfSlotVMs> slots,
            bool expectedValidation
        )
    {
        //Arrange
        IndexVM indexVM = new()
        {
            ZoneId = id,
            ZoneName = zoneName,
            SlotsVMs = slots,
        };

        var validationContext = new ValidationContext(indexVM, null, null);
        var validationResults = new List<ValidationResult>();

        //Act 
        var result = Validator.TryValidateObject(indexVM, validationContext, validationResults);

        //Assert
        Assert.Equal(result, expectedValidation);
    }
}
