// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Api.Controllers;
using ConferenceEventManager.Api.Features.Events;
using ConferenceEventManager.Core;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConferenceEventManager.Api.Tests.Controllers;

[TestFixture]
public class EventsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<EventsController>> _loggerMock = null!;
    private EventsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<EventsController>>();
        _controller = new EventsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetEvents_ReturnsOkResult_WithListOfEvents()
    {
        // Arrange
        var events = new List<EventDto>
        {
            new EventDto
            {
                EventId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Test Event",
                EventType = EventType.Conference,
                StartDate = DateTime.UtcNow.AddDays(30),
                EndDate = DateTime.UtcNow.AddDays(32)
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEvents.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(events);

        // Act
        var result = await _controller.GetEvents(null, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);

        var returnedEvents = okResult.Value as List<EventDto>;
        returnedEvents.Should().NotBeNull();
        returnedEvents.Should().HaveCount(1);
    }

    [Test]
    public async Task GetEvent_ReturnsOkResult_WhenEventExists()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventDto = new EventDto
        {
            EventId = eventId,
            UserId = Guid.NewGuid(),
            Name = "Test Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(32)
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEventById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventDto);

        // Act
        var result = await _controller.GetEvent(eventId, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);

        var returnedEvent = okResult.Value as EventDto;
        returnedEvent.Should().NotBeNull();
        returnedEvent!.EventId.Should().Be(eventId);
    }

    [Test]
    public async Task GetEvent_ReturnsNotFound_WhenEventDoesNotExist()
    {
        // Arrange
        var eventId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetEventById.Query>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException($"Event with ID {eventId} not found"));

        // Act
        var result = await _controller.GetEvent(eventId, CancellationToken.None);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(404);
    }

    [Test]
    public async Task CreateEvent_ReturnsCreatedResult_WithNewEvent()
    {
        // Arrange
        var command = new CreateEvent.Command
        {
            UserId = Guid.NewGuid(),
            Name = "New Event",
            EventType = EventType.Conference,
            StartDate = DateTime.UtcNow.AddDays(30),
            EndDate = DateTime.UtcNow.AddDays(32)
        };

        var createdEvent = new EventDto
        {
            EventId = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            EventType = command.EventType,
            StartDate = command.StartDate,
            EndDate = command.EndDate
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateEvent.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdEvent);

        // Act
        var result = await _controller.CreateEvent(command, CancellationToken.None);

        // Assert
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        createdResult!.StatusCode.Should().Be(201);

        var returnedEvent = createdResult.Value as EventDto;
        returnedEvent.Should().NotBeNull();
        returnedEvent!.Name.Should().Be(command.Name);
    }

    [Test]
    public async Task DeleteEvent_ReturnsNoContent_WhenEventExists()
    {
        // Arrange
        var eventId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteEvent.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteEvent(eventId, CancellationToken.None);

        // Assert
        var noContentResult = result as NoContentResult;
        noContentResult.Should().NotBeNull();
        noContentResult!.StatusCode.Should().Be(204);
    }

    [Test]
    public async Task DeleteEvent_ReturnsNotFound_WhenEventDoesNotExist()
    {
        // Arrange
        var eventId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteEvent.Command>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException($"Event with ID {eventId} not found"));

        // Act
        var result = await _controller.DeleteEvent(eventId, CancellationToken.None);

        // Assert
        var notFoundResult = result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(404);
    }
}
