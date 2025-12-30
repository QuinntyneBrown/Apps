// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Api.Controllers;
using FriendGroupEventCoordinator.Api.Features.RSVPs;
using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FriendGroupEventCoordinator.Api.Tests.Controllers;

[TestFixture]
public class RSVPsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<RSVPsController>> _loggerMock;
    private RSVPsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RSVPsController>>();
        _controller = new RSVPsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetRSVP_WithValidId_ReturnsOkResultWithRSVP()
    {
        // Arrange
        var rsvpId = Guid.NewGuid();
        var rsvpDto = new RSVPDto { RSVPId = rsvpId, Response = RSVPResponse.Yes };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRSVPQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(rsvpDto);

        // Act
        var result = await _controller.GetRSVP(rsvpId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(rsvpDto));
    }

    [Test]
    public async Task GetRSVP_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var rsvpId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRSVPQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((RSVPDto?)null);

        // Act
        var result = await _controller.GetRSVP(rsvpId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task GetRSVPsByEvent_ReturnsOkResultWithRSVPs()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var rsvps = new List<RSVPDto>
        {
            new RSVPDto { RSVPId = Guid.NewGuid(), EventId = eventId, Response = RSVPResponse.Yes },
            new RSVPDto { RSVPId = Guid.NewGuid(), EventId = eventId, Response = RSVPResponse.No }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRSVPsByEventQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(rsvps);

        // Act
        var result = await _controller.GetRSVPsByEvent(eventId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(rsvps));
    }

    [Test]
    public async Task CreateRSVP_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var createDto = new CreateRSVPDto
        {
            EventId = Guid.NewGuid(),
            MemberId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Response = RSVPResponse.Yes
        };
        var createdRSVP = new RSVPDto { RSVPId = Guid.NewGuid(), Response = RSVPResponse.Yes };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateRSVPCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdRSVP);

        // Act
        var result = await _controller.CreateRSVP(createDto, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult.Value, Is.EqualTo(createdRSVP));
    }

    [Test]
    public async Task UpdateRSVP_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var rsvpId = Guid.NewGuid();
        var updateDto = new UpdateRSVPDto { Response = RSVPResponse.No };
        var updatedRSVP = new RSVPDto { RSVPId = rsvpId, Response = RSVPResponse.No };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateRSVPCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedRSVP);

        // Act
        var result = await _controller.UpdateRSVP(rsvpId, updateDto, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task DeleteRSVP_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var rsvpId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRSVPCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteRSVP(rsvpId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteRSVP_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var rsvpId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRSVPCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteRSVP(rsvpId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
