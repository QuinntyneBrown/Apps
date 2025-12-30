// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Api.Controllers;
using ConferenceEventManager.Api.Features.Sessions;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConferenceEventManager.Api.Tests.Controllers;

[TestFixture]
public class SessionsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<SessionsController>> _loggerMock = null!;
    private SessionsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<SessionsController>>();
        _controller = new SessionsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetSessions_ReturnsOkResult_WithListOfSessions()
    {
        // Arrange
        var sessions = new List<SessionDto>
        {
            new SessionDto
            {
                SessionId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                EventId = Guid.NewGuid(),
                Title = "Test Session",
                StartTime = DateTime.UtcNow.AddDays(30)
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetSessions.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(sessions);

        // Act
        var result = await _controller.GetSessions(null, null, CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);

        var returnedSessions = okResult.Value as List<SessionDto>;
        returnedSessions.Should().NotBeNull();
        returnedSessions.Should().HaveCount(1);
    }

    [Test]
    public async Task CreateSession_ReturnsCreatedResult_WithNewSession()
    {
        // Arrange
        var command = new CreateSession.Command
        {
            UserId = Guid.NewGuid(),
            EventId = Guid.NewGuid(),
            Title = "New Session",
            StartTime = DateTime.UtcNow.AddDays(30)
        };

        var createdSession = new SessionDto
        {
            SessionId = Guid.NewGuid(),
            UserId = command.UserId,
            EventId = command.EventId,
            Title = command.Title,
            StartTime = command.StartTime
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateSession.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdSession);

        // Act
        var result = await _controller.CreateSession(command, CancellationToken.None);

        // Assert
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        createdResult!.StatusCode.Should().Be(201);
    }

    [Test]
    public async Task DeleteSession_ReturnsNoContent_WhenSessionExists()
    {
        // Arrange
        var sessionId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteSession.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteSession(sessionId, CancellationToken.None);

        // Assert
        var noContentResult = result as NoContentResult;
        noContentResult.Should().NotBeNull();
        noContentResult!.StatusCode.Should().Be(204);
    }
}
