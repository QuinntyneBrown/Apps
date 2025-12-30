// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Infrastructure.Tests;

/// <summary>
/// Unit tests for the LifeAdminDashboardContext.
/// </summary>
[TestFixture]
public class LifeAdminDashboardContextTests
{
    private LifeAdminDashboardContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<LifeAdminDashboardContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new LifeAdminDashboardContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that AdminTasks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task AdminTasks_CanAddAndRetrieve()
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
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Tasks.FindAsync(task.AdminTaskId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Task"));
        Assert.That(retrieved.Category, Is.EqualTo(TaskCategory.Financial));
        Assert.That(retrieved.Priority, Is.EqualTo(TaskPriority.High));
    }

    /// <summary>
    /// Tests that Renewals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Renewals_CanAddAndRetrieve()
    {
        // Arrange
        var renewal = new Renewal
        {
            RenewalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Renewal",
            RenewalType = "Subscription",
            Provider = "Test Provider",
            RenewalDate = DateTime.UtcNow.AddMonths(1),
            Cost = 99.99m,
            Frequency = "Monthly",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Renewals.Add(renewal);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Renewals.FindAsync(renewal.RenewalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Renewal"));
        Assert.That(retrieved.RenewalType, Is.EqualTo("Subscription"));
        Assert.That(retrieved.Cost, Is.EqualTo(99.99m));
    }

    /// <summary>
    /// Tests that Deadlines can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Deadlines_CanAddAndRetrieve()
    {
        // Arrange
        var deadline = new Deadline
        {
            DeadlineId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Deadline",
            Description = "Test Description",
            DeadlineDateTime = DateTime.UtcNow.AddDays(14),
            Category = "Financial",
            IsCompleted = false,
            RemindersEnabled = true,
            ReminderDaysAdvance = 7,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Deadlines.Add(deadline);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Deadlines.FindAsync(deadline.DeadlineId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Deadline"));
        Assert.That(retrieved.Category, Is.EqualTo("Financial"));
        Assert.That(retrieved.RemindersEnabled, Is.True);
    }

    /// <summary>
    /// Tests that tasks can be updated.
    /// </summary>
    [Test]
    public async Task AdminTasks_CanUpdate()
    {
        // Arrange
        var task = new AdminTask
        {
            AdminTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Original Title",
            Category = TaskCategory.Financial,
            Priority = TaskPriority.Low,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        // Act
        task.Title = "Updated Title";
        task.Priority = TaskPriority.High;
        await _context.SaveChangesAsync();

        var retrieved = await _context.Tasks.FindAsync(task.AdminTaskId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Updated Title"));
        Assert.That(retrieved.Priority, Is.EqualTo(TaskPriority.High));
    }
}
