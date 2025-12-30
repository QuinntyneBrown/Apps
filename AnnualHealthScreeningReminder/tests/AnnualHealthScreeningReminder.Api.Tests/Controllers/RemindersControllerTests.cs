// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Controllers;
using AnnualHealthScreeningReminder.Api.Features.Reminders.Commands;
using AnnualHealthScreeningReminder.Api.Features.Reminders.DTOs;
using AnnualHealthScreeningReminder.Api.Features.Reminders.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AnnualHealthScreeningReminder.Api.Tests.Controllers;

[TestFixture]
public class RemindersControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<RemindersController>> _loggerMock;
    private RemindersController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RemindersController>>();
        _controller = new RemindersController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkWithReminders()
    {
        // Arrange
        var reminders = new List<ReminderDto>
        {
            new() { ReminderId = Guid.NewGuid(), Message = "Time for checkup" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllReminders.Query>(), default))
            .ReturnsAsync(reminders);

        // Act
        var result = await _controller.GetAll(null, null, null, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(reminders);
    }

    [Test]
    public async Task GetById_ExistingId_ReturnsOkWithReminder()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var reminder = new ReminderDto { ReminderId = reminderId, Message = "Time for checkup" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetReminderById.Query>(q => q.ReminderId == reminderId), default))
            .ReturnsAsync(reminder);

        // Act
        var result = await _controller.GetById(reminderId, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(reminder);
    }

    [Test]
    public async Task Create_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateReminder.Command(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(7),
            "Time for your annual checkup");
        var reminder = new ReminderDto { ReminderId = Guid.NewGuid(), Message = "Time for checkup" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(reminder);

        // Act
        var result = await _controller.Create(command, default);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result as CreatedAtActionResult;
        createdResult!.Value.Should().BeEquivalentTo(reminder);
    }

    [Test]
    public async Task MarkAsSent_ExistingId_ReturnsOk()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var reminder = new ReminderDto { ReminderId = reminderId, IsSent = true };
        _mediatorMock.Setup(m => m.Send(It.Is<MarkReminderAsSent.Command>(c => c.ReminderId == reminderId), default))
            .ReturnsAsync(reminder);

        // Act
        var result = await _controller.MarkAsSent(reminderId, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        var returnedReminder = okResult!.Value as ReminderDto;
        returnedReminder!.IsSent.Should().BeTrue();
    }

    [Test]
    public async Task Delete_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteReminder.Command>(c => c.ReminderId == reminderId), default))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Delete(reminderId, default);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
