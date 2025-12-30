// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core.Tests;

public class PaymentTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPayment()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        var billId = Guid.NewGuid();
        var amount = 125.50m;
        var paymentDate = DateTime.UtcNow;
        var confirmationNumber = "CONF-123456";
        var paymentMethod = "Credit Card";
        var notes = "Paid online";

        // Act
        var payment = new Payment
        {
            PaymentId = paymentId,
            BillId = billId,
            Amount = amount,
            PaymentDate = paymentDate,
            ConfirmationNumber = confirmationNumber,
            PaymentMethod = paymentMethod,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(payment.PaymentId, Is.EqualTo(paymentId));
            Assert.That(payment.BillId, Is.EqualTo(billId));
            Assert.That(payment.Amount, Is.EqualTo(amount));
            Assert.That(payment.PaymentDate, Is.EqualTo(paymentDate));
            Assert.That(payment.ConfirmationNumber, Is.EqualTo(confirmationNumber));
            Assert.That(payment.PaymentMethod, Is.EqualTo(paymentMethod));
            Assert.That(payment.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var payment = new Payment();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(payment.ConfirmationNumber, Is.Null);
            Assert.That(payment.PaymentMethod, Is.Null);
            Assert.That(payment.Notes, Is.Null);
            Assert.That(payment.Bill, Is.Null);
        });
    }

    [Test]
    public void Amount_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment();
        var amount = 75.25m;

        // Act
        payment.Amount = amount;

        // Assert
        Assert.That(payment.Amount, Is.EqualTo(amount));
    }

    [Test]
    public void PaymentDate_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment();
        var paymentDate = DateTime.UtcNow.AddDays(-7);

        // Act
        payment.PaymentDate = paymentDate;

        // Assert
        Assert.That(payment.PaymentDate, Is.EqualTo(paymentDate));
    }

    [Test]
    public void ConfirmationNumber_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment();
        var confirmationNumber = "CONF-789012";

        // Act
        payment.ConfirmationNumber = confirmationNumber;

        // Assert
        Assert.That(payment.ConfirmationNumber, Is.EqualTo(confirmationNumber));
    }

    [Test]
    public void PaymentMethod_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment();
        var paymentMethod = "Bank Transfer";

        // Act
        payment.PaymentMethod = paymentMethod;

        // Assert
        Assert.That(payment.PaymentMethod, Is.EqualTo(paymentMethod));
    }

    [Test]
    public void Notes_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment();
        var notes = "Autopay processed";

        // Act
        payment.Notes = notes;

        // Assert
        Assert.That(payment.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Bill_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment();
        var bill = new Bill { BillId = Guid.NewGuid() };

        // Act
        payment.Bill = bill;

        // Assert
        Assert.That(payment.Bill, Is.EqualTo(bill));
    }

    [Test]
    public void BillId_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment();
        var billId = Guid.NewGuid();

        // Act
        payment.BillId = billId;

        // Assert
        Assert.That(payment.BillId, Is.EqualTo(billId));
    }

    [Test]
    public void ConfirmationNumber_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment { ConfirmationNumber = "CONF-123" };

        // Act
        payment.ConfirmationNumber = null;

        // Assert
        Assert.That(payment.ConfirmationNumber, Is.Null);
    }

    [Test]
    public void PaymentMethod_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment { PaymentMethod = "Cash" };

        // Act
        payment.PaymentMethod = null;

        // Assert
        Assert.That(payment.PaymentMethod, Is.Null);
    }

    [Test]
    public void Notes_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var payment = new Payment { Notes = "Some notes" };

        // Act
        payment.Notes = null;

        // Assert
        Assert.That(payment.Notes, Is.Null);
    }
}
