// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLoanComparisonTool.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PersonalLoanComparisonToolContext.
/// </summary>
[TestFixture]
public class PersonalLoanComparisonToolContextTests
{
    private PersonalLoanComparisonToolContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PersonalLoanComparisonToolContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PersonalLoanComparisonToolContext(options);
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
    /// Tests that Loans can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Loans_CanAddAndRetrieve()
    {
        // Arrange
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Personal,
            RequestedAmount = 10000m,
            Purpose = "Home improvement",
            CreditScore = 700,
            Notes = "Test notes",
        };

        // Act
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Loans.FindAsync(loan.LoanId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Loan"));
        Assert.That(retrieved.LoanType, Is.EqualTo(LoanType.Personal));
        Assert.That(retrieved.RequestedAmount, Is.EqualTo(10000m));
    }

    /// <summary>
    /// Tests that Offers can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Offers_CanAddAndRetrieve()
    {
        // Arrange
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Auto,
            RequestedAmount = 20000m,
            Purpose = "Vehicle purchase",
            CreditScore = 680,
        };

        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = loan.LoanId,
            LenderName = "Test Bank",
            LoanAmount = 20000m,
            InterestRate = 5.99m,
            TermMonths = 60,
            MonthlyPayment = 386.66m,
            TotalCost = 23199.60m,
            Fees = 0m,
            Notes = "Great rate",
        };

        // Act
        _context.Loans.Add(loan);
        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Offers.FindAsync(offer.OfferId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.LenderName, Is.EqualTo("Test Bank"));
        Assert.That(retrieved.InterestRate, Is.EqualTo(5.99m));
        Assert.That(retrieved.LoanId, Is.EqualTo(loan.LoanId));
    }

    /// <summary>
    /// Tests that PaymentSchedules can be added and retrieved.
    /// </summary>
    [Test]
    public async Task PaymentSchedules_CanAddAndRetrieve()
    {
        // Arrange
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Personal,
            RequestedAmount = 15000m,
            Purpose = "Debt consolidation",
            CreditScore = 650,
        };

        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = loan.LoanId,
            LenderName = "Credit Union",
            LoanAmount = 15000m,
            InterestRate = 6.50m,
            TermMonths = 48,
            MonthlyPayment = 355.00m,
            TotalCost = 17040m,
            Fees = 0m,
        };

        var paymentSchedule = new PaymentSchedule
        {
            PaymentScheduleId = Guid.NewGuid(),
            OfferId = offer.OfferId,
            PaymentNumber = 1,
            DueDate = DateTime.UtcNow.AddMonths(1),
            PaymentAmount = 355.00m,
            PrincipalAmount = 273.75m,
            InterestAmount = 81.25m,
            RemainingBalance = 14726.25m,
        };

        // Act
        _context.Loans.Add(loan);
        _context.Offers.Add(offer);
        _context.PaymentSchedules.Add(paymentSchedule);
        await _context.SaveChangesAsync();

        var retrieved = await _context.PaymentSchedules.FindAsync(paymentSchedule.PaymentScheduleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.PaymentNumber, Is.EqualTo(1));
        Assert.That(retrieved.PaymentAmount, Is.EqualTo(355.00m));
        Assert.That(retrieved.OfferId, Is.EqualTo(offer.OfferId));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var loan = new Loan
        {
            LoanId = Guid.NewGuid(),
            Name = "Test Loan",
            LoanType = LoanType.Personal,
            RequestedAmount = 10000m,
            Purpose = "Test",
            CreditScore = 700,
        };

        var offer = new Offer
        {
            OfferId = Guid.NewGuid(),
            LoanId = loan.LoanId,
            LenderName = "Test Bank",
            LoanAmount = 10000m,
            InterestRate = 7.00m,
            TermMonths = 36,
            MonthlyPayment = 308.77m,
            TotalCost = 11115.72m,
            Fees = 0m,
        };

        _context.Loans.Add(loan);
        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();

        // Act
        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();

        var retrievedOffer = await _context.Offers.FindAsync(offer.OfferId);

        // Assert
        Assert.That(retrievedOffer, Is.Null);
    }
}
