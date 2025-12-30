// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SideHustleIncomeTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the SideHustleIncomeTrackerContext.
/// </summary>
[TestFixture]
public class SideHustleIncomeTrackerContextTests
{
    private SideHustleIncomeTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SideHustleIncomeTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SideHustleIncomeTrackerContext(options);
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
    /// Tests that Businesses can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Businesses_CanAddAndRetrieve()
    {
        // Arrange
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Test Business",
            Description = "Test description",
            StartDate = DateTime.UtcNow,
            IsActive = true,
            TotalIncome = 10000m,
            TotalExpenses = 2000m,
        };

        // Act
        _context.Businesses.Add(business);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Businesses.FindAsync(business.BusinessId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Business"));
        Assert.That(retrieved.IsActive, Is.True);
    }

    /// <summary>
    /// Tests that Incomes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Incomes_CanAddAndRetrieve()
    {
        // Arrange
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Test Business",
            StartDate = DateTime.UtcNow,
            IsActive = true,
        };

        var income = new Income
        {
            IncomeId = Guid.NewGuid(),
            BusinessId = business.BusinessId,
            Description = "Test Income",
            Amount = 500m,
            IncomeDate = DateTime.UtcNow,
            IsPaid = true,
        };

        // Act
        _context.Businesses.Add(business);
        _context.Incomes.Add(income);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Incomes.FindAsync(income.IncomeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Test Income"));
        Assert.That(retrieved.Amount, Is.EqualTo(500m));
        Assert.That(retrieved.IsPaid, Is.True);
    }

    /// <summary>
    /// Tests that Expenses can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Expenses_CanAddAndRetrieve()
    {
        // Arrange
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Test Business",
            StartDate = DateTime.UtcNow,
            IsActive = true,
        };

        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            BusinessId = business.BusinessId,
            Description = "Test Expense",
            Amount = 100m,
            ExpenseDate = DateTime.UtcNow,
            Category = "Software",
            IsTaxDeductible = true,
        };

        // Act
        _context.Businesses.Add(business);
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Expenses.FindAsync(expense.ExpenseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Test Expense"));
        Assert.That(retrieved.Amount, Is.EqualTo(100m));
        Assert.That(retrieved.IsTaxDeductible, Is.True);
    }

    /// <summary>
    /// Tests that TaxEstimates can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TaxEstimates_CanAddAndRetrieve()
    {
        // Arrange
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Test Business",
            StartDate = DateTime.UtcNow,
            IsActive = true,
        };

        var taxEstimate = new TaxEstimate
        {
            TaxEstimateId = Guid.NewGuid(),
            BusinessId = business.BusinessId,
            TaxYear = 2024,
            Quarter = 1,
            NetProfit = 5000m,
            SelfEmploymentTax = 750m,
            IncomeTax = 1000m,
            TotalEstimatedTax = 1750m,
            IsPaid = false,
        };

        // Act
        _context.Businesses.Add(business);
        _context.TaxEstimates.Add(taxEstimate);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TaxEstimates.FindAsync(taxEstimate.TaxEstimateId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TaxYear, Is.EqualTo(2024));
        Assert.That(retrieved.Quarter, Is.EqualTo(1));
        Assert.That(retrieved.TotalEstimatedTax, Is.EqualTo(1750m));
    }

    /// <summary>
    /// Tests that businesses can be updated.
    /// </summary>
    [Test]
    public async Task Businesses_CanUpdate()
    {
        // Arrange
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Test Business",
            StartDate = DateTime.UtcNow,
            IsActive = true,
        };

        _context.Businesses.Add(business);
        await _context.SaveChangesAsync();

        // Act
        business.Name = "Updated Business";
        business.TotalIncome = 20000m;
        await _context.SaveChangesAsync();

        var retrieved = await _context.Businesses.FindAsync(business.BusinessId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Updated Business"));
        Assert.That(retrieved.TotalIncome, Is.EqualTo(20000m));
    }

    /// <summary>
    /// Tests that businesses can be deleted.
    /// </summary>
    [Test]
    public async Task Businesses_CanDelete()
    {
        // Arrange
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "Test Business",
            StartDate = DateTime.UtcNow,
            IsActive = true,
        };

        _context.Businesses.Add(business);
        await _context.SaveChangesAsync();

        // Act
        _context.Businesses.Remove(business);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Businesses.FindAsync(business.BusinessId);

        // Assert
        Assert.That(retrieved, Is.Null);
    }
}
