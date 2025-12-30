// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Api.Controllers;
using DocumentVaultOrganizer.Api.Features.ExpirationAlerts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DocumentVaultOrganizer.Api.Tests.Controllers;

[TestFixture]
public class ExpirationAlertsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ExpirationAlertsController>> _loggerMock;
    private ExpirationAlertsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ExpirationAlertsController>>();
        _controller = new ExpirationAlertsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetExpirationAlerts_ReturnsOkResult_WithListOfAlerts()
    {
        // Arrange
        var alerts = new List<ExpirationAlertDto>
        {
            new ExpirationAlertDto
            {
                ExpirationAlertId = Guid.NewGuid(),
                DocumentId = Guid.NewGuid(),
                AlertDate = DateTime.UtcNow.AddDays(7),
                IsAcknowledged = false,
                CreatedAt = DateTime.UtcNow
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetExpirationAlerts.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(alerts);

        // Act
        var result = await _controller.GetExpirationAlerts(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(alerts));
    }

    [Test]
    public async Task GetExpirationAlertById_ReturnsOkResult_WhenAlertExists()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var alert = new ExpirationAlertDto
        {
            ExpirationAlertId = alertId,
            DocumentId = Guid.NewGuid(),
            AlertDate = DateTime.UtcNow.AddDays(7),
            IsAcknowledged = false,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetExpirationAlertById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(alert);

        // Act
        var result = await _controller.GetExpirationAlertById(alertId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(alert));
    }

    [Test]
    public async Task GetExpirationAlertById_ReturnsNotFound_WhenAlertDoesNotExist()
    {
        // Arrange
        var alertId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetExpirationAlertById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ExpirationAlertDto?)null);

        // Act
        var result = await _controller.GetExpirationAlertById(alertId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateExpirationAlert_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateExpirationAlert.Command
        {
            DocumentId = Guid.NewGuid(),
            AlertDate = DateTime.UtcNow.AddDays(7)
        };

        var createdAlert = new ExpirationAlertDto
        {
            ExpirationAlertId = Guid.NewGuid(),
            DocumentId = command.DocumentId,
            AlertDate = command.AlertDate,
            IsAcknowledged = false,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdAlert);

        // Act
        var result = await _controller.CreateExpirationAlert(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdAlert));
    }

    [Test]
    public async Task AcknowledgeExpirationAlert_ReturnsOkResult_WhenAlertExists()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var acknowledgedAlert = new ExpirationAlertDto
        {
            ExpirationAlertId = alertId,
            DocumentId = Guid.NewGuid(),
            AlertDate = DateTime.UtcNow.AddDays(7),
            IsAcknowledged = true,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AcknowledgeExpirationAlert.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(acknowledgedAlert);

        // Act
        var result = await _controller.AcknowledgeExpirationAlert(alertId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(acknowledgedAlert));
    }

    [Test]
    public async Task AcknowledgeExpirationAlert_ReturnsNotFound_WhenAlertDoesNotExist()
    {
        // Arrange
        var alertId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AcknowledgeExpirationAlert.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ExpirationAlertDto?)null);

        // Act
        var result = await _controller.AcknowledgeExpirationAlert(alertId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task DeleteExpirationAlert_ReturnsNoContent_WhenAlertExists()
    {
        // Arrange
        var alertId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteExpirationAlert.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteExpirationAlert(alertId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteExpirationAlert_ReturnsNotFound_WhenAlertDoesNotExist()
    {
        // Arrange
        var alertId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteExpirationAlert.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteExpirationAlert(alertId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
