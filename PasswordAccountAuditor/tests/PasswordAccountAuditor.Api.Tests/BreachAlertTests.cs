// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Api.Tests;

namespace PasswordAccountAuditor.Api.Tests;

[TestFixture]
public class BreachAlertTests
{
    [Test]
    public void BreachAlertDto_ToDto_MapsCorrectly()
    {
        // Arrange
        var breachAlert = new Core.BreachAlert
        {
            BreachAlertId = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            Severity = BreachSeverity.High,
            Status = AlertStatus.New,
            DetectedDate = DateTime.UtcNow,
            BreachDate = DateTime.UtcNow.AddDays(-7),
            Source = "HaveIBeenPwned",
            Description = "Data breach detected",
            DataCompromised = "Email, Password",
            RecommendedActions = "Change password immediately",
            Notes = "Test notes"
        };

        // Act
        var dto = breachAlert.ToDto();

        // Assert
        Assert.That(dto.BreachAlertId, Is.EqualTo(breachAlert.BreachAlertId));
        Assert.That(dto.AccountId, Is.EqualTo(breachAlert.AccountId));
        Assert.That(dto.Severity, Is.EqualTo(breachAlert.Severity));
        Assert.That(dto.Status, Is.EqualTo(breachAlert.Status));
        Assert.That(dto.Description, Is.EqualTo(breachAlert.Description));
        Assert.That(dto.Source, Is.EqualTo(breachAlert.Source));
    }

    [Test]
    public async Task CreateBreachAlertCommand_CreatesBreachAlert()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new CreateBreachAlertCommandHandler(mockContext.Object);
        var command = new CreateBreachAlertCommand
        {
            AccountId = Guid.NewGuid(),
            Severity = BreachSeverity.Critical,
            Description = "Critical breach detected",
            Source = "Security Audit"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Severity, Is.EqualTo(BreachSeverity.Critical));
        Assert.That(result.Status, Is.EqualTo(AlertStatus.New));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateBreachAlertCommand_UpdatesBreachAlert()
    {
        // Arrange
        var breachAlertId = Guid.NewGuid();
        var breachAlerts = new List<Core.BreachAlert>
        {
            new Core.BreachAlert
            {
                BreachAlertId = breachAlertId,
                AccountId = Guid.NewGuid(),
                Severity = BreachSeverity.Low,
                Status = AlertStatus.New,
                Description = "Old description"
            }
        };

        var mockContext = TestHelpers.CreateMockContext(breachAlerts: breachAlerts);
        var handler = new UpdateBreachAlertCommandHandler(mockContext.Object);
        var command = new UpdateBreachAlertCommand
        {
            BreachAlertId = breachAlertId,
            Severity = BreachSeverity.High,
            Status = AlertStatus.Acknowledged,
            Description = "Updated description"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Severity, Is.EqualTo(BreachSeverity.High));
        Assert.That(result.Status, Is.EqualTo(AlertStatus.Acknowledged));
        Assert.That(result.Description, Is.EqualTo("Updated description"));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void UpdateBreachAlertCommand_ThrowsException_WhenNotFound()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new UpdateBreachAlertCommandHandler(mockContext.Object);
        var command = new UpdateBreachAlertCommand
        {
            BreachAlertId = Guid.NewGuid(),
            Severity = BreachSeverity.Low,
            Status = AlertStatus.New,
            Description = "Test"
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task DeleteBreachAlertCommand_DeletesBreachAlert()
    {
        // Arrange
        var breachAlertId = Guid.NewGuid();
        var breachAlerts = new List<Core.BreachAlert>
        {
            new Core.BreachAlert
            {
                BreachAlertId = breachAlertId,
                AccountId = Guid.NewGuid(),
                Severity = BreachSeverity.Medium,
                Description = "Test"
            }
        };

        var mockContext = TestHelpers.CreateMockContext(breachAlerts: breachAlerts);
        var handler = new DeleteBreachAlertCommandHandler(mockContext.Object);
        var command = new DeleteBreachAlertCommand(breachAlertId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetBreachAlertsQuery_ReturnsAllBreachAlerts()
    {
        // Arrange
        var breachAlerts = new List<Core.BreachAlert>
        {
            new Core.BreachAlert
            {
                BreachAlertId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Severity = BreachSeverity.High,
                Description = "Alert 1",
                DetectedDate = DateTime.UtcNow
            },
            new Core.BreachAlert
            {
                BreachAlertId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Severity = BreachSeverity.Low,
                Description = "Alert 2",
                DetectedDate = DateTime.UtcNow
            }
        };

        var mockContext = TestHelpers.CreateMockContext(breachAlerts: breachAlerts);
        var handler = new GetBreachAlertsQueryHandler(mockContext.Object);
        var query = new GetBreachAlertsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetBreachAlertByIdQuery_ReturnsBreachAlert_WhenExists()
    {
        // Arrange
        var breachAlertId = Guid.NewGuid();
        var breachAlerts = new List<Core.BreachAlert>
        {
            new Core.BreachAlert
            {
                BreachAlertId = breachAlertId,
                AccountId = Guid.NewGuid(),
                Severity = BreachSeverity.Critical,
                Description = "Test Alert"
            }
        };

        var mockContext = TestHelpers.CreateMockContext(breachAlerts: breachAlerts);
        var handler = new GetBreachAlertByIdQueryHandler(mockContext.Object);
        var query = new GetBreachAlertByIdQuery(breachAlertId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.BreachAlertId, Is.EqualTo(breachAlertId));
        Assert.That(result.Description, Is.EqualTo("Test Alert"));
    }

    [Test]
    public async Task GetBreachAlertByIdQuery_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new GetBreachAlertByIdQueryHandler(mockContext.Object);
        var query = new GetBreachAlertByIdQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}
