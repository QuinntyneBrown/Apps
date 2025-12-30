// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Api.Controllers;
using FriendGroupEventCoordinator.Api.Features.Events;
using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FriendGroupEventCoordinator.Api.Tests.Controllers;

[TestFixture]
public class EventsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<EventsController>> _loggerMock;
    private EventsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<EventsController>>();
        _controller = new EventsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetEvents_ReturnsOkResultWithEvents()
    {
        // Arrange
        var events = new List<EventDto>
        {
            new EventDto { EventId = Guid.NewGuid(), Title = "Test Event 1" },
            new EventDto { EventId = Guid.NewGuid(), Title = "Test Event 2" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(events);

        // Act
        var result = await _controller.GetEvents(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(events));
    }

    [Test]
    public async Task GetEvent_WithValidId_ReturnsOkResultWithEvent()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var eventDto = new EventDto { EventId = eventId, Title = "Test Event" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventDto);

        // Act
        var result = await _controller.GetEvent(eventId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(eventDto));
    }

    [Test]
    public async Task GetEvent_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((EventDto?)null);

        // Act
        var result = await _controller.GetEvent(eventId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateEvent_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var createDto = new CreateEventDto { Title = "New Event", GroupId = Guid.NewGuid() };
        var createdEvent = new EventDto { EventId = Guid.NewGuid(), Title = "New Event" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdEvent);

        // Act
        var result = await _controller.CreateEvent(createDto, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult.Value, Is.EqualTo(createdEvent));
    }

    [Test]
    public async Task UpdateEvent_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var updateDto = new UpdateEventDto { Title = "Updated Event" };
        var updatedEvent = new EventDto { EventId = eventId, Title = "Updated Event" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEventCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedEvent);

        // Act
        var result = await _controller.UpdateEvent(eventId, updateDto, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task CancelEvent_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var cancelledEvent = new EventDto { EventId = eventId, IsCancelled = true };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CancelEventCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cancelledEvent);

        // Act
        var result = await _controller.CancelEvent(eventId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task DeleteEvent_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteEvent(eventId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteEvent_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteEvent(eventId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
