// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core.Tests;

public class InterviewTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesInterview()
    {
        var interviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var applicationId = Guid.NewGuid();
        var interviewType = "Technical";
        var scheduledDateTime = new DateTime(2025, 1, 20, 10, 0, 0);
        var durationMinutes = 60;

        var interview = new Interview
        {
            InterviewId = interviewId,
            UserId = userId,
            ApplicationId = applicationId,
            InterviewType = interviewType,
            ScheduledDateTime = scheduledDateTime,
            DurationMinutes = durationMinutes
        };

        Assert.Multiple(() =>
        {
            Assert.That(interview.InterviewId, Is.EqualTo(interviewId));
            Assert.That(interview.UserId, Is.EqualTo(userId));
            Assert.That(interview.ApplicationId, Is.EqualTo(applicationId));
            Assert.That(interview.InterviewType, Is.EqualTo(interviewType));
            Assert.That(interview.ScheduledDateTime, Is.EqualTo(scheduledDateTime));
            Assert.That(interview.DurationMinutes, Is.EqualTo(durationMinutes));
            Assert.That(interview.IsCompleted, Is.False);
            Assert.That(interview.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Complete_SetsCompletedFlagAndTimestamps()
    {
        var interview = new Interview { IsCompleted = false, CompletedDate = null, UpdatedAt = null };

        interview.Complete();

        Assert.Multiple(() =>
        {
            Assert.That(interview.IsCompleted, Is.True);
            Assert.That(interview.CompletedDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(interview.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Reschedule_UpdatesScheduledDateTimeAndTimestamp()
    {
        var oldDateTime = new DateTime(2025, 1, 15, 10, 0, 0);
        var newDateTime = new DateTime(2025, 1, 20, 14, 0, 0);
        var interview = new Interview { ScheduledDateTime = oldDateTime, UpdatedAt = null };

        interview.Reschedule(newDateTime);

        Assert.Multiple(() =>
        {
            Assert.That(interview.ScheduledDateTime, Is.EqualTo(newDateTime));
            Assert.That(interview.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsCompleted_DefaultValue_IsFalse()
    {
        var interview = new Interview();
        Assert.That(interview.IsCompleted, Is.False);
    }

    [Test]
    public void Interviewers_DefaultValue_IsEmptyList()
    {
        var interview = new Interview();
        Assert.That(interview.Interviewers, Is.Not.Null);
        Assert.That(interview.Interviewers, Is.Empty);
    }

    [Test]
    public void OptionalFields_CanBeNull()
    {
        var interview = new Interview
        {
            DurationMinutes = null,
            Location = null,
            PreparationNotes = null,
            Feedback = null,
            CompletedDate = null
        };

        Assert.Multiple(() =>
        {
            Assert.That(interview.DurationMinutes, Is.Null);
            Assert.That(interview.Location, Is.Null);
            Assert.That(interview.PreparationNotes, Is.Null);
            Assert.That(interview.Feedback, Is.Null);
            Assert.That(interview.CompletedDate, Is.Null);
        });
    }

    [Test]
    public void Application_NavigationProperty_CanBeSet()
    {
        var application = new Application { ApplicationId = Guid.NewGuid() };
        var interview = new Interview();
        interview.Application = application;
        Assert.That(interview.Application, Is.EqualTo(application));
    }
}
