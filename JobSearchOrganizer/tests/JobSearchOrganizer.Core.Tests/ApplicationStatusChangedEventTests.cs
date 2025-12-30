// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class ApplicationStatusChangedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesApplicationStatusChangedEvent()
    {
        var applicationId = Guid.NewGuid();
        var oldStatus = ApplicationStatus.Applied;
        var newStatus = ApplicationStatus.Interviewing;
        var timestamp = DateTime.UtcNow;

        var evt = new ApplicationStatusChangedEvent
        {
            ApplicationId = applicationId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.ApplicationId, Is.EqualTo(applicationId));
            Assert.That(evt.OldStatus, Is.EqualTo(oldStatus));
            Assert.That(evt.NewStatus, Is.EqualTo(newStatus));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var applicationId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new ApplicationStatusChangedEvent { ApplicationId = applicationId, OldStatus = ApplicationStatus.Applied, NewStatus = ApplicationStatus.Interviewing, Timestamp = timestamp };
        var evt2 = new ApplicationStatusChangedEvent { ApplicationId = applicationId, OldStatus = ApplicationStatus.Applied, NewStatus = ApplicationStatus.Interviewing, Timestamp = timestamp };

        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new ApplicationStatusChangedEvent { ApplicationId = Guid.NewGuid(), OldStatus = ApplicationStatus.Draft, NewStatus = ApplicationStatus.Applied };
        var modified = original with { NewStatus = ApplicationStatus.Interviewing };

        Assert.That(modified.NewStatus, Is.EqualTo(ApplicationStatus.Interviewing));
        Assert.That(modified, Is.Not.SameAs(original));
    }
}
