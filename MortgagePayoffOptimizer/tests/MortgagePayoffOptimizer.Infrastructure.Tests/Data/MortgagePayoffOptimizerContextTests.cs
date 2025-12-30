// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Infrastructure.Tests;

/// <summary>
/// Unit tests for the MortgagePayoffOptimizerContext.
/// </summary>
[TestFixture]
public class MortgagePayoffOptimizerContextTests
{
    private MortgagePayoffOptimizerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MortgagePayoffOptimizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MortgagePayoffOptimizerContext(options);
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
    /// Tests that Mortgages can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Mortgages_CanAddAndRetrieve()
    {
        // Arrange
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = "123 Test Street",
            Lender = "Test Bank",
            OriginalLoanAmount = 300000m,
            CurrentBalance = 275000m,
            InterestRate = 3.75m,
            LoanTermYears = 30,
            MonthlyPayment = 1389.35m,
            StartDate = DateTime.UtcNow,
            MortgageType = MortgageType.Conventional,
            IsActive = true,
        };

        // Act
        _context.Mortgages.Add(mortgage);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Mortgages.FindAsync(mortgage.MortgageId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.PropertyAddress, Is.EqualTo("123 Test Street"));
        Assert.That(retrieved.Lender, Is.EqualTo("Test Bank"));
        Assert.That(retrieved.CurrentBalance, Is.EqualTo(275000m));
    }

    /// <summary>
    /// Tests that Payments can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Payments_CanAddAndRetrieve()
    {
        // Arrange
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = "123 Test Street",
            Lender = "Test Bank",
            OriginalLoanAmount = 300000m,
            CurrentBalance = 275000m,
            InterestRate = 3.75m,
            LoanTermYears = 30,
            MonthlyPayment = 1389.35m,
            StartDate = DateTime.UtcNow,
            MortgageType = MortgageType.Conventional,
            IsActive = true,
        };

        var payment = new Payment
        {
            PaymentId = Guid.NewGuid(),
            MortgageId = mortgage.MortgageId,
            PaymentDate = DateTime.UtcNow,
            Amount = 1389.35m,
            PrincipalAmount = 527.10m,
            InterestAmount = 862.25m,
            ExtraPrincipal = 100m,
        };

        // Act
        _context.Mortgages.Add(mortgage);
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Payments.FindAsync(payment.PaymentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Amount, Is.EqualTo(1389.35m));
        Assert.That(retrieved.PrincipalAmount, Is.EqualTo(527.10m));
        Assert.That(retrieved.ExtraPrincipal, Is.EqualTo(100m));
    }

    /// <summary>
    /// Tests that RefinanceScenarios can be added and retrieved.
    /// </summary>
    [Test]
    public async Task RefinanceScenarios_CanAddAndRetrieve()
    {
        // Arrange
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = "123 Test Street",
            Lender = "Test Bank",
            OriginalLoanAmount = 300000m,
            CurrentBalance = 275000m,
            InterestRate = 3.75m,
            LoanTermYears = 30,
            MonthlyPayment = 1389.35m,
            StartDate = DateTime.UtcNow,
            MortgageType = MortgageType.Conventional,
            IsActive = true,
        };

        var scenario = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.NewGuid(),
            MortgageId = mortgage.MortgageId,
            Name = "Lower Rate Scenario",
            NewInterestRate = 3.25m,
            NewLoanTermYears = 30,
            RefinancingCosts = 5000m,
            NewMonthlyPayment = 1196.50m,
            MonthlySavings = 192.85m,
            BreakEvenMonths = 26,
            TotalSavings = 69426m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Mortgages.Add(mortgage);
        _context.RefinanceScenarios.Add(scenario);
        await _context.SaveChangesAsync();

        var retrieved = await _context.RefinanceScenarios.FindAsync(scenario.RefinanceScenarioId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Lower Rate Scenario"));
        Assert.That(retrieved.NewInterestRate, Is.EqualTo(3.25m));
        Assert.That(retrieved.MonthlySavings, Is.EqualTo(192.85m));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var mortgage = new Mortgage
        {
            MortgageId = Guid.NewGuid(),
            PropertyAddress = "123 Test Street",
            Lender = "Test Bank",
            OriginalLoanAmount = 300000m,
            CurrentBalance = 275000m,
            InterestRate = 3.75m,
            LoanTermYears = 30,
            MonthlyPayment = 1389.35m,
            StartDate = DateTime.UtcNow,
            MortgageType = MortgageType.Conventional,
            IsActive = true,
        };

        var payment = new Payment
        {
            PaymentId = Guid.NewGuid(),
            MortgageId = mortgage.MortgageId,
            PaymentDate = DateTime.UtcNow,
            Amount = 1389.35m,
            PrincipalAmount = 527.10m,
            InterestAmount = 862.25m,
        };

        _context.Mortgages.Add(mortgage);
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        // Act
        _context.Mortgages.Remove(mortgage);
        await _context.SaveChangesAsync();

        var retrievedPayment = await _context.Payments.FindAsync(payment.PaymentId);

        // Assert
        Assert.That(retrievedPayment, Is.Null);
    }
}
