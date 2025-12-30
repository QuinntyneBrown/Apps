using LifeAdminDashboard.Api.Features.AdminTasks;

namespace LifeAdminDashboard.Api.Tests.Features.AdminTasks;

[TestFixture]
public class AdminTaskDtoMappingTests
{
    [Test]
    public void ToDto_MapsAllProperties()
    {
        // Arrange
        var task = new AdminTask
        {
            AdminTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Test Description",
            Category = TaskCategory.Financial,
            Priority = TaskPriority.High,
            DueDate = DateTime.UtcNow.AddDays(7),
            IsCompleted = false,
            CompletionDate = null,
            IsRecurring = true,
            RecurrencePattern = "Monthly",
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = task.ToDto();

        // Assert
        Assert.That(dto.AdminTaskId, Is.EqualTo(task.AdminTaskId));
        Assert.That(dto.UserId, Is.EqualTo(task.UserId));
        Assert.That(dto.Title, Is.EqualTo(task.Title));
        Assert.That(dto.Description, Is.EqualTo(task.Description));
        Assert.That(dto.Category, Is.EqualTo(task.Category));
        Assert.That(dto.Priority, Is.EqualTo(task.Priority));
        Assert.That(dto.DueDate, Is.EqualTo(task.DueDate));
        Assert.That(dto.IsCompleted, Is.EqualTo(task.IsCompleted));
        Assert.That(dto.CompletionDate, Is.EqualTo(task.CompletionDate));
        Assert.That(dto.IsRecurring, Is.EqualTo(task.IsRecurring));
        Assert.That(dto.RecurrencePattern, Is.EqualTo(task.RecurrencePattern));
        Assert.That(dto.Notes, Is.EqualTo(task.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(task.CreatedAt));
    }
}
