// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core.Tests;

public class EventsTests
{
    [Test]
    public void AppointmentBookedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var screeningId = Guid.NewGuid();
        var appointmentDate = DateTime.UtcNow.AddDays(7);

        // Act
        var evt = new AppointmentBookedEvent
        {
            AppointmentId = appointmentId,
            ScreeningId = screeningId,
            AppointmentDate = appointmentDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AppointmentId, Is.EqualTo(appointmentId));
            Assert.That(evt.ScreeningId, Is.EqualTo(screeningId));
            Assert.That(evt.AppointmentDate, Is.EqualTo(appointmentDate));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void AppointmentBookedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var screeningId = Guid.NewGuid();
        var appointmentDate = DateTime.UtcNow.AddDays(7);
        var timestamp = DateTime.UtcNow.AddMinutes(-5);

        // Act
        var evt = new AppointmentBookedEvent
        {
            AppointmentId = appointmentId,
            ScreeningId = screeningId,
            AppointmentDate = appointmentDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void ScreeningScheduledEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var screeningType = ScreeningType.Mammogram;
        var nextDueDate = DateTime.UtcNow.AddMonths(12);

        // Act
        var evt = new ScreeningScheduledEvent
        {
            ScreeningId = screeningId,
            UserId = userId,
            ScreeningType = screeningType,
            NextDueDate = nextDueDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ScreeningId, Is.EqualTo(screeningId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.ScreeningType, Is.EqualTo(screeningType));
            Assert.That(evt.NextDueDate, Is.EqualTo(nextDueDate));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ScreeningScheduledEvent_NullNextDueDate_CreatesEvent()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var screeningType = ScreeningType.PhysicalExam;

        // Act
        var evt = new ScreeningScheduledEvent
        {
            ScreeningId = screeningId,
            UserId = userId,
            ScreeningType = screeningType,
            NextDueDate = null
        };

        // Assert
        Assert.That(evt.NextDueDate, Is.Null);
    }

    [Test]
    public void ScreeningScheduledEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var screeningType = ScreeningType.BloodWork;
        var timestamp = DateTime.UtcNow.AddMinutes(-10);

        // Act
        var evt = new ScreeningScheduledEvent
        {
            ScreeningId = screeningId,
            UserId = userId,
            ScreeningType = screeningType,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void ReminderSentEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var screeningId = Guid.NewGuid();

        // Act
        var evt = new ReminderSentEvent
        {
            ReminderId = reminderId,
            ScreeningId = screeningId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReminderId, Is.EqualTo(reminderId));
            Assert.That(evt.ScreeningId, Is.EqualTo(screeningId));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ReminderSentEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var screeningId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow.AddMinutes(-3);

        // Act
        var evt = new ReminderSentEvent
        {
            ReminderId = reminderId,
            ScreeningId = screeningId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void AppointmentBookedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new AppointmentBookedEvent
        {
            AppointmentId = Guid.NewGuid(),
            ScreeningId = Guid.NewGuid(),
            AppointmentDate = DateTime.UtcNow
        };
        var newAppointmentId = Guid.NewGuid();

        // Act
        var newEvt = evt with { AppointmentId = newAppointmentId };

        // Assert
        Assert.That(newEvt.AppointmentId, Is.EqualTo(newAppointmentId));
        Assert.That(newEvt.ScreeningId, Is.EqualTo(evt.ScreeningId));
    }

    [Test]
    public void ScreeningScheduledEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new ScreeningScheduledEvent
        {
            ScreeningId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ScreeningType = ScreeningType.PhysicalExam
        };
        var newScreeningType = ScreeningType.DentalCheckup;

        // Act
        var newEvt = evt with { ScreeningType = newScreeningType };

        // Assert
        Assert.That(newEvt.ScreeningType, Is.EqualTo(newScreeningType));
        Assert.That(newEvt.ScreeningId, Is.EqualTo(evt.ScreeningId));
    }

    [Test]
    public void ReminderSentEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new ReminderSentEvent
        {
            ReminderId = Guid.NewGuid(),
            ScreeningId = Guid.NewGuid()
        };
        var newReminderId = Guid.NewGuid();

        // Act
        var newEvt = evt with { ReminderId = newReminderId };

        // Assert
        Assert.That(newEvt.ReminderId, Is.EqualTo(newReminderId));
        Assert.That(newEvt.ScreeningId, Is.EqualTo(evt.ScreeningId));
    }
}
