// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the TimeAuditTrackerContext.
/// </summary>
[TestFixture]
public class TimeAuditTrackerContextTests
{
    private TimeAuditTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TimeAuditTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TimeAuditTrackerContext(options);
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
    /// Tests that TimeBlocks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TimeBlocks_CanAddAndRetrieve()
    {
        // Arrange
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Project development",
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(2),
            Notes = "Working on feature implementation",
            Tags = "coding, development",
            IsProductive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.TimeBlocks.Add(timeBlock);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TimeBlocks.FindAsync(timeBlock.TimeBlockId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Project development"));
        Assert.That(retrieved.Category, Is.EqualTo(ActivityCategory.Work));
        Assert.That(retrieved.IsProductive, Is.True);
    }

    /// <summary>
    /// Tests that Goals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Goals_CanAddAndRetrieve()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Learning,
            TargetHoursPerWeek = 10.0,
            MinimumHoursPerWeek = 5.0,
            Description = "Improve technical skills",
            IsActive = true,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Improve technical skills"));
        Assert.That(retrieved.TargetHoursPerWeek, Is.EqualTo(10.0));
        Assert.That(retrieved.Category, Is.EqualTo(ActivityCategory.Learning));
    }

    /// <summary>
    /// Tests that AuditReports can be added and retrieved.
    /// </summary>
    [Test]
    public async Task AuditReports_CanAddAndRetrieve()
    {
        // Arrange
        var auditReport = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Weekly Time Audit",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 50.0,
            ProductiveHours = 40.0,
            Summary = "Productive week overall",
            Insights = "Good balance achieved",
            Recommendations = "Continue current pattern",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.AuditReports.Add(auditReport);
        await _context.SaveChangesAsync();

        var retrieved = await _context.AuditReports.FindAsync(auditReport.AuditReportId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Weekly Time Audit"));
        Assert.That(retrieved.TotalTrackedHours, Is.EqualTo(50.0));
        Assert.That(retrieved.ProductiveHours, Is.EqualTo(40.0));
    }

    /// <summary>
    /// Tests that TimeBlocks can be queried by user.
    /// </summary>
    [Test]
    public async Task TimeBlocks_CanQueryByUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var timeBlock1 = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = userId,
            Category = ActivityCategory.Work,
            Description = "Task 1",
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var timeBlock2 = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = userId,
            Category = ActivityCategory.Learning,
            Description = "Task 2",
            StartTime = DateTime.UtcNow.AddHours(1),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.TimeBlocks.AddRange(timeBlock1, timeBlock2);
        await _context.SaveChangesAsync();

        var userBlocks = await _context.TimeBlocks
            .Where(tb => tb.UserId == userId)
            .ToListAsync();

        // Assert
        Assert.That(userBlocks, Has.Count.EqualTo(2));
        Assert.That(userBlocks.All(tb => tb.UserId == userId), Is.True);
    }

    /// <summary>
    /// Tests that TimeBlocks can be updated.
    /// </summary>
    [Test]
    public async Task TimeBlocks_CanUpdate()
    {
        // Arrange
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Initial description",
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.TimeBlocks.Add(timeBlock);
        await _context.SaveChangesAsync();

        // Act
        timeBlock.EndActivity(DateTime.UtcNow.AddHours(2));
        timeBlock.Notes = "Updated notes";
        await _context.SaveChangesAsync();

        var retrieved = await _context.TimeBlocks.FindAsync(timeBlock.TimeBlockId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.EndTime, Is.Not.Null);
        Assert.That(retrieved.Notes, Is.EqualTo("Updated notes"));
    }

    /// <summary>
    /// Tests that TimeBlocks can be deleted.
    /// </summary>
    [Test]
    public async Task TimeBlocks_CanDelete()
    {
        // Arrange
        var timeBlock = new TimeBlock
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "To be deleted",
            StartTime = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.TimeBlocks.Add(timeBlock);
        await _context.SaveChangesAsync();

        // Act
        _context.TimeBlocks.Remove(timeBlock);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TimeBlocks.FindAsync(timeBlock.TimeBlockId);

        // Assert
        Assert.That(retrieved, Is.Null);
    }

    /// <summary>
    /// Tests that Goals can be deactivated.
    /// </summary>
    [Test]
    public async Task Goals_CanDeactivate()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Exercise,
            TargetHoursPerWeek = 5.0,
            Description = "Regular exercise",
            IsActive = true,
            StartDate = DateTime.UtcNow.AddMonths(-1),
            CreatedAt = DateTime.UtcNow.AddMonths(-1),
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        // Act
        goal.Deactivate();
        await _context.SaveChangesAsync();

        var retrieved = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.IsActive, Is.False);
        Assert.That(retrieved.EndDate, Is.Not.Null);
    }

    /// <summary>
    /// Tests that AuditReports can query by date range.
    /// </summary>
    [Test]
    public async Task AuditReports_CanQueryByDateRange()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var report1 = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = userId,
            Title = "Report 1",
            StartDate = DateTime.UtcNow.AddDays(-14),
            EndDate = DateTime.UtcNow.AddDays(-7),
            TotalTrackedHours = 40.0,
            ProductiveHours = 35.0,
            CreatedAt = DateTime.UtcNow.AddDays(-7),
        };

        var report2 = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = userId,
            Title = "Report 2",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 45.0,
            ProductiveHours = 38.0,
            CreatedAt = DateTime.UtcNow,
        };

        _context.AuditReports.AddRange(report1, report2);
        await _context.SaveChangesAsync();

        // Act
        var recentReports = await _context.AuditReports
            .Where(r => r.UserId == userId && r.StartDate >= DateTime.UtcNow.AddDays(-10))
            .ToListAsync();

        // Assert
        Assert.That(recentReports, Has.Count.EqualTo(1));
        Assert.That(recentReports[0].Title, Is.EqualTo("Report 2"));
    }
}
