// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Controllers;
using AnnualHealthScreeningReminder.Api.Features.Screenings.Commands;
using AnnualHealthScreeningReminder.Api.Features.Screenings.DTOs;
using AnnualHealthScreeningReminder.Api.Features.Screenings.Queries;
using AnnualHealthScreeningReminder.Core;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AnnualHealthScreeningReminder.Api.Tests.Controllers;

[TestFixture]
public class ScreeningsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ScreeningsController>> _loggerMock;
    private ScreeningsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ScreeningsController>>();
        _controller = new ScreeningsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkWithScreenings()
    {
        // Arrange
        var screenings = new List<ScreeningDto>
        {
            new() { ScreeningId = Guid.NewGuid(), Name = "Physical Exam", ScreeningType = ScreeningType.PhysicalExam }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllScreenings.Query>(), default))
            .ReturnsAsync(screenings);

        // Act
        var result = await _controller.GetAll(null, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(screenings);
    }

    [Test]
    public async Task GetById_ExistingId_ReturnsOkWithScreening()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        var screening = new ScreeningDto { ScreeningId = screeningId, Name = "Dental Checkup" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetScreeningById.Query>(q => q.ScreeningId == screeningId), default))
            .ReturnsAsync(screening);

        // Act
        var result = await _controller.GetById(screeningId, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(screening);
    }

    [Test]
    public async Task GetById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetScreeningById.Query>(), default))
            .ThrowsAsync(new KeyNotFoundException($"Screening with ID {screeningId} not found."));

        // Act
        var result = await _controller.GetById(screeningId, default);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Test]
    public async Task Create_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateScreening.Command(
            Guid.NewGuid(),
            ScreeningType.PhysicalExam,
            "Physical Exam",
            12,
            null,
            "Dr. Smith",
            "Annual checkup");
        var screening = new ScreeningDto { ScreeningId = Guid.NewGuid(), Name = "Physical Exam" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(screening);

        // Act
        var result = await _controller.Create(command, default);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result as CreatedAtActionResult;
        createdResult!.Value.Should().BeEquivalentTo(screening);
    }

    [Test]
    public async Task Update_ValidCommand_ReturnsOk()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        var command = new UpdateScreening.Command(
            screeningId,
            ScreeningType.PhysicalExam,
            "Physical Exam",
            12,
            null,
            null,
            "Dr. Smith",
            "Annual checkup");
        var screening = new ScreeningDto { ScreeningId = screeningId, Name = "Physical Exam" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(screening);

        // Act
        var result = await _controller.Update(screeningId, command, default);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(screening);
    }

    [Test]
    public async Task Update_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        var command = new UpdateScreening.Command(
            Guid.NewGuid(),
            ScreeningType.PhysicalExam,
            "Physical Exam",
            12,
            null,
            null,
            "Dr. Smith",
            "Annual checkup");

        // Act
        var result = await _controller.Update(screeningId, command, default);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public async Task Delete_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteScreening.Command>(c => c.ScreeningId == screeningId), default))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Delete(screeningId, default);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task Delete_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var screeningId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteScreening.Command>(), default))
            .ThrowsAsync(new KeyNotFoundException($"Screening with ID {screeningId} not found."));

        // Act
        var result = await _controller.Delete(screeningId, default);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }
}
