// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Appointments.Commands;
using AnnualHealthScreeningReminder.Core;
using AnnualHealthScreeningReminder.Infrastructure.Data;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Tests.Features.Appointments;

[TestFixture]
public class CreateAppointmentTests
{
    private AnnualHealthScreeningReminderContext _context;
    private CreateAppointment.Handler _handler;
    private CreateAppointment.Validator _validator;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AnnualHealthScreeningReminderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AnnualHealthScreeningReminderContext(options);
        _validator = new CreateAppointment.Validator();
        _handler = new CreateAppointment.Handler(_context, _validator);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesAppointment()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var screeningId = Guid.NewGuid();
        var appointmentDate = DateTime.UtcNow.AddDays(30);
        var command = new CreateAppointment.Command(
            userId,
            screeningId,
            appointmentDate,
            "Clinic A, 123 Main St",
            "Dr. Smith",
            "Annual checkup");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.AppointmentId.Should().NotBeEmpty();
        result.UserId.Should().Be(userId);
        result.ScreeningId.Should().Be(screeningId);
        result.Location.Should().Be("Clinic A, 123 Main St");
        result.IsCompleted.Should().BeFalse();

        var appointment = await _context.Appointments.FindAsync(result.AppointmentId);
        appointment.Should().NotBeNull();
        appointment!.Location.Should().Be("Clinic A, 123 Main St");
    }

    [Test]
    public void Validator_EmptyUserId_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateAppointment.Command(
            Guid.Empty,
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(30),
            "Clinic A",
            "Dr. Smith",
            "Regular checkup");

        // Act & Assert
        var act = async () => await _handler.Handle(command, default);
        act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public void Validator_EmptyLocation_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateAppointment.Command(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(30),
            "",
            "Dr. Smith",
            "Regular checkup");

        // Act & Assert
        var act = async () => await _handler.Handle(command, default);
        act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task Handle_FutureDate_SetsIsUpcomingTrue()
    {
        // Arrange
        var command = new CreateAppointment.Command(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(30),
            "Clinic A",
            "Dr. Smith",
            "Regular checkup");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsUpcoming.Should().BeTrue();
    }
}
