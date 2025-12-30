// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Tests;

[TestFixture]
public class DoseScheduleTests
{
    [Test]
    public void DoseScheduleToDto_MapsAllProperties()
    {
        // Arrange
        var doseSchedule = new DoseSchedule
        {
            DoseScheduleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(8, 0, 0),
            DaysOfWeek = "Monday,Wednesday,Friday",
            Frequency = "Daily",
            ReminderEnabled = true,
            ReminderOffsetMinutes = 15,
            LastTaken = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = doseSchedule.ToDto();

        // Assert
        Assert.That(dto.DoseScheduleId, Is.EqualTo(doseSchedule.DoseScheduleId));
        Assert.That(dto.UserId, Is.EqualTo(doseSchedule.UserId));
        Assert.That(dto.MedicationId, Is.EqualTo(doseSchedule.MedicationId));
        Assert.That(dto.ScheduledTime, Is.EqualTo(doseSchedule.ScheduledTime));
        Assert.That(dto.DaysOfWeek, Is.EqualTo(doseSchedule.DaysOfWeek));
        Assert.That(dto.Frequency, Is.EqualTo(doseSchedule.Frequency));
        Assert.That(dto.ReminderEnabled, Is.EqualTo(doseSchedule.ReminderEnabled));
        Assert.That(dto.ReminderOffsetMinutes, Is.EqualTo(doseSchedule.ReminderOffsetMinutes));
        Assert.That(dto.LastTaken, Is.EqualTo(doseSchedule.LastTaken));
        Assert.That(dto.IsActive, Is.EqualTo(doseSchedule.IsActive));
        Assert.That(dto.CreatedAt, Is.EqualTo(doseSchedule.CreatedAt));
    }

    [Test]
    public async Task CreateDoseScheduleCommand_CreatesNewDoseSchedule()
    {
        // Arrange
        var doseSchedules = new List<DoseSchedule>();
        var mockSet = TestHelpers.CreateMockDbSet(doseSchedules);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<CreateDoseScheduleCommandHandler>();

        mockContext.Setup(c => c.DoseSchedules).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateDoseScheduleCommandHandler(mockContext.Object, mockLogger.Object);
        var command = new CreateDoseScheduleCommand
        {
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            ScheduledTime = new TimeSpan(9, 0, 0),
            DaysOfWeek = "Daily",
            Frequency = "Once daily",
            ReminderEnabled = true,
            ReminderOffsetMinutes = 30
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ScheduledTime, Is.EqualTo(command.ScheduledTime));
        Assert.That(result.DaysOfWeek, Is.EqualTo(command.DaysOfWeek));
        Assert.That(result.Frequency, Is.EqualTo(command.Frequency));
        Assert.That(doseSchedules.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetDoseSchedulesQuery_ReturnsAllDoseSchedules()
    {
        // Arrange
        var doseSchedules = new List<DoseSchedule>
        {
            new DoseSchedule
            {
                DoseScheduleId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                MedicationId = Guid.NewGuid(),
                ScheduledTime = new TimeSpan(8, 0, 0),
                DaysOfWeek = "Daily",
                Frequency = "Once daily",
                CreatedAt = DateTime.UtcNow
            },
            new DoseSchedule
            {
                DoseScheduleId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                MedicationId = Guid.NewGuid(),
                ScheduledTime = new TimeSpan(20, 0, 0),
                DaysOfWeek = "Daily",
                Frequency = "Once daily",
                CreatedAt = DateTime.UtcNow
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(doseSchedules);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<GetDoseSchedulesQueryHandler>();

        mockContext.Setup(c => c.DoseSchedules).Returns(mockSet.Object);

        var handler = new GetDoseSchedulesQueryHandler(mockContext.Object, mockLogger.Object);
        var query = new GetDoseSchedulesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task UpdateDoseScheduleCommand_UpdatesDoseSchedule()
    {
        // Arrange
        var doseScheduleId = Guid.NewGuid();
        var doseSchedules = new List<DoseSchedule>
        {
            new DoseSchedule
            {
                DoseScheduleId = doseScheduleId,
                UserId = Guid.NewGuid(),
                MedicationId = Guid.NewGuid(),
                ScheduledTime = new TimeSpan(8, 0, 0),
                DaysOfWeek = "Daily",
                Frequency = "Once daily",
                ReminderEnabled = true,
                ReminderOffsetMinutes = 15
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(doseSchedules);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<UpdateDoseScheduleCommandHandler>();

        mockContext.Setup(c => c.DoseSchedules).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new UpdateDoseScheduleCommandHandler(mockContext.Object, mockLogger.Object);
        var command = new UpdateDoseScheduleCommand
        {
            DoseScheduleId = doseScheduleId,
            ScheduledTime = new TimeSpan(9, 0, 0),
            DaysOfWeek = "Monday,Wednesday,Friday",
            Frequency = "Three times weekly",
            ReminderEnabled = false,
            ReminderOffsetMinutes = 0,
            IsActive = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ScheduledTime, Is.EqualTo(command.ScheduledTime));
        Assert.That(result.DaysOfWeek, Is.EqualTo(command.DaysOfWeek));
        Assert.That(result.Frequency, Is.EqualTo(command.Frequency));
        Assert.That(result.ReminderEnabled, Is.EqualTo(command.ReminderEnabled));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
