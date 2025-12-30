// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Api.Tests;

/// <summary>
/// Tests for Schedule feature handlers.
/// </summary>
[TestFixture]
public class ScheduleFeatureTests
{
    private Mock<IKidsActivitySportsTrackerContext> _mockContext = null!;
    private List<Core.Schedule> _schedules = null!;

    [SetUp]
    public void Setup()
    {
        _schedules = new List<Core.Schedule>();
        _mockContext = new Mock<IKidsActivitySportsTrackerContext>();
        _mockContext.Setup(c => c.Schedules).Returns(TestHelpers.CreateMockDbSet(_schedules).Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
    }

    [Test]
    public async Task CreateScheduleCommand_CreatesSchedule()
    {
        // Arrange
        var handler = new CreateScheduleCommandHandler(_mockContext.Object);
        var command = new CreateScheduleCommand
        {
            ActivityId = Guid.NewGuid(),
            EventType = "Practice",
            DateTime = new DateTime(2024, 3, 15, 14, 0, 0),
            Location = "City Park",
            DurationMinutes = 90,
            IsConfirmed = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.EventType, Is.EqualTo(command.EventType));
        Assert.That(result.Location, Is.EqualTo(command.Location));
        Assert.That(_schedules.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetSchedulesQuery_ReturnsAllSchedules()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        _schedules.Add(new Core.Schedule
        {
            ScheduleId = Guid.NewGuid(),
            ActivityId = activityId,
            EventType = "Practice",
            DateTime = new DateTime(2024, 3, 15, 14, 0, 0)
        });
        _schedules.Add(new Core.Schedule
        {
            ScheduleId = Guid.NewGuid(),
            ActivityId = activityId,
            EventType = "Game",
            DateTime = new DateTime(2024, 3, 20, 10, 0, 0)
        });

        var handler = new GetSchedulesQueryHandler(_mockContext.Object);
        var query = new GetSchedulesQuery { ActivityId = activityId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetScheduleByIdQuery_ReturnsSchedule()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        _schedules.Add(new Core.Schedule
        {
            ScheduleId = scheduleId,
            ActivityId = Guid.NewGuid(),
            EventType = "Practice",
            DateTime = new DateTime(2024, 3, 15, 14, 0, 0)
        });

        var handler = new GetScheduleByIdQueryHandler(_mockContext.Object);
        var query = new GetScheduleByIdQuery { ScheduleId = scheduleId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ScheduleId, Is.EqualTo(scheduleId));
    }

    [Test]
    public async Task UpdateScheduleCommand_UpdatesSchedule()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        _schedules.Add(new Core.Schedule
        {
            ScheduleId = scheduleId,
            ActivityId = Guid.NewGuid(),
            EventType = "Practice",
            DateTime = new DateTime(2024, 3, 15, 14, 0, 0),
            Location = "City Park"
        });

        var handler = new UpdateScheduleCommandHandler(_mockContext.Object);
        var command = new UpdateScheduleCommand
        {
            ScheduleId = scheduleId,
            EventType = "Game",
            DateTime = new DateTime(2024, 3, 20, 10, 0, 0),
            Location = "Sports Complex"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.EventType, Is.EqualTo("Game"));
        Assert.That(result.Location, Is.EqualTo("Sports Complex"));
    }

    [Test]
    public void UpdateScheduleCommand_ThrowsWhenScheduleNotFound()
    {
        // Arrange
        var handler = new UpdateScheduleCommandHandler(_mockContext.Object);
        var command = new UpdateScheduleCommand
        {
            ScheduleId = Guid.NewGuid(),
            EventType = "Practice",
            DateTime = DateTime.UtcNow
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task DeleteScheduleCommand_DeletesSchedule()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        _schedules.Add(new Core.Schedule
        {
            ScheduleId = scheduleId,
            ActivityId = Guid.NewGuid(),
            EventType = "Practice",
            DateTime = DateTime.UtcNow
        });

        var handler = new DeleteScheduleCommandHandler(_mockContext.Object);
        var command = new DeleteScheduleCommand { ScheduleId = scheduleId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(_schedules.Count, Is.EqualTo(0));
    }

    [Test]
    public void DeleteScheduleCommand_ThrowsWhenScheduleNotFound()
    {
        // Arrange
        var handler = new DeleteScheduleCommandHandler(_mockContext.Object);
        var command = new DeleteScheduleCommand { ScheduleId = Guid.NewGuid() };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }
}
