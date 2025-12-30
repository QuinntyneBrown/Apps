// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class InterviewCompletedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesInterviewCompletedEvent()
    {
        var interviewId = Guid.NewGuid();
        var completedDate = new DateTime(2025, 1, 20, 11, 0, 0);
        var timestamp = DateTime.UtcNow;

        var evt = new InterviewCompletedEvent
        {
            InterviewId = interviewId,
            CompletedDate = completedDate,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.InterviewId, Is.EqualTo(interviewId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var id = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;

        var evt1 = new InterviewCompletedEvent { InterviewId = id, CompletedDate = completedDate, Timestamp = completedDate };
        var evt2 = new InterviewCompletedEvent { InterviewId = id, CompletedDate = completedDate, Timestamp = completedDate };

        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new InterviewCompletedEvent { InterviewId = Guid.NewGuid(), CompletedDate = DateTime.UtcNow };
        var newDate = DateTime.UtcNow.AddDays(1);
        var modified = original with { CompletedDate = newDate };

        Assert.That(modified.CompletedDate, Is.EqualTo(newDate));
    }
}
