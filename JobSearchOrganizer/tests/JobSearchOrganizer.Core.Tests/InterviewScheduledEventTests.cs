// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class InterviewScheduledEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesInterviewScheduledEvent()
    {
        var interviewId = Guid.NewGuid();
        var applicationId = Guid.NewGuid();
        var interviewType = "Technical";
        var scheduledDateTime = new DateTime(2025, 1, 20, 10, 0, 0);
        var timestamp = DateTime.UtcNow;

        var evt = new InterviewScheduledEvent
        {
            InterviewId = interviewId,
            ApplicationId = applicationId,
            InterviewType = interviewType,
            ScheduledDateTime = scheduledDateTime,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.InterviewId, Is.EqualTo(interviewId));
            Assert.That(evt.ApplicationId, Is.EqualTo(applicationId));
            Assert.That(evt.InterviewType, Is.EqualTo(interviewType));
            Assert.That(evt.ScheduledDateTime, Is.EqualTo(scheduledDateTime));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var id = Guid.NewGuid();
        var scheduledDateTime = DateTime.UtcNow;

        var evt1 = new InterviewScheduledEvent { InterviewId = id, InterviewType = "Phone", ScheduledDateTime = scheduledDateTime, Timestamp = scheduledDateTime };
        var evt2 = new InterviewScheduledEvent { InterviewId = id, InterviewType = "Phone", ScheduledDateTime = scheduledDateTime, Timestamp = scheduledDateTime };

        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new InterviewScheduledEvent { InterviewId = Guid.NewGuid(), InterviewType = "Phone" };
        var modified = original with { InterviewType = "Video" };

        Assert.That(modified.InterviewType, Is.EqualTo("Video"));
    }
}
