// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;

namespace FreelanceProjectManager.Core.Tests;

public class TimeEntryTests
{
    [Test]
    public void TimeEntry_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var timeEntryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var workDate = new DateTime(2024, 6, 15);
        var hours = 8.5m;
        var description = "Implemented user authentication";

        // Act
        var timeEntry = new TimeEntry
        {
            TimeEntryId = timeEntryId,
            UserId = userId,
            ProjectId = projectId,
            WorkDate = workDate,
            Hours = hours,
            Description = description,
            IsBillable = true,
            IsInvoiced = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(timeEntry.TimeEntryId, Is.EqualTo(timeEntryId));
            Assert.That(timeEntry.UserId, Is.EqualTo(userId));
            Assert.That(timeEntry.ProjectId, Is.EqualTo(projectId));
            Assert.That(timeEntry.WorkDate, Is.EqualTo(workDate));
            Assert.That(timeEntry.Hours, Is.EqualTo(hours));
            Assert.That(timeEntry.Description, Is.EqualTo(description));
            Assert.That(timeEntry.IsBillable, Is.True);
            Assert.That(timeEntry.IsInvoiced, Is.False);
            Assert.That(timeEntry.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TimeEntry_DefaultValues_AreSetCorrectly()
    {
        // Act
        var timeEntry = new TimeEntry();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(timeEntry.Description, Is.EqualTo(string.Empty));
            Assert.That(timeEntry.IsBillable, Is.True);
            Assert.That(timeEntry.IsInvoiced, Is.False);
            Assert.That(timeEntry.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsInvoiced_SetsIsInvoicedAndInvoiceId()
    {
        // Arrange
        var invoiceId = Guid.NewGuid();
        var timeEntry = new TimeEntry
        {
            IsInvoiced = false,
            InvoiceId = null
        };
        var beforeCall = DateTime.UtcNow;

        // Act
        timeEntry.MarkAsInvoiced(invoiceId);
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(timeEntry.IsInvoiced, Is.True);
            Assert.That(timeEntry.InvoiceId, Is.EqualTo(invoiceId));
            Assert.That(timeEntry.UpdatedAt, Is.Not.Null);
            Assert.That(timeEntry.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void MarkAsInvoiced_WhenAlreadyInvoiced_UpdatesInvoiceId()
    {
        // Arrange
        var oldInvoiceId = Guid.NewGuid();
        var newInvoiceId = Guid.NewGuid();
        var timeEntry = new TimeEntry
        {
            IsInvoiced = true,
            InvoiceId = oldInvoiceId
        };

        // Act
        timeEntry.MarkAsInvoiced(newInvoiceId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(timeEntry.IsInvoiced, Is.True);
            Assert.That(timeEntry.InvoiceId, Is.EqualTo(newInvoiceId));
        });
    }

    [Test]
    public void TimeEntry_IsBillable_CanBeSetToFalse()
    {
        // Arrange & Act
        var timeEntry = new TimeEntry { IsBillable = false };

        // Assert
        Assert.That(timeEntry.IsBillable, Is.False);
    }

    [Test]
    public void TimeEntry_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var timeEntry = new TimeEntry
        {
            InvoiceId = null,
            UpdatedAt = null,
            Project = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(timeEntry.InvoiceId, Is.Null);
            Assert.That(timeEntry.UpdatedAt, Is.Null);
            Assert.That(timeEntry.Project, Is.Null);
        });
    }

    [Test]
    public void TimeEntry_Hours_CanBeZero()
    {
        // Arrange & Act
        var timeEntry = new TimeEntry { Hours = 0m };

        // Assert
        Assert.That(timeEntry.Hours, Is.EqualTo(0m));
    }

    [Test]
    public void TimeEntry_Hours_CanBeFractional()
    {
        // Arrange & Act
        var timeEntry = new TimeEntry { Hours = 2.5m };

        // Assert
        Assert.That(timeEntry.Hours, Is.EqualTo(2.5m));
    }

    [Test]
    public void TimeEntry_Hours_CanBeSmallIncrement()
    {
        // Arrange & Act
        var timeEntry = new TimeEntry { Hours = 0.25m };

        // Assert
        Assert.That(timeEntry.Hours, Is.EqualTo(0.25m));
    }

    [Test]
    public void TimeEntry_Hours_CanBeLargeValue()
    {
        // Arrange & Act
        var timeEntry = new TimeEntry { Hours = 24.0m };

        // Assert
        Assert.That(timeEntry.Hours, Is.EqualTo(24.0m));
    }

    [Test]
    public void TimeEntry_WorkDate_CanBeSetToSpecificDate()
    {
        // Arrange
        var specificDate = new DateTime(2024, 12, 25);
        var timeEntry = new TimeEntry();

        // Act
        timeEntry.WorkDate = specificDate;

        // Assert
        Assert.That(timeEntry.WorkDate, Is.EqualTo(specificDate));
    }

    [Test]
    public void TimeEntry_Description_CanBeEmptyString()
    {
        // Arrange & Act
        var timeEntry = new TimeEntry { Description = string.Empty };

        // Assert
        Assert.That(timeEntry.Description, Is.EqualTo(string.Empty));
    }

    [Test]
    public void TimeEntry_Description_CanBeLongText()
    {
        // Arrange
        var longDescription = new string('A', 1000);
        var timeEntry = new TimeEntry();

        // Act
        timeEntry.Description = longDescription;

        // Assert
        Assert.That(timeEntry.Description, Is.EqualTo(longDescription));
    }
}
