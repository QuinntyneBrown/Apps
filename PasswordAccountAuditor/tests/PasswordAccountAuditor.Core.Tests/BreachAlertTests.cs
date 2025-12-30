// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class BreachAlertTests
{
    [Test]
    public void Constructor_CreatesBreachAlert_WithDefaultValues()
    {
        // Arrange & Act
        var alert = new BreachAlert();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(alert.BreachAlertId, Is.EqualTo(Guid.Empty));
            Assert.That(alert.AccountId, Is.EqualTo(Guid.Empty));
            Assert.That(alert.Severity, Is.EqualTo(BreachSeverity.Low));
            Assert.That(alert.Status, Is.EqualTo(AlertStatus.New));
            Assert.That(alert.DetectedDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(alert.BreachDate, Is.Null);
            Assert.That(alert.Source, Is.Null);
            Assert.That(alert.Description, Is.EqualTo(string.Empty));
            Assert.That(alert.DataCompromised, Is.Null);
            Assert.That(alert.RecommendedActions, Is.Null);
            Assert.That(alert.AcknowledgedAt, Is.Null);
            Assert.That(alert.ResolvedAt, Is.Null);
            Assert.That(alert.Notes, Is.Null);
            Assert.That(alert.Account, Is.Null);
        });
    }

    [Test]
    public void Acknowledge_SetsStatusToAcknowledged()
    {
        // Arrange
        var alert = new BreachAlert { Status = AlertStatus.New };

        // Act
        alert.Acknowledge();

        // Assert
        Assert.That(alert.Status, Is.EqualTo(AlertStatus.Acknowledged));
    }

    [Test]
    public void Acknowledge_SetsAcknowledgedAtToCurrentTime()
    {
        // Arrange
        var alert = new BreachAlert();
        var beforeAck = DateTime.UtcNow;

        // Act
        alert.Acknowledge();
        var afterAck = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(alert.AcknowledgedAt, Is.Not.Null);
            Assert.That(alert.AcknowledgedAt!.Value, Is.GreaterThanOrEqualTo(beforeAck));
            Assert.That(alert.AcknowledgedAt!.Value, Is.LessThanOrEqualTo(afterAck));
        });
    }

    [Test]
    public void Resolve_SetsStatusToResolved()
    {
        // Arrange
        var alert = new BreachAlert { Status = AlertStatus.Acknowledged };

        // Act
        alert.Resolve();

        // Assert
        Assert.That(alert.Status, Is.EqualTo(AlertStatus.Resolved));
    }

    [Test]
    public void Resolve_SetsResolvedAtToCurrentTime()
    {
        // Arrange
        var alert = new BreachAlert();
        var beforeResolve = DateTime.UtcNow;

        // Act
        alert.Resolve();
        var afterResolve = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(alert.ResolvedAt, Is.Not.Null);
            Assert.That(alert.ResolvedAt!.Value, Is.GreaterThanOrEqualTo(beforeResolve));
            Assert.That(alert.ResolvedAt!.Value, Is.LessThanOrEqualTo(afterResolve));
        });
    }

    [Test]
    public void Resolve_WithResolutionNotes_AppendsToNotes()
    {
        // Arrange
        var alert = new BreachAlert();

        // Act
        alert.Resolve("Password changed and account secured");

        // Assert
        Assert.That(alert.Notes, Does.Contain("Password changed and account secured"));
    }

    [Test]
    public void Resolve_WithExistingNotes_AppendsResolutionNotes()
    {
        // Arrange
        var alert = new BreachAlert { Notes = "Initial notes" };

        // Act
        alert.Resolve("Issue resolved");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(alert.Notes, Does.Contain("Initial notes"));
            Assert.That(alert.Notes, Does.Contain("Resolution: Issue resolved"));
        });
    }

    [Test]
    public void Dismiss_SetsStatusToDismissed()
    {
        // Arrange
        var alert = new BreachAlert { Status = AlertStatus.New };

        // Act
        alert.Dismiss("False positive");

        // Assert
        Assert.That(alert.Status, Is.EqualTo(AlertStatus.Dismissed));
    }

    [Test]
    public void Dismiss_AddsReasonToNotes()
    {
        // Arrange
        var alert = new BreachAlert();

        // Act
        alert.Dismiss("False positive - different email");

        // Assert
        Assert.That(alert.Notes, Does.Contain("Dismissed: False positive - different email"));
    }

    [Test]
    public void Dismiss_WithExistingNotes_AppendsReason()
    {
        // Arrange
        var alert = new BreachAlert { Notes = "Initial investigation" };

        // Act
        alert.Dismiss("Not applicable");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(alert.Notes, Does.Contain("Initial investigation"));
            Assert.That(alert.Notes, Does.Contain("Dismissed: Not applicable"));
        });
    }

    [Test]
    public void RequiresImmediateAction_ReturnsTrue_ForCriticalSeverity()
    {
        // Arrange
        var alert = new BreachAlert { Severity = BreachSeverity.Critical };

        // Act
        var result = alert.RequiresImmediateAction();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void RequiresImmediateAction_ReturnsTrue_ForHighSeverity()
    {
        // Arrange
        var alert = new BreachAlert { Severity = BreachSeverity.High };

        // Act
        var result = alert.RequiresImmediateAction();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void RequiresImmediateAction_ReturnsFalse_ForMediumSeverity()
    {
        // Arrange
        var alert = new BreachAlert { Severity = BreachSeverity.Medium };

        // Act
        var result = alert.RequiresImmediateAction();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void RequiresImmediateAction_ReturnsFalse_ForLowSeverity()
    {
        // Arrange
        var alert = new BreachAlert { Severity = BreachSeverity.Low };

        // Act
        var result = alert.RequiresImmediateAction();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void DaysSinceDetection_ReturnsZero_ForTodayDetection()
    {
        // Arrange
        var alert = new BreachAlert { DetectedDate = DateTime.UtcNow };

        // Act
        var days = alert.DaysSinceDetection();

        // Assert
        Assert.That(days, Is.EqualTo(0));
    }

    [Test]
    public void DaysSinceDetection_ReturnsCorrectDays_ForOlderDetection()
    {
        // Arrange
        var alert = new BreachAlert { DetectedDate = DateTime.UtcNow.AddDays(-5) };

        // Act
        var days = alert.DaysSinceDetection();

        // Assert
        Assert.That(days, Is.EqualTo(5));
    }
}
