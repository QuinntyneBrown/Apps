using TimeAuditTracker.Api.Features.TimeBlocks;
using TimeAuditTracker.Api.Features.AuditReports;
using TimeAuditTracker.Api.Features.Goals;

namespace TimeAuditTracker.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void TimeBlockDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var timeBlock = new Core.TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = Core.ActivityCategory.Work,
            Description = "Test Description",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(2),
            Notes = "Test Notes",
            Tags = "test,tags",
            IsProductive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = timeBlock.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TimeBlockId, Is.EqualTo(timeBlock.TimeBlockId));
            Assert.That(dto.UserId, Is.EqualTo(timeBlock.UserId));
            Assert.That(dto.Category, Is.EqualTo(timeBlock.Category));
            Assert.That(dto.Description, Is.EqualTo(timeBlock.Description));
            Assert.That(dto.StartTime, Is.EqualTo(timeBlock.StartTime));
            Assert.That(dto.EndTime, Is.EqualTo(timeBlock.EndTime));
            Assert.That(dto.Notes, Is.EqualTo(timeBlock.Notes));
            Assert.That(dto.Tags, Is.EqualTo(timeBlock.Tags));
            Assert.That(dto.IsProductive, Is.EqualTo(timeBlock.IsProductive));
            Assert.That(dto.CreatedAt, Is.EqualTo(timeBlock.CreatedAt));
            Assert.That(dto.DurationInMinutes, Is.EqualTo(timeBlock.GetDurationInMinutes()));
        });
    }

    [Test]
    public void AuditReportDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var report = new Core.AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Report",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 40.0,
            ProductiveHours = 32.0,
            Summary = "Test Summary",
            Insights = "Test Insights",
            Recommendations = "Test Recommendations",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = report.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.AuditReportId, Is.EqualTo(report.AuditReportId));
            Assert.That(dto.UserId, Is.EqualTo(report.UserId));
            Assert.That(dto.Title, Is.EqualTo(report.Title));
            Assert.That(dto.StartDate, Is.EqualTo(report.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(report.EndDate));
            Assert.That(dto.TotalTrackedHours, Is.EqualTo(report.TotalTrackedHours));
            Assert.That(dto.ProductiveHours, Is.EqualTo(report.ProductiveHours));
            Assert.That(dto.Summary, Is.EqualTo(report.Summary));
            Assert.That(dto.Insights, Is.EqualTo(report.Insights));
            Assert.That(dto.Recommendations, Is.EqualTo(report.Recommendations));
            Assert.That(dto.CreatedAt, Is.EqualTo(report.CreatedAt));
            Assert.That(dto.ProductivityPercentage, Is.EqualTo(report.GetProductivityPercentage()));
            Assert.That(dto.PeriodDays, Is.EqualTo(report.GetPeriodDays()));
        });
    }

    [Test]
    public void GoalDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var goal = new Core.Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = Core.ActivityCategory.Exercise,
            TargetHoursPerWeek = 5.0,
            MinimumHoursPerWeek = 3.0,
            Description = "Test Goal",
            IsActive = true,
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = goal.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GoalId, Is.EqualTo(goal.GoalId));
            Assert.That(dto.UserId, Is.EqualTo(goal.UserId));
            Assert.That(dto.Category, Is.EqualTo(goal.Category));
            Assert.That(dto.TargetHoursPerWeek, Is.EqualTo(goal.TargetHoursPerWeek));
            Assert.That(dto.MinimumHoursPerWeek, Is.EqualTo(goal.MinimumHoursPerWeek));
            Assert.That(dto.Description, Is.EqualTo(goal.Description));
            Assert.That(dto.IsActive, Is.EqualTo(goal.IsActive));
            Assert.That(dto.StartDate, Is.EqualTo(goal.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(goal.EndDate));
            Assert.That(dto.CreatedAt, Is.EqualTo(goal.CreatedAt));
            Assert.That(dto.TargetHoursPerDay, Is.EqualTo(goal.GetTargetHoursPerDay()));
        });
    }
}
