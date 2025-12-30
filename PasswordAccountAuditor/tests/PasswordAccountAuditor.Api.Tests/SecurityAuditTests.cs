// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Api.Tests;

namespace PasswordAccountAuditor.Api.Tests;

[TestFixture]
public class SecurityAuditTests
{
    [Test]
    public void SecurityAuditDto_ToDto_MapsCorrectly()
    {
        // Arrange
        var securityAudit = new Core.SecurityAudit
        {
            SecurityAuditId = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            AuditType = AuditType.PasswordStrength,
            Status = AuditStatus.Completed,
            AuditDate = DateTime.UtcNow,
            Findings = "Password is strong",
            Recommendations = "Continue using strong passwords",
            SecurityScore = 85,
            Notes = "Test notes"
        };

        // Act
        var dto = securityAudit.ToDto();

        // Assert
        Assert.That(dto.SecurityAuditId, Is.EqualTo(securityAudit.SecurityAuditId));
        Assert.That(dto.AccountId, Is.EqualTo(securityAudit.AccountId));
        Assert.That(dto.AuditType, Is.EqualTo(securityAudit.AuditType));
        Assert.That(dto.Status, Is.EqualTo(securityAudit.Status));
        Assert.That(dto.SecurityScore, Is.EqualTo(securityAudit.SecurityScore));
        Assert.That(dto.Findings, Is.EqualTo(securityAudit.Findings));
    }

    [Test]
    public async Task CreateSecurityAuditCommand_CreatesSecurityAudit()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new CreateSecurityAuditCommandHandler(mockContext.Object);
        var command = new CreateSecurityAuditCommand
        {
            AccountId = Guid.NewGuid(),
            AuditType = AuditType.Automated,
            SecurityScore = 75,
            Findings = "Good security posture",
            Recommendations = "Enable 2FA"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AuditType, Is.EqualTo(AuditType.Automated));
        Assert.That(result.Status, Is.EqualTo(AuditStatus.Pending));
        Assert.That(result.SecurityScore, Is.EqualTo(75));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateSecurityAuditCommand_ClampsSecurityScore()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new CreateSecurityAuditCommandHandler(mockContext.Object);
        var command = new CreateSecurityAuditCommand
        {
            AccountId = Guid.NewGuid(),
            AuditType = AuditType.Manual,
            SecurityScore = 150, // Above maximum
            Findings = "Test"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.SecurityScore, Is.EqualTo(100));
    }

    [Test]
    public async Task UpdateSecurityAuditCommand_UpdatesSecurityAudit()
    {
        // Arrange
        var securityAuditId = Guid.NewGuid();
        var securityAudits = new List<Core.SecurityAudit>
        {
            new Core.SecurityAudit
            {
                SecurityAuditId = securityAuditId,
                AccountId = Guid.NewGuid(),
                AuditType = AuditType.Manual,
                Status = AuditStatus.Pending,
                SecurityScore = 50
            }
        };

        var mockContext = TestHelpers.CreateMockContext(securityAudits: securityAudits);
        var handler = new UpdateSecurityAuditCommandHandler(mockContext.Object);
        var command = new UpdateSecurityAuditCommand
        {
            SecurityAuditId = securityAuditId,
            AuditType = AuditType.Automated,
            Status = AuditStatus.Completed,
            SecurityScore = 80,
            Findings = "Updated findings"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Status, Is.EqualTo(AuditStatus.Completed));
        Assert.That(result.SecurityScore, Is.EqualTo(80));
        Assert.That(result.Findings, Is.EqualTo("Updated findings"));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void UpdateSecurityAuditCommand_ThrowsException_WhenNotFound()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new UpdateSecurityAuditCommandHandler(mockContext.Object);
        var command = new UpdateSecurityAuditCommand
        {
            SecurityAuditId = Guid.NewGuid(),
            AuditType = AuditType.Manual,
            Status = AuditStatus.Pending,
            SecurityScore = 50
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task DeleteSecurityAuditCommand_DeletesSecurityAudit()
    {
        // Arrange
        var securityAuditId = Guid.NewGuid();
        var securityAudits = new List<Core.SecurityAudit>
        {
            new Core.SecurityAudit
            {
                SecurityAuditId = securityAuditId,
                AccountId = Guid.NewGuid(),
                AuditType = AuditType.BreachDetection,
                SecurityScore = 60
            }
        };

        var mockContext = TestHelpers.CreateMockContext(securityAudits: securityAudits);
        var handler = new DeleteSecurityAuditCommandHandler(mockContext.Object);
        var command = new DeleteSecurityAuditCommand(securityAuditId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetSecurityAuditsQuery_ReturnsAllSecurityAudits()
    {
        // Arrange
        var securityAudits = new List<Core.SecurityAudit>
        {
            new Core.SecurityAudit
            {
                SecurityAuditId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                AuditType = AuditType.Manual,
                SecurityScore = 70,
                AuditDate = DateTime.UtcNow
            },
            new Core.SecurityAudit
            {
                SecurityAuditId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                AuditType = AuditType.Automated,
                SecurityScore = 80,
                AuditDate = DateTime.UtcNow
            }
        };

        var mockContext = TestHelpers.CreateMockContext(securityAudits: securityAudits);
        var handler = new GetSecurityAuditsQueryHandler(mockContext.Object);
        var query = new GetSecurityAuditsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetSecurityAuditByIdQuery_ReturnsSecurityAudit_WhenExists()
    {
        // Arrange
        var securityAuditId = Guid.NewGuid();
        var securityAudits = new List<Core.SecurityAudit>
        {
            new Core.SecurityAudit
            {
                SecurityAuditId = securityAuditId,
                AccountId = Guid.NewGuid(),
                AuditType = AuditType.Compliance,
                SecurityScore = 90
            }
        };

        var mockContext = TestHelpers.CreateMockContext(securityAudits: securityAudits);
        var handler = new GetSecurityAuditByIdQueryHandler(mockContext.Object);
        var query = new GetSecurityAuditByIdQuery(securityAuditId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.SecurityAuditId, Is.EqualTo(securityAuditId));
        Assert.That(result.SecurityScore, Is.EqualTo(90));
    }

    [Test]
    public async Task GetSecurityAuditByIdQuery_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var mockContext = TestHelpers.CreateMockContext();
        var handler = new GetSecurityAuditByIdQueryHandler(mockContext.Object);
        var query = new GetSecurityAuditByIdQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}
