// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core.Tests;

public class DoseScheduleTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDoseSchedule()
    {
        // Arrange
        var doseScheduleId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var medicationId = Guid.NewGuid();
        var scheduledTime = new TimeSpan(8, 30, 0);
        var daysOfWeek = "Monday,Wednesday,Friday";
        var frequency = "Daily";
        var reminderOffsetMinutes = 15;

        // Act
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = doseScheduleId,
            UserId = userId,
            MedicationId = medicationId,
            ScheduledTime = scheduledTime,
            DaysOfWeek = daysOfWeek,
            Frequency = frequency,
            ReminderEnabled = true,
            ReminderOffsetMinutes = reminderOffsetMinutes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(doseSchedule.DoseScheduleId, Is.EqualTo(doseScheduleId));
            Assert.That(doseSchedule.UserId, Is.EqualTo(userId));
            Assert.That(doseSchedule.MedicationId, Is.EqualTo(medicationId));
            Assert.That(doseSchedule.ScheduledTime, Is.EqualTo(scheduledTime));
            Assert.That(doseSchedule.DaysOfWeek, Is.EqualTo(daysOfWeek));
            Assert.That(doseSchedule.Frequency, Is.EqualTo(frequency));
            Assert.That(doseSchedule.ReminderEnabled, Is.True);
            Assert.That(doseSchedule.ReminderOffsetMinutes, Is.EqualTo(reminderOffsetMinutes));
            Assert.That(doseSchedule.IsActive, Is.True);
            Assert.That(doseSchedule.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var doseSchedule = new DoseSchedule();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(doseSchedule.DaysOfWeek, Is.EqualTo(string.Empty));
            Assert.That(doseSchedule.Frequency, Is.EqualTo(string.Empty));
            Assert.That(doseSchedule.ReminderEnabled, Is.True);
            Assert.That(doseSchedule.ReminderOffsetMinutes, Is.EqualTo(0));
            Assert.That(doseSchedule.LastTaken, Is.Null);
            Assert.That(doseSchedule.IsActive, Is.True);
            Assert.That(doseSchedule.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsTaken_WhenCalled_SetsLastTakenToNow()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0)
        };

        var beforeCall = DateTime.UtcNow;

        // Act
        doseSchedule.MarkAsTaken();

        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(doseSchedule.LastTaken, Is.Not.Null);
            Assert.That(doseSchedule.LastTaken!.Value, Is.GreaterThanOrEqualTo(beforeCall));
            Assert.That(doseSchedule.LastTaken!.Value, Is.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void MarkAsTaken_CalledMultipleTimes_UpdatesLastTaken()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0)
        };

        // Act
        doseSchedule.MarkAsTaken();
        var firstTaken = doseSchedule.LastTaken;

        System.Threading.Thread.Sleep(10); // Small delay to ensure different timestamp

        doseSchedule.MarkAsTaken();
        var secondTaken = doseSchedule.LastTaken;

        // Assert
        Assert.That(secondTaken, Is.GreaterThanOrEqualTo(firstTaken));
    }

    [Test]
    public void WasTakenToday_WhenTakenToday_ReturnsTrue()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0),
            LastTaken = DateTime.UtcNow
        };

        // Act
        var result = doseSchedule.WasTakenToday();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void WasTakenToday_WhenTakenYesterday_ReturnsFalse()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0),
            LastTaken = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = doseSchedule.WasTakenToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void WasTakenToday_WhenNeverTaken_ReturnsFalse()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0),
            LastTaken = null
        };

        // Act
        var result = doseSchedule.WasTakenToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void WasTakenToday_WhenTakenTomorrow_ReturnsFalse()
    {
        // Arrange (edge case for testing)
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0),
            LastTaken = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = doseSchedule.WasTakenToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Medication_CanBeAssociated_WithDoseSchedule()
    {
        // Arrange
        var medication = new Medication
        {
            MedicationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Aspirin",
            MedicationType = MedicationType.Tablet
        };

        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = medication.UserId,
            MedicationId = medication.MedicationId,
            ScheduledTime = new TimeSpan(8, 0, 0),
            Medication = medication
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(doseSchedule.MedicationId, Is.EqualTo(medication.MedicationId));
            Assert.That(doseSchedule.Medication, Is.Not.Null);
            Assert.That(doseSchedule.Medication.Name, Is.EqualTo("Aspirin"));
        });
    }

    [Test]
    public void ReminderEnabled_CanBeToggled()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0),
            ReminderEnabled = true
        };

        // Act
        doseSchedule.ReminderEnabled = false;

        // Assert
        Assert.That(doseSchedule.ReminderEnabled, Is.False);
    }

    [Test]
    public void IsActive_CanBeToggled()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0),
            IsActive = true
        };

        // Act
        doseSchedule.IsActive = false;

        // Assert
        Assert.That(doseSchedule.IsActive, Is.False);
    }

    [Test]
    public void ScheduledTime_CanBeSetToDifferentTimes()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid()
        };

        // Act & Assert - Morning
        doseSchedule.ScheduledTime = new TimeSpan(7, 0, 0);
        Assert.That(doseSchedule.ScheduledTime, Is.EqualTo(new TimeSpan(7, 0, 0)));

        // Act & Assert - Evening
        doseSchedule.ScheduledTime = new TimeSpan(20, 30, 0);
        Assert.That(doseSchedule.ScheduledTime, Is.EqualTo(new TimeSpan(20, 30, 0)));
    }

    [Test]
    public void DaysOfWeek_CanStoreJsonArray()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid()
        };

        var daysJson = "[\"Monday\", \"Wednesday\", \"Friday\"]";

        // Act
        doseSchedule.DaysOfWeek = daysJson;

        // Assert
        Assert.That(doseSchedule.DaysOfWeek, Is.EqualTo(daysJson));
    }

    [Test]
    public void Frequency_CanBeSetToDifferentValues()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid()
        };

        // Act & Assert - Daily
        doseSchedule.Frequency = "Daily";
        Assert.That(doseSchedule.Frequency, Is.EqualTo("Daily"));

        // Act & Assert - Weekly
        doseSchedule.Frequency = "Weekly";
        Assert.That(doseSchedule.Frequency, Is.EqualTo("Weekly"));

        // Act & Assert - As Needed
        doseSchedule.Frequency = "As Needed";
        Assert.That(doseSchedule.Frequency, Is.EqualTo("As Needed"));
    }
}
