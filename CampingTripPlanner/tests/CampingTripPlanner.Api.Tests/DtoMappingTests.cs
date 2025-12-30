using CampingTripPlanner.Api.Features.Trips;
using CampingTripPlanner.Api.Features.Campsites;
using CampingTripPlanner.Api.Features.GearChecklists;
using CampingTripPlanner.Api.Features.Reviews;

namespace CampingTripPlanner.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void TripDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var trip = new Core.Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Trip",
            CampsiteId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(3),
            NumberOfPeople = 4,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = trip.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TripId, Is.EqualTo(trip.TripId));
            Assert.That(dto.UserId, Is.EqualTo(trip.UserId));
            Assert.That(dto.Name, Is.EqualTo(trip.Name));
            Assert.That(dto.CampsiteId, Is.EqualTo(trip.CampsiteId));
            Assert.That(dto.StartDate, Is.EqualTo(trip.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(trip.EndDate));
            Assert.That(dto.NumberOfPeople, Is.EqualTo(trip.NumberOfPeople));
            Assert.That(dto.Notes, Is.EqualTo(trip.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(trip.CreatedAt));
        });
    }

    [Test]
    public void CampsiteDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var campsite = new Core.Campsite
        {
            CampsiteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Campsite",
            Location = "Test Location",
            CampsiteType = Core.CampsiteType.Tent,
            Description = "Test description",
            HasElectricity = true,
            HasWater = true,
            CostPerNight = 25.00m,
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = campsite.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.CampsiteId, Is.EqualTo(campsite.CampsiteId));
            Assert.That(dto.UserId, Is.EqualTo(campsite.UserId));
            Assert.That(dto.Name, Is.EqualTo(campsite.Name));
            Assert.That(dto.Location, Is.EqualTo(campsite.Location));
            Assert.That(dto.CampsiteType, Is.EqualTo(campsite.CampsiteType));
            Assert.That(dto.Description, Is.EqualTo(campsite.Description));
            Assert.That(dto.HasElectricity, Is.EqualTo(campsite.HasElectricity));
            Assert.That(dto.HasWater, Is.EqualTo(campsite.HasWater));
            Assert.That(dto.CostPerNight, Is.EqualTo(campsite.CostPerNight));
            Assert.That(dto.IsFavorite, Is.EqualTo(campsite.IsFavorite));
            Assert.That(dto.CreatedAt, Is.EqualTo(campsite.CreatedAt));
        });
    }

    [Test]
    public void GearChecklistDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var gearChecklist = new Core.GearChecklist
        {
            GearChecklistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            ItemName = "Test Item",
            IsPacked = true,
            Quantity = 2,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = gearChecklist.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GearChecklistId, Is.EqualTo(gearChecklist.GearChecklistId));
            Assert.That(dto.UserId, Is.EqualTo(gearChecklist.UserId));
            Assert.That(dto.TripId, Is.EqualTo(gearChecklist.TripId));
            Assert.That(dto.ItemName, Is.EqualTo(gearChecklist.ItemName));
            Assert.That(dto.IsPacked, Is.EqualTo(gearChecklist.IsPacked));
            Assert.That(dto.Quantity, Is.EqualTo(gearChecklist.Quantity));
            Assert.That(dto.Notes, Is.EqualTo(gearChecklist.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(gearChecklist.CreatedAt));
        });
    }

    [Test]
    public void ReviewDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var review = new Core.Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CampsiteId = Guid.NewGuid(),
            Rating = 5,
            ReviewText = "Great campsite!",
            ReviewDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = review.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ReviewId, Is.EqualTo(review.ReviewId));
            Assert.That(dto.UserId, Is.EqualTo(review.UserId));
            Assert.That(dto.CampsiteId, Is.EqualTo(review.CampsiteId));
            Assert.That(dto.Rating, Is.EqualTo(review.Rating));
            Assert.That(dto.ReviewText, Is.EqualTo(review.ReviewText));
            Assert.That(dto.ReviewDate, Is.EqualTo(review.ReviewDate));
            Assert.That(dto.CreatedAt, Is.EqualTo(review.CreatedAt));
        });
    }
}
