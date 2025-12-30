// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Infrastructure.Tests;

/// <summary>
/// Unit tests for the BillPaymentSchedulerContext.
/// </summary>
[TestFixture]
public class BillPaymentSchedulerContextTests
{
    private BillPaymentSchedulerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BillPaymentSchedulerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BillPaymentSchedulerContext(options);
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
    /// Tests that Payees can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Payees_CanAddAndRetrieve()
    {
        // Arrange
        var payee = new Payee
        {
            PayeeId = Guid.NewGuid(),
            Name = "Electric Company",
            AccountNumber = "123456",
            Website = "https://electric.example.com",
            PhoneNumber = "(555) 123-4567",
        };

        // Act
        _context.Payees.Add(payee);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Payees.FindAsync(payee.PayeeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Electric Company"));
        Assert.That(retrieved.AccountNumber, Is.EqualTo("123456"));
    }

    /// <summary>
    /// Tests that Bills can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Bills_CanAddAndRetrieve()
    {
        // Arrange
        var payee = new Payee
        {
            PayeeId = Guid.NewGuid(),
            Name = "Test Payee",
        };

        var bill = new Bill
        {
            BillId = Guid.NewGuid(),
            PayeeId = payee.PayeeId,
            Name = "Monthly Service",
            Amount = 99.99m,
            DueDate = DateTime.UtcNow.AddDays(15),
            BillingFrequency = BillingFrequency.Monthly,
            Status = BillStatus.Pending,
            IsAutoPay = true,
        };

        // Act
        _context.Payees.Add(payee);
        _context.Bills.Add(bill);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Bills.FindAsync(bill.BillId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Monthly Service"));
        Assert.That(retrieved.Amount, Is.EqualTo(99.99m));
        Assert.That(retrieved.IsAutoPay, Is.True);
    }

    /// <summary>
    /// Tests that Payments can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Payments_CanAddAndRetrieve()
    {
        // Arrange
        var payee = new Payee
        {
            PayeeId = Guid.NewGuid(),
            Name = "Test Payee",
        };

        var bill = new Bill
        {
            BillId = Guid.NewGuid(),
            PayeeId = payee.PayeeId,
            Name = "Service Bill",
            Amount = 50.00m,
            DueDate = DateTime.UtcNow,
            BillingFrequency = BillingFrequency.Monthly,
            Status = BillStatus.Paid,
        };

        var payment = new Payment
        {
            PaymentId = Guid.NewGuid(),
            BillId = bill.BillId,
            Amount = 50.00m,
            PaymentDate = DateTime.UtcNow,
            ConfirmationNumber = "CONF123",
            PaymentMethod = "Credit Card",
        };

        // Act
        _context.Payees.Add(payee);
        _context.Bills.Add(bill);
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Payments.FindAsync(payment.PaymentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Amount, Is.EqualTo(50.00m));
        Assert.That(retrieved.ConfirmationNumber, Is.EqualTo("CONF123"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedBills()
    {
        // Arrange
        var payee = new Payee
        {
            PayeeId = Guid.NewGuid(),
            Name = "Test Payee",
        };

        var bill = new Bill
        {
            BillId = Guid.NewGuid(),
            PayeeId = payee.PayeeId,
            Name = "Service",
            Amount = 100.00m,
            DueDate = DateTime.UtcNow,
            BillingFrequency = BillingFrequency.Monthly,
            Status = BillStatus.Pending,
        };

        _context.Payees.Add(payee);
        _context.Bills.Add(bill);
        await _context.SaveChangesAsync();

        // Act
        _context.Payees.Remove(payee);
        await _context.SaveChangesAsync();

        var retrievedBill = await _context.Bills.FindAsync(bill.BillId);

        // Assert
        Assert.That(retrievedBill, Is.Null);
    }
}
