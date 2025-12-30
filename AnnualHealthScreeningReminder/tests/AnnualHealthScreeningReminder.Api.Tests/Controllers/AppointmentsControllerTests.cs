// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Controllers;
using AnnualHealthScreeningReminder.Api.Features.Appointments.Commands;
using AnnualHealthScreeningReminder.Api.Features.Appointments.DTOs;
using AnnualHealthScreeningReminder.Api.Features.Appointments.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AnnualHealthScreeningReminder.Api.Tests.Controllers;

[TestFixture]
public class AppointmentsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<AppointmentsController>> _loggerMock;
    private AppointmentsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<AppointmentsController>>();
        _controller = new AppointmentsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkWithAppointments()
    {
        // Arrange
        var appointments = new List<AppointmentDto>
        {
            new() { AppointmentId = Guid.NewGuid(), Location = "Clinic A" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAppointments.Query>(), default))
            .ReturnsAsync(appointments);

        // Act
        var result = await _controller.GetAll(null, null, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(appointments);
    }

    [Test]
    public async Task GetById_ExistingId_ReturnsOkWithAppointment()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var appointment = new AppointmentDto { AppointmentId = appointmentId, Location = "Clinic A" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetAppointmentById.Query>(q => q.AppointmentId == appointmentId), default))
            .ReturnsAsync(appointment);

        // Act
        var result = await _controller.GetById(appointmentId, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(appointment);
    }

    [Test]
    public async Task Create_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateAppointment.Command(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(30),
            "Clinic A",
            "Dr. Smith",
            "Regular checkup");
        var appointment = new AppointmentDto { AppointmentId = Guid.NewGuid(), Location = "Clinic A" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(appointment);

        // Act
        var result = await _controller.Create(command, default);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result as CreatedAtActionResult;
        createdResult!.Value.Should().BeEquivalentTo(appointment);
    }

    [Test]
    public async Task Update_ValidCommand_ReturnsOk()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        var command = new UpdateAppointment.Command(
            appointmentId,
            DateTime.UtcNow.AddDays(30),
            "Clinic A",
            "Dr. Smith",
            false,
            "Regular checkup");
        var appointment = new AppointmentDto { AppointmentId = appointmentId, Location = "Clinic A" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(appointment);

        // Act
        var result = await _controller.Update(appointmentId, command, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(appointment);
    }

    [Test]
    public async Task Delete_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteAppointment.Command>(c => c.AppointmentId == appointmentId), default))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Delete(appointmentId, default);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
