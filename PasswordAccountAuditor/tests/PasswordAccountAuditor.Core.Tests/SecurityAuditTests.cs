// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class SecurityAuditTests
{
    [Test]
    public void Constructor_CreatesSecurityAudit_WithDefaultValues()
    {
        // Arrange & Act
        var audit = new SecurityAudit();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(audit.SecurityAuditId, Is.EqualTo(Guid.Empty));
            Assert.That(audit.AccountId, Is.EqualTo(Guid.Empty));
            Assert.That(audit.AuditType, Is.EqualTo(AuditType.Manual));
            Assert.That(audit.Status, Is.EqualTo(AuditStatus.Pending));
            Assert.That(audit.AuditDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(audit.Findings, Is.Null);
            Assert.That(audit.Recommendations, Is.Null);
            Assert.That(audit.SecurityScore, Is.EqualTo(0));
            Assert.That(audit.Notes, Is.Null);
            Assert.That(audit.Account, Is.Null);
        });
    }

    [Test]
    public void Complete_SetsStatusToCompleted()
    {
        // Arrange
        var audit = new SecurityAudit();

        // Act
        audit.Complete("Test findings", 75);

        // Assert
        Assert.That(audit.Status, Is.EqualTo(AuditStatus.Completed));
    }

    [Test]
    public void Complete_SetsFindings()
    {
        // Arrange
        var audit = new SecurityAudit();
        var findings = "No critical issues found";

        // Act
        audit.Complete(findings, 85);

        // Assert
        Assert.That(audit.Findings, Is.EqualTo(findings));
    }

    [Test]
    public void Complete_SetsSecurityScore()
    {
        // Arrange
        var audit = new SecurityAudit();

        // Act
        audit.Complete("Test findings", 92);

        // Assert
        Assert.That(audit.SecurityScore, Is.EqualTo(92));
    }

    [Test]
    public void Complete_ClampsSecurityScoreToMaximum100()
    {
        // Arrange
        var audit = new SecurityAudit();

        // Act
        audit.Complete("Test findings", 150);

        // Assert
        Assert.That(audit.SecurityScore, Is.EqualTo(100));
    }

    [Test]
    public void Complete_ClampsSecurityScoreToMinimum0()
    {
        // Arrange
        var audit = new SecurityAudit();

        // Act
        audit.Complete("Test findings", -50);

        // Assert
        Assert.That(audit.SecurityScore, Is.EqualTo(0));
    }

    [Test]
    public void Complete_SetsRecommendations()
    {
        // Arrange
        var audit = new SecurityAudit();
        var recommendations = "Enable 2FA";

        // Act
        audit.Complete("Test findings", 75, recommendations);

        // Assert
        Assert.That(audit.Recommendations, Is.EqualTo(recommendations));
    }

    [Test]
    public void MarkAsFailed_SetsStatusToFailed()
    {
        // Arrange
        var audit = new SecurityAudit();

        // Act
        audit.MarkAsFailed("Connection timeout");

        // Assert
        Assert.That(audit.Status, Is.EqualTo(AuditStatus.Failed));
    }

    [Test]
    public void MarkAsFailed_SetsNotesWithReason()
    {
        // Arrange
        var audit = new SecurityAudit();
        var reason = "Unable to connect to service";

        // Act
        audit.MarkAsFailed(reason);

        // Assert
        Assert.That(audit.Notes, Is.EqualTo(reason));
    }

    [Test]
    public void HasCriticalIssues_ReturnsTrue_WhenScoreIsBelow40()
    {
        // Arrange
        var audit = new SecurityAudit();
        audit.Complete("Critical issues found", 39);

        // Act
        var result = audit.HasCriticalIssues();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void HasCriticalIssues_ReturnsFalse_WhenScoreIs40()
    {
        // Arrange
        var audit = new SecurityAudit();
        audit.Complete("Test findings", 40);

        // Act
        var result = audit.HasCriticalIssues();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void HasCriticalIssues_ReturnsFalse_WhenScoreIsAbove40()
    {
        // Arrange
        var audit = new SecurityAudit();
        audit.Complete("Test findings", 65);

        // Act
        var result = audit.HasCriticalIssues();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GetRiskLevel_ReturnsLowRisk_ForScoreAbove80()
    {
        // Arrange
        var audit = new SecurityAudit();
        audit.Complete("Test findings", 85);

        // Act
        var riskLevel = audit.GetRiskLevel();

        // Assert
        Assert.That(riskLevel, Is.EqualTo("Low Risk"));
    }

    [Test]
    public void GetRiskLevel_ReturnsMediumRisk_ForScore60To79()
    {
        // Arrange
        var audit = new SecurityAudit();
        audit.Complete("Test findings", 70);

        // Act
        var riskLevel = audit.GetRiskLevel();

        // Assert
        Assert.That(riskLevel, Is.EqualTo("Medium Risk"));
    }

    [Test]
    public void GetRiskLevel_ReturnsHighRisk_ForScore40To59()
    {
        // Arrange
        var audit = new SecurityAudit();
        audit.Complete("Test findings", 50);

        // Act
        var riskLevel = audit.GetRiskLevel();

        // Assert
        Assert.That(riskLevel, Is.EqualTo("High Risk"));
    }

    [Test]
    public void GetRiskLevel_ReturnsCriticalRisk_ForScoreBelow40()
    {
        // Arrange
        var audit = new SecurityAudit();
        audit.Complete("Test findings", 25);

        // Act
        var riskLevel = audit.GetRiskLevel();

        // Assert
        Assert.That(riskLevel, Is.EqualTo("Critical Risk"));
    }
}
