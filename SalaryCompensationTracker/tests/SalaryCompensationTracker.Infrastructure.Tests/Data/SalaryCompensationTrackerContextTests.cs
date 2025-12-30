// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SalaryCompensationTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the SalaryCompensationTrackerContext.
/// </summary>
[TestFixture]
public class SalaryCompensationTrackerContextTests
{
    private SalaryCompensationTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SalaryCompensationTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SalaryCompensationTrackerContext(options);
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
    /// Tests that Compensations can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Compensations_CanAddAndRetrieve()
    {
        // Arrange
        var compensation = new Compensation
        {
            CompensationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompensationType = CompensationType.FullTime,
            Employer = "Test Company",
            JobTitle = "Software Developer",
            BaseSalary = 100000m,
            Currency = "USD",
            TotalCompensation = 100000m,
            EffectiveDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Compensations.Add(compensation);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Compensations.FindAsync(compensation.CompensationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Employer, Is.EqualTo("Test Company"));
        Assert.That(retrieved.JobTitle, Is.EqualTo("Software Developer"));
        Assert.That(retrieved.BaseSalary, Is.EqualTo(100000m));
    }

    /// <summary>
    /// Tests that Benefits can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Benefits_CanAddAndRetrieve()
    {
        // Arrange
        var compensation = new Compensation
        {
            CompensationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompensationType = CompensationType.FullTime,
            Employer = "Test Company",
            JobTitle = "Software Developer",
            BaseSalary = 100000m,
            Currency = "USD",
            TotalCompensation = 100000m,
            EffectiveDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var benefit = new Benefit
        {
            BenefitId = Guid.NewGuid(),
            CompensationId = compensation.CompensationId,
            UserId = compensation.UserId,
            Name = "Health Insurance",
            Category = "Health",
            EstimatedValue = 10000m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Compensations.Add(compensation);
        _context.Benefits.Add(benefit);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Benefits.FindAsync(benefit.BenefitId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Health Insurance"));
        Assert.That(retrieved.Category, Is.EqualTo("Health"));
        Assert.That(retrieved.EstimatedValue, Is.EqualTo(10000m));
    }

    /// <summary>
    /// Tests that MarketComparisons can be added and retrieved.
    /// </summary>
    [Test]
    public async Task MarketComparisons_CanAddAndRetrieve()
    {
        // Arrange
        var marketComparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            JobTitle = "Software Developer",
            Location = "San Francisco",
            ExperienceLevel = "Mid-level",
            MinSalary = 90000m,
            MaxSalary = 130000m,
            MedianSalary = 110000m,
            DataSource = "Glassdoor",
            ComparisonDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MarketComparisons.Add(marketComparison);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MarketComparisons.FindAsync(marketComparison.MarketComparisonId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.JobTitle, Is.EqualTo("Software Developer"));
        Assert.That(retrieved.Location, Is.EqualTo("San Francisco"));
        Assert.That(retrieved.MedianSalary, Is.EqualTo(110000m));
    }

    /// <summary>
    /// Tests that compensation records can be updated.
    /// </summary>
    [Test]
    public async Task Compensations_CanUpdate()
    {
        // Arrange
        var compensation = new Compensation
        {
            CompensationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompensationType = CompensationType.FullTime,
            Employer = "Test Company",
            JobTitle = "Software Developer",
            BaseSalary = 100000m,
            Currency = "USD",
            TotalCompensation = 100000m,
            EffectiveDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Compensations.Add(compensation);
        await _context.SaveChangesAsync();

        // Act
        compensation.BaseSalary = 120000m;
        compensation.TotalCompensation = 120000m;
        compensation.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var retrieved = await _context.Compensations.FindAsync(compensation.CompensationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.BaseSalary, Is.EqualTo(120000m));
        Assert.That(retrieved.UpdatedAt, Is.Not.Null);
    }

    /// <summary>
    /// Tests that compensation records can be deleted.
    /// </summary>
    [Test]
    public async Task Compensations_CanDelete()
    {
        // Arrange
        var compensation = new Compensation
        {
            CompensationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompensationType = CompensationType.FullTime,
            Employer = "Test Company",
            JobTitle = "Software Developer",
            BaseSalary = 100000m,
            Currency = "USD",
            TotalCompensation = 100000m,
            EffectiveDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Compensations.Add(compensation);
        await _context.SaveChangesAsync();

        // Act
        _context.Compensations.Remove(compensation);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Compensations.FindAsync(compensation.CompensationId);

        // Assert
        Assert.That(retrieved, Is.Null);
    }
}
