// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Api.Tests;

/// <summary>
/// Tests for Activity feature handlers.
/// </summary>
[TestFixture]
public class ActivityFeatureTests
{
    private Mock<IKidsActivitySportsTrackerContext> _mockContext = null!;
    private List<Core.Activity> _activities = null!;

    [SetUp]
    public void Setup()
    {
        _activities = new List<Core.Activity>();
        _mockContext = new Mock<IKidsActivitySportsTrackerContext>();
        _mockContext.Setup(c => c.Activities).Returns(TestHelpers.CreateMockDbSet(_activities).Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
    }

    [Test]
    public async Task CreateActivityCommand_CreatesActivity()
    {
        // Arrange
        var handler = new CreateActivityCommandHandler(_mockContext.Object);
        var command = new CreateActivityCommand
        {
            UserId = Guid.NewGuid(),
            ChildName = "John Doe",
            Name = "Soccer",
            ActivityType = ActivityType.TeamSports,
            Organization = "Youth League"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ChildName, Is.EqualTo(command.ChildName));
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(_activities.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetActivitiesQuery_ReturnsAllActivities()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _activities.Add(new Core.Activity
        {
            ActivityId = Guid.NewGuid(),
            UserId = userId,
            ChildName = "John",
            Name = "Soccer",
            ActivityType = ActivityType.TeamSports
        });
        _activities.Add(new Core.Activity
        {
            ActivityId = Guid.NewGuid(),
            UserId = userId,
            ChildName = "Jane",
            Name = "Piano",
            ActivityType = ActivityType.Music
        });

        var handler = new GetActivitiesQueryHandler(_mockContext.Object);
        var query = new GetActivitiesQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetActivityByIdQuery_ReturnsActivity()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        _activities.Add(new Core.Activity
        {
            ActivityId = activityId,
            UserId = Guid.NewGuid(),
            ChildName = "John",
            Name = "Soccer",
            ActivityType = ActivityType.TeamSports
        });

        var handler = new GetActivityByIdQueryHandler(_mockContext.Object);
        var query = new GetActivityByIdQuery { ActivityId = activityId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ActivityId, Is.EqualTo(activityId));
    }

    [Test]
    public async Task UpdateActivityCommand_UpdatesActivity()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        _activities.Add(new Core.Activity
        {
            ActivityId = activityId,
            UserId = Guid.NewGuid(),
            ChildName = "John",
            Name = "Soccer",
            ActivityType = ActivityType.TeamSports
        });

        var handler = new UpdateActivityCommandHandler(_mockContext.Object);
        var command = new UpdateActivityCommand
        {
            ActivityId = activityId,
            ChildName = "John Updated",
            Name = "Soccer Updated",
            ActivityType = ActivityType.TeamSports
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.ChildName, Is.EqualTo("John Updated"));
        Assert.That(result.Name, Is.EqualTo("Soccer Updated"));
    }

    [Test]
    public void UpdateActivityCommand_ThrowsWhenActivityNotFound()
    {
        // Arrange
        var handler = new UpdateActivityCommandHandler(_mockContext.Object);
        var command = new UpdateActivityCommand
        {
            ActivityId = Guid.NewGuid(),
            ChildName = "John",
            Name = "Soccer",
            ActivityType = ActivityType.TeamSports
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task DeleteActivityCommand_DeletesActivity()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        _activities.Add(new Core.Activity
        {
            ActivityId = activityId,
            UserId = Guid.NewGuid(),
            ChildName = "John",
            Name = "Soccer",
            ActivityType = ActivityType.TeamSports
        });

        var handler = new DeleteActivityCommandHandler(_mockContext.Object);
        var command = new DeleteActivityCommand { ActivityId = activityId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(_activities.Count, Is.EqualTo(0));
    }

    [Test]
    public void DeleteActivityCommand_ThrowsWhenActivityNotFound()
    {
        // Arrange
        var handler = new DeleteActivityCommandHandler(_mockContext.Object);
        var command = new DeleteActivityCommand { ActivityId = Guid.NewGuid() };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }
}
