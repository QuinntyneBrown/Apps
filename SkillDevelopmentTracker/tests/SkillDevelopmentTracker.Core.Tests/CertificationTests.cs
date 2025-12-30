// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core.Tests;

public class CertificationTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCertification()
    {
        // Arrange
        var certificationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "AWS Solutions Architect";
        var issuingOrg = "Amazon Web Services";
        var issueDate = new DateTime(2023, 1, 15);
        var expirationDate = new DateTime(2026, 1, 15);
        var credentialId = "AWS-SA-12345";
        var credentialUrl = "https://aws.amazon.com/verify/12345";
        var skillIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var notes = "Passed on first attempt";

        // Act
        var certification = new Certification
        {
            CertificationId = certificationId,
            UserId = userId,
            Name = name,
            IssuingOrganization = issuingOrg,
            IssueDate = issueDate,
            ExpirationDate = expirationDate,
            CredentialId = credentialId,
            CredentialUrl = credentialUrl,
            SkillIds = skillIds,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(certification.CertificationId, Is.EqualTo(certificationId));
            Assert.That(certification.UserId, Is.EqualTo(userId));
            Assert.That(certification.Name, Is.EqualTo(name));
            Assert.That(certification.IssuingOrganization, Is.EqualTo(issuingOrg));
            Assert.That(certification.IssueDate, Is.EqualTo(issueDate));
            Assert.That(certification.ExpirationDate, Is.EqualTo(expirationDate));
            Assert.That(certification.CredentialId, Is.EqualTo(credentialId));
            Assert.That(certification.CredentialUrl, Is.EqualTo(credentialUrl));
            Assert.That(certification.IsActive, Is.True);
            Assert.That(certification.SkillIds, Has.Count.EqualTo(2));
            Assert.That(certification.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_NewCertification_HasExpectedDefaults()
    {
        // Act
        var certification = new Certification();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(certification.Name, Is.EqualTo(string.Empty));
            Assert.That(certification.IssuingOrganization, Is.EqualTo(string.Empty));
            Assert.That(certification.IsActive, Is.True);
            Assert.That(certification.SkillIds, Is.Not.Null);
            Assert.That(certification.SkillIds, Is.Empty);
            Assert.That(certification.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsExpired_WithFutureExpirationDate_ReturnsFalse()
    {
        // Arrange
        var certification = new Certification
        {
            ExpirationDate = DateTime.UtcNow.AddYears(1)
        };

        // Act
        var isExpired = certification.IsExpired();

        // Assert
        Assert.That(isExpired, Is.False);
    }

    [Test]
    public void IsExpired_WithPastExpirationDate_ReturnsTrue()
    {
        // Arrange
        var certification = new Certification
        {
            ExpirationDate = DateTime.UtcNow.AddYears(-1)
        };

        // Act
        var isExpired = certification.IsExpired();

        // Assert
        Assert.That(isExpired, Is.True);
    }

    [Test]
    public void IsExpired_WithNoExpirationDate_ReturnsFalse()
    {
        // Arrange
        var certification = new Certification
        {
            ExpirationDate = null
        };

        // Act
        var isExpired = certification.IsExpired();

        // Assert
        Assert.That(isExpired, Is.False);
    }

    [Test]
    public void IsExpired_WithExpirationDateToday_ReturnsTrue()
    {
        // Arrange
        var certification = new Certification
        {
            ExpirationDate = DateTime.UtcNow.AddHours(-1)
        };

        // Act
        var isExpired = certification.IsExpired();

        // Assert
        Assert.That(isExpired, Is.True);
    }

    [Test]
    public void Renew_WithNewExpirationDate_UpdatesExpirationDate()
    {
        // Arrange
        var certification = new Certification
        {
            ExpirationDate = DateTime.UtcNow.AddYears(-1),
            IsActive = false
        };
        var newExpirationDate = DateTime.UtcNow.AddYears(3);

        // Act
        certification.Renew(newExpirationDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(certification.ExpirationDate, Is.EqualTo(newExpirationDate));
            Assert.That(certification.IsActive, Is.True);
            Assert.That(certification.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Renew_ExpiredCertification_ReactivatesCertification()
    {
        // Arrange
        var certification = new Certification
        {
            IsActive = false,
            ExpirationDate = DateTime.UtcNow.AddYears(-1)
        };
        var newExpirationDate = DateTime.UtcNow.AddYears(3);

        // Act
        certification.Renew(newExpirationDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(certification.IsActive, Is.True);
            Assert.That(certification.ExpirationDate, Is.EqualTo(newExpirationDate));
        });
    }

    [Test]
    public void SkillIds_AddMultipleSkills_StoresAllSkills()
    {
        // Arrange
        var certification = new Certification();
        var skill1 = Guid.NewGuid();
        var skill2 = Guid.NewGuid();
        var skill3 = Guid.NewGuid();

        // Act
        certification.SkillIds.Add(skill1);
        certification.SkillIds.Add(skill2);
        certification.SkillIds.Add(skill3);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(certification.SkillIds, Has.Count.EqualTo(3));
            Assert.That(certification.SkillIds, Contains.Item(skill1));
            Assert.That(certification.SkillIds, Contains.Item(skill2));
            Assert.That(certification.SkillIds, Contains.Item(skill3));
        });
    }

    [Test]
    public void IsActive_DefaultValue_IsTrue()
    {
        // Arrange & Act
        var certification = new Certification();

        // Assert
        Assert.That(certification.IsActive, Is.True);
    }

    [Test]
    public void IsActive_CanBeSetToFalse_StoresValue()
    {
        // Arrange
        var certification = new Certification
        {
            IsActive = false
        };

        // Assert
        Assert.That(certification.IsActive, Is.False);
    }

    [Test]
    public void CredentialId_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var certification = new Certification
        {
            CredentialId = null
        };

        // Assert
        Assert.That(certification.CredentialId, Is.Null);
    }

    [Test]
    public void CredentialUrl_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var certification = new Certification
        {
            CredentialUrl = null
        };

        // Assert
        Assert.That(certification.CredentialUrl, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var certification = new Certification
        {
            Notes = null
        };

        // Assert
        Assert.That(certification.Notes, Is.Null);
    }

    [Test]
    public void UpdatedAt_InitiallyNull_BeforeAnyUpdates()
    {
        // Arrange & Act
        var certification = new Certification();

        // Assert
        Assert.That(certification.UpdatedAt, Is.Null);
    }
}
