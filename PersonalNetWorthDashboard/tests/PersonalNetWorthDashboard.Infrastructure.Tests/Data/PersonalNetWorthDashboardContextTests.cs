// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PersonalNetWorthDashboardContext.
/// </summary>
[TestFixture]
public class PersonalNetWorthDashboardContextTests
{
    private PersonalNetWorthDashboardContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PersonalNetWorthDashboardContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PersonalNetWorthDashboardContext(options);
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
    /// Tests that Assets can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Assets_CanAddAndRetrieve()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 10000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true,
        };

        // Act
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Assets.FindAsync(asset.AssetId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Asset"));
        Assert.That(retrieved.AssetType, Is.EqualTo(AssetType.Cash));
        Assert.That(retrieved.CurrentValue, Is.EqualTo(10000m));
    }

    /// <summary>
    /// Tests that Liabilities can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Liabilities_CanAddAndRetrieve()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Test Liability",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m,
            InterestRate = 15.99m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true,
        };

        // Act
        _context.Liabilities.Add(liability);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Liabilities.FindAsync(liability.LiabilityId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Liability"));
        Assert.That(retrieved.LiabilityType, Is.EqualTo(LiabilityType.CreditCard));
        Assert.That(retrieved.CurrentBalance, Is.EqualTo(5000m));
    }

    /// <summary>
    /// Tests that NetWorthSnapshots can be added and retrieved.
    /// </summary>
    [Test]
    public async Task NetWorthSnapshots_CanAddAndRetrieve()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 100000m,
            TotalLiabilities = 50000m,
            NetWorth = 50000m,
            Notes = "Test snapshot",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.NetWorthSnapshots.Add(snapshot);
        await _context.SaveChangesAsync();

        var retrieved = await _context.NetWorthSnapshots.FindAsync(snapshot.NetWorthSnapshotId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TotalAssets, Is.EqualTo(100000m));
        Assert.That(retrieved.TotalLiabilities, Is.EqualTo(50000m));
        Assert.That(retrieved.NetWorth, Is.EqualTo(50000m));
    }

    /// <summary>
    /// Tests that multiple assets can be queried.
    /// </summary>
    [Test]
    public async Task Assets_CanQueryMultiple()
    {
        // Arrange
        var asset1 = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Asset 1",
            AssetType = AssetType.Cash,
            CurrentValue = 10000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true,
        };

        var asset2 = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Asset 2",
            AssetType = AssetType.Investment,
            CurrentValue = 20000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true,
        };

        // Act
        _context.Assets.AddRange(asset1, asset2);
        await _context.SaveChangesAsync();

        var allAssets = await _context.Assets.ToListAsync();

        // Assert
        Assert.That(allAssets, Has.Count.EqualTo(2));
        Assert.That(allAssets.Sum(a => a.CurrentValue), Is.EqualTo(30000m));
    }

    /// <summary>
    /// Tests that assets can be updated.
    /// </summary>
    [Test]
    public async Task Assets_CanUpdate()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 10000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true,
        };

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        // Act
        asset.UpdateValue(15000m);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Assets.FindAsync(asset.AssetId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.CurrentValue, Is.EqualTo(15000m));
    }
}
