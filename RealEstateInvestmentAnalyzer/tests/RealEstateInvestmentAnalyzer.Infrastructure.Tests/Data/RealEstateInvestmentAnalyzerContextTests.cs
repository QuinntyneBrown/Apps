// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Infrastructure.Tests;

/// <summary>
/// Unit tests for the RealEstateInvestmentAnalyzerContext.
/// </summary>
[TestFixture]
public class RealEstateInvestmentAnalyzerContextTests
{
    private RealEstateInvestmentAnalyzerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RealEstateInvestmentAnalyzerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new RealEstateInvestmentAnalyzerContext(options);
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
    /// Tests that Properties can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Properties_CanAddAndRetrieve()
    {
        // Arrange
        var property = new Property
        {
            PropertyId = Guid.NewGuid(),
            Address = "123 Test Street",
            PropertyType = PropertyType.SingleFamily,
            PurchasePrice = 250000.00m,
            PurchaseDate = DateTime.UtcNow,
            CurrentValue = 300000.00m,
            SquareFeet = 2000,
            Bedrooms = 3,
            Bathrooms = 2,
            Notes = "Test property",
        };

        // Act
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Properties.FindAsync(property.PropertyId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Address, Is.EqualTo("123 Test Street"));
        Assert.That(retrieved.PropertyType, Is.EqualTo(PropertyType.SingleFamily));
        Assert.That(retrieved.PurchasePrice, Is.EqualTo(250000.00m));
    }

    /// <summary>
    /// Tests that CashFlows can be added and retrieved.
    /// </summary>
    [Test]
    public async Task CashFlows_CanAddAndRetrieve()
    {
        // Arrange
        var property = new Property
        {
            PropertyId = Guid.NewGuid(),
            Address = "123 Test Street",
            PropertyType = PropertyType.SingleFamily,
            PurchasePrice = 250000.00m,
            PurchaseDate = DateTime.UtcNow,
            CurrentValue = 300000.00m,
            SquareFeet = 2000,
            Bedrooms = 3,
            Bathrooms = 2,
        };

        var cashFlow = new CashFlow
        {
            CashFlowId = Guid.NewGuid(),
            PropertyId = property.PropertyId,
            Date = DateTime.UtcNow,
            Income = 2000.00m,
            Expenses = 500.00m,
            NetCashFlow = 1500.00m,
            Notes = "Monthly cash flow",
        };

        // Act
        _context.Properties.Add(property);
        _context.CashFlows.Add(cashFlow);
        await _context.SaveChangesAsync();

        var retrieved = await _context.CashFlows.FindAsync(cashFlow.CashFlowId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Income, Is.EqualTo(2000.00m));
        Assert.That(retrieved.Expenses, Is.EqualTo(500.00m));
        Assert.That(retrieved.NetCashFlow, Is.EqualTo(1500.00m));
    }

    /// <summary>
    /// Tests that Expenses can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Expenses_CanAddAndRetrieve()
    {
        // Arrange
        var property = new Property
        {
            PropertyId = Guid.NewGuid(),
            Address = "123 Test Street",
            PropertyType = PropertyType.SingleFamily,
            PurchasePrice = 250000.00m,
            PurchaseDate = DateTime.UtcNow,
            CurrentValue = 300000.00m,
            SquareFeet = 2000,
            Bedrooms = 3,
            Bathrooms = 2,
        };

        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            PropertyId = property.PropertyId,
            Description = "Property Tax",
            Amount = 3000.00m,
            Date = DateTime.UtcNow,
            Category = "Tax",
            IsRecurring = true,
            Notes = "Annual property tax",
        };

        // Act
        _context.Properties.Add(property);
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Expenses.FindAsync(expense.ExpenseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Property Tax"));
        Assert.That(retrieved.Amount, Is.EqualTo(3000.00m));
        Assert.That(retrieved.Category, Is.EqualTo("Tax"));
        Assert.That(retrieved.IsRecurring, Is.True);
    }

    /// <summary>
    /// Tests that Leases can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Leases_CanAddAndRetrieve()
    {
        // Arrange
        var property = new Property
        {
            PropertyId = Guid.NewGuid(),
            Address = "123 Test Street",
            PropertyType = PropertyType.SingleFamily,
            PurchasePrice = 250000.00m,
            PurchaseDate = DateTime.UtcNow,
            CurrentValue = 300000.00m,
            SquareFeet = 2000,
            Bedrooms = 3,
            Bathrooms = 2,
        };

        var lease = new Lease
        {
            LeaseId = Guid.NewGuid(),
            PropertyId = property.PropertyId,
            TenantName = "John Doe",
            MonthlyRent = 1800.00m,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1),
            SecurityDeposit = 3600.00m,
            IsActive = true,
            Notes = "Great tenant",
        };

        // Act
        _context.Properties.Add(property);
        _context.Leases.Add(lease);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Leases.FindAsync(lease.LeaseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TenantName, Is.EqualTo("John Doe"));
        Assert.That(retrieved.MonthlyRent, Is.EqualTo(1800.00m));
        Assert.That(retrieved.SecurityDeposit, Is.EqualTo(3600.00m));
        Assert.That(retrieved.IsActive, Is.True);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities when Property is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var property = new Property
        {
            PropertyId = Guid.NewGuid(),
            Address = "123 Test Street",
            PropertyType = PropertyType.SingleFamily,
            PurchasePrice = 250000.00m,
            PurchaseDate = DateTime.UtcNow,
            CurrentValue = 300000.00m,
            SquareFeet = 2000,
            Bedrooms = 3,
            Bathrooms = 2,
        };

        var lease = new Lease
        {
            LeaseId = Guid.NewGuid(),
            PropertyId = property.PropertyId,
            TenantName = "John Doe",
            MonthlyRent = 1800.00m,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1),
            SecurityDeposit = 3600.00m,
            IsActive = true,
        };

        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            PropertyId = property.PropertyId,
            Description = "Test Expense",
            Amount = 500.00m,
            Date = DateTime.UtcNow,
            Category = "Maintenance",
            IsRecurring = false,
        };

        _context.Properties.Add(property);
        _context.Leases.Add(lease);
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        // Act
        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();

        var retrievedLease = await _context.Leases.FindAsync(lease.LeaseId);
        var retrievedExpense = await _context.Expenses.FindAsync(expense.ExpenseId);

        // Assert
        Assert.That(retrievedLease, Is.Null);
        Assert.That(retrievedExpense, Is.Null);
    }
}
