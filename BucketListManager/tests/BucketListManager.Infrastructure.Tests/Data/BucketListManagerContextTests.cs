// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BucketListManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the BucketListManagerContext.
/// </summary>
[TestFixture]
public class BucketListManagerContextTests
{
    private BucketListManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BucketListManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BucketListManagerContext(options);
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
    /// Tests that BucketListItems can be added and retrieved.
    /// </summary>
    [Test]
    public async Task BucketListItems_CanAddAndRetrieve()
    {
        // Arrange
        var bucketListItem = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Bucket List Item",
            Description = "Test Description",
            Category = Category.Travel,
            Priority = Priority.High,
            Status = ItemStatus.NotStarted,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.BucketListItems.Add(bucketListItem);
        await _context.SaveChangesAsync();

        var retrieved = await _context.BucketListItems.FindAsync(bucketListItem.BucketListItemId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Bucket List Item"));
        Assert.That(retrieved.Category, Is.EqualTo(Category.Travel));
        Assert.That(retrieved.Priority, Is.EqualTo(Priority.High));
    }

    /// <summary>
    /// Tests that Milestones can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Milestones_CanAddAndRetrieve()
    {
        // Arrange
        var bucketListItem = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Item",
            Description = "Test Description",
            Category = Category.Learning,
            Priority = Priority.Medium,
            Status = ItemStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            UserId = bucketListItem.UserId,
            BucketListItemId = bucketListItem.BucketListItemId,
            Title = "Test Milestone",
            Description = "Milestone Description",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.BucketListItems.Add(bucketListItem);
        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Milestones.FindAsync(milestone.MilestoneId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Milestone"));
        Assert.That(retrieved.IsCompleted, Is.False);
    }

    /// <summary>
    /// Tests that Memories can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Memories_CanAddAndRetrieve()
    {
        // Arrange
        var bucketListItem = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Item",
            Description = "Test Description",
            Category = Category.Adventure,
            Priority = Priority.High,
            Status = ItemStatus.Completed,
            CreatedAt = DateTime.UtcNow,
        };

        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = bucketListItem.UserId,
            BucketListItemId = bucketListItem.BucketListItemId,
            Title = "Test Memory",
            Description = "Memory Description",
            MemoryDate = DateTime.UtcNow,
            PhotoUrl = "https://example.com/photo.jpg",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.BucketListItems.Add(bucketListItem);
        _context.Memories.Add(memory);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Memories.FindAsync(memory.MemoryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Memory"));
        Assert.That(retrieved.PhotoUrl, Is.EqualTo("https://example.com/photo.jpg"));
    }
}
