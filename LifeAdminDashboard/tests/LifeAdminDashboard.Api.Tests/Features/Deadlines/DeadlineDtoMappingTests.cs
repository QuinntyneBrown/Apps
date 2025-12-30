using LifeAdminDashboard.Api.Features.Deadlines;

namespace LifeAdminDashboard.Api.Tests.Features.Deadlines;

[TestFixture]
public class DeadlineDtoMappingTests
{
    [Test]
    public void ToDto_MapsAllProperties()
    {
        // Arrange
        var deadline = new Deadline
        {
            DeadlineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Deadline",
            Description = "Test Description",
            DeadlineDateTime = DateTime.UtcNow.AddDays(30),
            Category = "Financial",
            IsCompleted = false,
            CompletionDate = null,
            RemindersEnabled = true,
            ReminderDaysAdvance = 7,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = deadline.ToDto();

        // Assert
        Assert.That(dto.DeadlineId, Is.EqualTo(deadline.DeadlineId));
        Assert.That(dto.UserId, Is.EqualTo(deadline.UserId));
        Assert.That(dto.Title, Is.EqualTo(deadline.Title));
        Assert.That(dto.Description, Is.EqualTo(deadline.Description));
        Assert.That(dto.DeadlineDateTime, Is.EqualTo(deadline.DeadlineDateTime));
        Assert.That(dto.Category, Is.EqualTo(deadline.Category));
        Assert.That(dto.IsCompleted, Is.EqualTo(deadline.IsCompleted));
        Assert.That(dto.CompletionDate, Is.EqualTo(deadline.CompletionDate));
        Assert.That(dto.RemindersEnabled, Is.EqualTo(deadline.RemindersEnabled));
        Assert.That(dto.ReminderDaysAdvance, Is.EqualTo(deadline.ReminderDaysAdvance));
        Assert.That(dto.Notes, Is.EqualTo(deadline.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(deadline.CreatedAt));
    }
}
