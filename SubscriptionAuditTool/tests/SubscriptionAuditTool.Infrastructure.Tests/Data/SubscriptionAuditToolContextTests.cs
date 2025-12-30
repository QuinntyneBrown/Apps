// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SubscriptionAuditTool.Infrastructure.Tests;

/// <summary>
/// Unit tests for the SubscriptionAuditToolContext.
/// </summary>
[TestFixture]
public class SubscriptionAuditToolContextTests
{
    private SubscriptionAuditToolContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SubscriptionAuditToolContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SubscriptionAuditToolContext(options);
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
    /// Tests that Categories can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Categories_CanAddAndRetrieve()
    {
        // Arrange
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = "Entertainment",
            Description = "Streaming services",
            ColorCode = "#FF5733",
        };

        // Act
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Categories.FindAsync(category.CategoryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Entertainment"));
        Assert.That(retrieved.ColorCode, Is.EqualTo("#FF5733"));
    }

    /// <summary>
    /// Tests that Subscriptions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Subscriptions_CanAddAndRetrieve()
    {
        // Arrange
        var subscription = new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            ServiceName = "Netflix",
            Cost = 15.99m,
            BillingCycle = BillingCycle.Monthly,
            NextBillingDate = DateTime.UtcNow.AddDays(30),
            Status = SubscriptionStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-6),
            Notes = "Premium plan",
        };

        // Act
        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Subscriptions.FindAsync(subscription.SubscriptionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ServiceName, Is.EqualTo("Netflix"));
        Assert.That(retrieved.Cost, Is.EqualTo(15.99m));
        Assert.That(retrieved.Status, Is.EqualTo(SubscriptionStatus.Active));
    }

    /// <summary>
    /// Tests that Subscriptions can be associated with Categories.
    /// </summary>
    [Test]
    public async Task Subscriptions_CanAssociateWithCategory()
    {
        // Arrange
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = "Entertainment",
            Description = "Streaming services",
        };

        var subscription = new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            ServiceName = "Netflix",
            Cost = 15.99m,
            BillingCycle = BillingCycle.Monthly,
            NextBillingDate = DateTime.UtcNow.AddDays(30),
            Status = SubscriptionStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-6),
            CategoryId = category.CategoryId,
        };

        // Act
        _context.Categories.Add(category);
        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Subscriptions
            .Include(s => s.Category)
            .FirstOrDefaultAsync(s => s.SubscriptionId == subscription.SubscriptionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Category, Is.Not.Null);
        Assert.That(retrieved.Category!.Name, Is.EqualTo("Entertainment"));
    }

    /// <summary>
    /// Tests that Subscriptions can be updated.
    /// </summary>
    [Test]
    public async Task Subscriptions_CanUpdate()
    {
        // Arrange
        var subscription = new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            ServiceName = "Netflix",
            Cost = 15.99m,
            BillingCycle = BillingCycle.Monthly,
            NextBillingDate = DateTime.UtcNow.AddDays(30),
            Status = SubscriptionStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-6),
        };

        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        // Act
        subscription.Cost = 17.99m;
        subscription.Notes = "Price increased";
        await _context.SaveChangesAsync();

        var retrieved = await _context.Subscriptions.FindAsync(subscription.SubscriptionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Cost, Is.EqualTo(17.99m));
        Assert.That(retrieved.Notes, Is.EqualTo("Price increased"));
    }

    /// <summary>
    /// Tests that Subscriptions can be cancelled.
    /// </summary>
    [Test]
    public async Task Subscriptions_CanCancel()
    {
        // Arrange
        var subscription = new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            ServiceName = "Netflix",
            Cost = 15.99m,
            BillingCycle = BillingCycle.Monthly,
            NextBillingDate = DateTime.UtcNow.AddDays(30),
            Status = SubscriptionStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-6),
        };

        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        // Act
        subscription.Cancel();
        await _context.SaveChangesAsync();

        var retrieved = await _context.Subscriptions.FindAsync(subscription.SubscriptionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Status, Is.EqualTo(SubscriptionStatus.Cancelled));
        Assert.That(retrieved.CancellationDate, Is.Not.Null);
    }

    /// <summary>
    /// Tests that Subscriptions can be deleted.
    /// </summary>
    [Test]
    public async Task Subscriptions_CanDelete()
    {
        // Arrange
        var subscription = new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            ServiceName = "Netflix",
            Cost = 15.99m,
            BillingCycle = BillingCycle.Monthly,
            NextBillingDate = DateTime.UtcNow.AddDays(30),
            Status = SubscriptionStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-6),
        };

        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        // Act
        _context.Subscriptions.Remove(subscription);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Subscriptions.FindAsync(subscription.SubscriptionId);

        // Assert
        Assert.That(retrieved, Is.Null);
    }

    /// <summary>
    /// Tests that deleting a Category sets Subscription CategoryId to null.
    /// </summary>
    [Test]
    public async Task Categories_DeleteSetsCategoryIdToNull()
    {
        // Arrange
        var category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = "Entertainment",
        };

        var subscription = new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            ServiceName = "Netflix",
            Cost = 15.99m,
            BillingCycle = BillingCycle.Monthly,
            NextBillingDate = DateTime.UtcNow.AddDays(30),
            Status = SubscriptionStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-6),
            CategoryId = category.CategoryId,
        };

        _context.Categories.Add(category);
        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        // Act
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Subscriptions.FindAsync(subscription.SubscriptionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.CategoryId, Is.Null);
    }
}
