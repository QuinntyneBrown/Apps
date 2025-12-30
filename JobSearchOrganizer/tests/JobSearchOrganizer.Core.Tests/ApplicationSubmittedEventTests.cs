// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class ApplicationSubmittedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesApplicationSubmittedEvent()
    {
        var applicationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var jobTitle = "Software Engineer";
        var companyId = Guid.NewGuid();
        var appliedDate = new DateTime(2025, 1, 15);
        var timestamp = DateTime.UtcNow;

        var evt = new ApplicationSubmittedEvent
        {
            ApplicationId = applicationId,
            UserId = userId,
            JobTitle = jobTitle,
            CompanyId = companyId,
            AppliedDate = appliedDate,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.ApplicationId, Is.EqualTo(applicationId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.JobTitle, Is.EqualTo(jobTitle));
            Assert.That(evt.CompanyId, Is.EqualTo(companyId));
            Assert.That(evt.AppliedDate, Is.EqualTo(appliedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        var evt = new ApplicationSubmittedEvent();
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var applicationId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new ApplicationSubmittedEvent { ApplicationId = applicationId, JobTitle = "Engineer", Timestamp = timestamp };
        var evt2 = new ApplicationSubmittedEvent { ApplicationId = applicationId, JobTitle = "Engineer", Timestamp = timestamp };

        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new ApplicationSubmittedEvent { ApplicationId = Guid.NewGuid(), JobTitle = "Original" };
        var modified = original with { JobTitle = "Modified" };

        Assert.That(modified.JobTitle, Is.EqualTo("Modified"));
        Assert.That(modified, Is.Not.SameAs(original));
    }
}
