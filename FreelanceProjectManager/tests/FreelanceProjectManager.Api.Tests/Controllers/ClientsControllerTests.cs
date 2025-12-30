// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Api.Controllers;
using FreelanceProjectManager.Api.Features.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FreelanceProjectManager.Api.Tests.Controllers;

/// <summary>
/// Tests for the ClientsController.
/// </summary>
[TestFixture]
public class ClientsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private ClientsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ClientsController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetClients_ReturnsOkResult_WithListOfClients()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var clients = new List<ClientDto>
        {
            new ClientDto { ClientId = Guid.NewGuid(), UserId = userId, Name = "Client 1" },
            new ClientDto { ClientId = Guid.NewGuid(), UserId = userId, Name = "Client 2" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetClientsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(clients);

        // Act
        var result = await _controller.GetClients(userId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(clients));
    }

    [Test]
    public async Task GetClient_ReturnsOkResult_WhenClientExists()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var client = new ClientDto { ClientId = clientId, UserId = userId, Name = "Test Client" };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetClientByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        // Act
        var result = await _controller.GetClient(clientId, userId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(client));
    }

    [Test]
    public async Task GetClient_ReturnsNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetClientByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ClientDto?)null);

        // Act
        var result = await _controller.GetClient(clientId, userId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateClient_ReturnsCreatedResult_WithClient()
    {
        // Arrange
        var command = new CreateClientCommand { UserId = Guid.NewGuid(), Name = "New Client" };
        var createdClient = new ClientDto { ClientId = Guid.NewGuid(), UserId = command.UserId, Name = command.Name };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateClientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdClient);

        // Act
        var result = await _controller.CreateClient(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdClient));
    }

    [Test]
    public async Task UpdateClient_ReturnsOkResult_WhenClientExists()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var command = new UpdateClientCommand { ClientId = clientId, UserId = Guid.NewGuid(), Name = "Updated Client" };
        var updatedClient = new ClientDto { ClientId = clientId, UserId = command.UserId, Name = command.Name };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateClientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedClient);

        // Act
        var result = await _controller.UpdateClient(clientId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedClient));
    }

    [Test]
    public async Task UpdateClient_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var command = new UpdateClientCommand { ClientId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Updated Client" };

        // Act
        var result = await _controller.UpdateClient(clientId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeleteClient_ReturnsNoContent_WhenClientExists()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteClientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteClient(clientId, userId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteClient_ReturnsNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteClientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteClient(clientId, userId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
