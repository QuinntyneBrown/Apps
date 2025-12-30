// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core.Tests;

public class BillTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesBill()
    {
        // Arrange
        var billId = Guid.NewGuid();
        var payeeId = Guid.NewGuid();
        var name = "Monthly Electric Bill";
        var amount = 125.50m;
        var dueDate = DateTime.UtcNow.AddDays(15);
        var frequency = BillingFrequency.Monthly;
        var status = BillStatus.Pending;
        var notes = "Pay before due date";

        // Act
        var bill = new Bill
        {
            BillId = billId,
            PayeeId = payeeId,
            Name = name,
            Amount = amount,
            DueDate = dueDate,
            BillingFrequency = frequency,
            Status = status,
            IsAutoPay = true,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(bill.BillId, Is.EqualTo(billId));
            Assert.That(bill.PayeeId, Is.EqualTo(payeeId));
            Assert.That(bill.Name, Is.EqualTo(name));
            Assert.That(bill.Amount, Is.EqualTo(amount));
            Assert.That(bill.DueDate, Is.EqualTo(dueDate));
            Assert.That(bill.BillingFrequency, Is.EqualTo(frequency));
            Assert.That(bill.Status, Is.EqualTo(status));
            Assert.That(bill.IsAutoPay, Is.True);
            Assert.That(bill.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var bill = new Bill();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(bill.Name, Is.EqualTo(string.Empty));
            Assert.That(bill.IsAutoPay, Is.False);
            Assert.That(bill.Notes, Is.Null);
            Assert.That(bill.Payee, Is.Null);
        });
    }

    [Test]
    public void MarkAsPaid_WhenPending_StatusBecomesPaid()
    {
        // Arrange
        var bill = new Bill { Status = BillStatus.Pending };

        // Act
        bill.MarkAsPaid();

        // Assert
        Assert.That(bill.Status, Is.EqualTo(BillStatus.Paid));
    }

    [Test]
    public void MarkAsPaid_WhenOverdue_StatusBecomesPaid()
    {
        // Arrange
        var bill = new Bill { Status = BillStatus.Overdue };

        // Act
        bill.MarkAsPaid();

        // Assert
        Assert.That(bill.Status, Is.EqualTo(BillStatus.Paid));
    }

    [Test]
    public void MarkAsPaid_WhenAlreadyPaid_RemainsPaid()
    {
        // Arrange
        var bill = new Bill { Status = BillStatus.Paid };

        // Act
        bill.MarkAsPaid();

        // Assert
        Assert.That(bill.Status, Is.EqualTo(BillStatus.Paid));
    }

    [Test]
    public void CalculateNextDueDate_WithWeeklyFrequency_AddsSevenDays()
    {
        // Arrange
        var dueDate = new DateTime(2024, 1, 1);
        var bill = new Bill
        {
            DueDate = dueDate,
            BillingFrequency = BillingFrequency.Weekly
        };

        // Act
        var nextDueDate = bill.CalculateNextDueDate();

        // Assert
        Assert.That(nextDueDate, Is.EqualTo(new DateTime(2024, 1, 8)));
    }

    [Test]
    public void CalculateNextDueDate_WithMonthlyFrequency_AddsOneMonth()
    {
        // Arrange
        var dueDate = new DateTime(2024, 1, 15);
        var bill = new Bill
        {
            DueDate = dueDate,
            BillingFrequency = BillingFrequency.Monthly
        };

        // Act
        var nextDueDate = bill.CalculateNextDueDate();

        // Assert
        Assert.That(nextDueDate, Is.EqualTo(new DateTime(2024, 2, 15)));
    }

    [Test]
    public void CalculateNextDueDate_WithQuarterlyFrequency_AddsThreeMonths()
    {
        // Arrange
        var dueDate = new DateTime(2024, 1, 1);
        var bill = new Bill
        {
            DueDate = dueDate,
            BillingFrequency = BillingFrequency.Quarterly
        };

        // Act
        var nextDueDate = bill.CalculateNextDueDate();

        // Assert
        Assert.That(nextDueDate, Is.EqualTo(new DateTime(2024, 4, 1)));
    }

    [Test]
    public void CalculateNextDueDate_WithAnnualFrequency_AddsOneYear()
    {
        // Arrange
        var dueDate = new DateTime(2024, 1, 1);
        var bill = new Bill
        {
            DueDate = dueDate,
            BillingFrequency = BillingFrequency.Annual
        };

        // Act
        var nextDueDate = bill.CalculateNextDueDate();

        // Assert
        Assert.That(nextDueDate, Is.EqualTo(new DateTime(2025, 1, 1)));
    }

    [Test]
    public void CalculateNextDueDate_WithBiWeeklyFrequency_ReturnsSameDueDate()
    {
        // Arrange
        var dueDate = new DateTime(2024, 1, 1);
        var bill = new Bill
        {
            DueDate = dueDate,
            BillingFrequency = BillingFrequency.BiWeekly
        };

        // Act
        var nextDueDate = bill.CalculateNextDueDate();

        // Assert
        Assert.That(nextDueDate, Is.EqualTo(dueDate));
    }

    [Test]
    public void Amount_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var bill = new Bill();
        var amount = 99.99m;

        // Act
        bill.Amount = amount;

        // Assert
        Assert.That(bill.Amount, Is.EqualTo(amount));
    }

    [Test]
    public void IsAutoPay_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var bill = new Bill { IsAutoPay = false };

        // Act
        bill.IsAutoPay = true;

        // Assert
        Assert.That(bill.IsAutoPay, Is.True);
    }

    [Test]
    public void Status_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var bill = new Bill();

        // Act
        bill.Status = BillStatus.Overdue;

        // Assert
        Assert.That(bill.Status, Is.EqualTo(BillStatus.Overdue));
    }

    [Test]
    public void Payee_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var bill = new Bill();
        var payee = new Payee { PayeeId = Guid.NewGuid() };

        // Act
        bill.Payee = payee;

        // Assert
        Assert.That(bill.Payee, Is.EqualTo(payee));
    }

    [Test]
    public void Notes_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var bill = new Bill { Notes = "Some notes" };

        // Act
        bill.Notes = null;

        // Assert
        Assert.That(bill.Notes, Is.Null);
    }
}
