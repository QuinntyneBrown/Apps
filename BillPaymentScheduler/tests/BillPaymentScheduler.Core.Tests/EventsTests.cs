// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BillPaymentScheduler.Core.Tests;

public class EventsTests
{
    [Test]
    public void PayeeAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var payeeId = Guid.NewGuid();
        var name = "Electric Company";

        // Act
        var evt = new PayeeAddedEvent
        {
            PayeeId = payeeId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PayeeId, Is.EqualTo(payeeId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void PayeeAddedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var payeeId = Guid.NewGuid();
        var name = "Water Utility";
        var timestamp = DateTime.UtcNow.AddMinutes(-5);

        // Act
        var evt = new PayeeAddedEvent
        {
            PayeeId = payeeId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void PayeeAddedEvent_EmptyName_CreatesEvent()
    {
        // Arrange
        var payeeId = Guid.NewGuid();

        // Act
        var evt = new PayeeAddedEvent
        {
            PayeeId = payeeId,
            Name = string.Empty
        };

        // Assert
        Assert.That(evt.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void BillCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var billId = Guid.NewGuid();
        var name = "Monthly Electric Bill";
        var amount = 125.50m;
        var dueDate = DateTime.UtcNow.AddDays(15);

        // Act
        var evt = new BillCreatedEvent
        {
            BillId = billId,
            Name = name,
            Amount = amount,
            DueDate = dueDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BillId, Is.EqualTo(billId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Amount, Is.EqualTo(amount));
            Assert.That(evt.DueDate, Is.EqualTo(dueDate));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void BillCreatedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var billId = Guid.NewGuid();
        var name = "Water Bill";
        var amount = 45.00m;
        var dueDate = DateTime.UtcNow.AddDays(10);
        var timestamp = DateTime.UtcNow.AddMinutes(-10);

        // Act
        var evt = new BillCreatedEvent
        {
            BillId = billId,
            Name = name,
            Amount = amount,
            DueDate = dueDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void BillCreatedEvent_ZeroAmount_CreatesEvent()
    {
        // Arrange
        var billId = Guid.NewGuid();
        var name = "Free Service";
        var amount = 0m;
        var dueDate = DateTime.UtcNow;

        // Act
        var evt = new BillCreatedEvent
        {
            BillId = billId,
            Name = name,
            Amount = amount,
            DueDate = dueDate
        };

        // Assert
        Assert.That(evt.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void PaymentRecordedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        var billId = Guid.NewGuid();
        var amount = 125.50m;
        var paymentDate = DateTime.UtcNow;

        // Act
        var evt = new PaymentRecordedEvent
        {
            PaymentId = paymentId,
            BillId = billId,
            Amount = amount,
            PaymentDate = paymentDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PaymentId, Is.EqualTo(paymentId));
            Assert.That(evt.BillId, Is.EqualTo(billId));
            Assert.That(evt.Amount, Is.EqualTo(amount));
            Assert.That(evt.PaymentDate, Is.EqualTo(paymentDate));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void PaymentRecordedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        var billId = Guid.NewGuid();
        var amount = 99.99m;
        var paymentDate = DateTime.UtcNow.AddDays(-1);
        var timestamp = DateTime.UtcNow.AddMinutes(-3);

        // Act
        var evt = new PaymentRecordedEvent
        {
            PaymentId = paymentId,
            BillId = billId,
            Amount = amount,
            PaymentDate = paymentDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void PayeeAddedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new PayeeAddedEvent
        {
            PayeeId = Guid.NewGuid(),
            Name = "Old Name"
        };
        var newName = "New Name";

        // Act
        var newEvt = evt with { Name = newName };

        // Assert
        Assert.That(newEvt.Name, Is.EqualTo(newName));
        Assert.That(newEvt.PayeeId, Is.EqualTo(evt.PayeeId));
    }

    [Test]
    public void BillCreatedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new BillCreatedEvent
        {
            BillId = Guid.NewGuid(),
            Name = "Bill Name",
            Amount = 100m,
            DueDate = DateTime.UtcNow
        };
        var newAmount = 150m;

        // Act
        var newEvt = evt with { Amount = newAmount };

        // Assert
        Assert.That(newEvt.Amount, Is.EqualTo(newAmount));
        Assert.That(newEvt.BillId, Is.EqualTo(evt.BillId));
    }

    [Test]
    public void PaymentRecordedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new PaymentRecordedEvent
        {
            PaymentId = Guid.NewGuid(),
            BillId = Guid.NewGuid(),
            Amount = 100m,
            PaymentDate = DateTime.UtcNow
        };
        var newPaymentDate = DateTime.UtcNow.AddDays(-5);

        // Act
        var newEvt = evt with { PaymentDate = newPaymentDate };

        // Assert
        Assert.That(newEvt.PaymentDate, Is.EqualTo(newPaymentDate));
        Assert.That(newEvt.PaymentId, Is.EqualTo(evt.PaymentId));
    }
}
