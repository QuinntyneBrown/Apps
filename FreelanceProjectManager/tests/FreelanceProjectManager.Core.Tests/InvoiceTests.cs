// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;

namespace FreelanceProjectManager.Core.Tests;

public class InvoiceTests
{
    [Test]
    public void Invoice_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var invoiceId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var clientId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var invoiceNumber = "INV-2024-001";
        var invoiceDate = new DateTime(2024, 6, 1);
        var dueDate = new DateTime(2024, 6, 30);

        // Act
        var invoice = new Invoice
        {
            InvoiceId = invoiceId,
            UserId = userId,
            ClientId = clientId,
            ProjectId = projectId,
            InvoiceNumber = invoiceNumber,
            InvoiceDate = invoiceDate,
            DueDate = dueDate,
            TotalAmount = 5000.00m,
            Currency = "USD",
            Status = "Draft",
            Notes = "First invoice"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(invoice.InvoiceId, Is.EqualTo(invoiceId));
            Assert.That(invoice.UserId, Is.EqualTo(userId));
            Assert.That(invoice.ClientId, Is.EqualTo(clientId));
            Assert.That(invoice.ProjectId, Is.EqualTo(projectId));
            Assert.That(invoice.InvoiceNumber, Is.EqualTo(invoiceNumber));
            Assert.That(invoice.InvoiceDate, Is.EqualTo(invoiceDate));
            Assert.That(invoice.DueDate, Is.EqualTo(dueDate));
            Assert.That(invoice.TotalAmount, Is.EqualTo(5000.00m));
            Assert.That(invoice.Currency, Is.EqualTo("USD"));
            Assert.That(invoice.Status, Is.EqualTo("Draft"));
            Assert.That(invoice.Notes, Is.EqualTo("First invoice"));
            Assert.That(invoice.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Invoice_DefaultValues_AreSetCorrectly()
    {
        // Act
        var invoice = new Invoice();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(invoice.InvoiceNumber, Is.EqualTo(string.Empty));
            Assert.That(invoice.Currency, Is.EqualTo("USD"));
            Assert.That(invoice.Status, Is.EqualTo("Draft"));
            Assert.That(invoice.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsPaid_SetsStatusToPaidAndUpdatesTimestamps()
    {
        // Arrange
        var invoice = new Invoice
        {
            Status = "Sent",
            PaidDate = null
        };
        var beforeCall = DateTime.UtcNow;

        // Act
        invoice.MarkAsPaid();
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(invoice.Status, Is.EqualTo("Paid"));
            Assert.That(invoice.PaidDate, Is.Not.Null);
            Assert.That(invoice.PaidDate!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
            Assert.That(invoice.UpdatedAt, Is.Not.Null);
            Assert.That(invoice.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void Send_SetsStatusToSentAndUpdatesTimestamp()
    {
        // Arrange
        var invoice = new Invoice { Status = "Draft" };
        var beforeCall = DateTime.UtcNow;

        // Act
        invoice.Send();
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(invoice.Status, Is.EqualTo("Sent"));
            Assert.That(invoice.UpdatedAt, Is.Not.Null);
            Assert.That(invoice.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void IsOverdue_WhenSentAndPastDueDate_ReturnsTrue()
    {
        // Arrange
        var invoice = new Invoice
        {
            Status = "Sent",
            DueDate = DateTime.UtcNow.AddDays(-5),
            PaidDate = null
        };

        // Act
        var isOverdue = invoice.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.True);
    }

    [Test]
    public void IsOverdue_WhenSentButNotPastDueDate_ReturnsFalse()
    {
        // Arrange
        var invoice = new Invoice
        {
            Status = "Sent",
            DueDate = DateTime.UtcNow.AddDays(5),
            PaidDate = null
        };

        // Act
        var isOverdue = invoice.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_WhenPaid_ReturnsFalse()
    {
        // Arrange
        var invoice = new Invoice
        {
            Status = "Sent",
            DueDate = DateTime.UtcNow.AddDays(-5),
            PaidDate = DateTime.UtcNow
        };

        // Act
        var isOverdue = invoice.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_WhenStatusIsDraft_ReturnsFalse()
    {
        // Arrange
        var invoice = new Invoice
        {
            Status = "Draft",
            DueDate = DateTime.UtcNow.AddDays(-5),
            PaidDate = null
        };

        // Act
        var isOverdue = invoice.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void Invoice_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var invoice = new Invoice
        {
            ProjectId = null,
            PaidDate = null,
            Notes = null,
            UpdatedAt = null,
            Client = null,
            Project = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(invoice.ProjectId, Is.Null);
            Assert.That(invoice.PaidDate, Is.Null);
            Assert.That(invoice.Notes, Is.Null);
            Assert.That(invoice.UpdatedAt, Is.Null);
            Assert.That(invoice.Client, Is.Null);
            Assert.That(invoice.Project, Is.Null);
        });
    }

    [Test]
    public void Invoice_TotalAmount_CanBeZero()
    {
        // Arrange & Act
        var invoice = new Invoice { TotalAmount = 0m };

        // Assert
        Assert.That(invoice.TotalAmount, Is.EqualTo(0m));
    }

    [Test]
    public void Invoice_TotalAmount_CanBeLargeValue()
    {
        // Arrange & Act
        var invoice = new Invoice { TotalAmount = 100000.50m };

        // Assert
        Assert.That(invoice.TotalAmount, Is.EqualTo(100000.50m));
    }

    [Test]
    public void Invoice_Currency_CanBeSetToVariousValues()
    {
        // Arrange
        var currencies = new[] { "USD", "EUR", "GBP", "CAD", "AUD" };

        // Act & Assert
        foreach (var currency in currencies)
        {
            var invoice = new Invoice { Currency = currency };
            Assert.That(invoice.Currency, Is.EqualTo(currency));
        }
    }

    [Test]
    public void Invoice_Status_CanBeSetToVariousValues()
    {
        // Arrange
        var statuses = new[] { "Draft", "Sent", "Paid", "Overdue", "Cancelled" };

        // Act & Assert
        foreach (var status in statuses)
        {
            var invoice = new Invoice { Status = status };
            Assert.That(invoice.Status, Is.EqualTo(status));
        }
    }
}
