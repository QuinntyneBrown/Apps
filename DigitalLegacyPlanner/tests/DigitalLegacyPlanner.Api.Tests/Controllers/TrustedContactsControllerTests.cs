// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Api.Controllers;
using DigitalLegacyPlanner.Api.Features.TrustedContacts;
using DigitalLegacyPlanner.Api.Features.TrustedContacts.Commands;
using DigitalLegacyPlanner.Api.Features.TrustedContacts.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DigitalLegacyPlanner.Api.Tests.Controllers;

[TestFixture]
public class TrustedContactsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<TrustedContactsController>> _loggerMock;
    private TrustedContactsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<TrustedContactsController>>();
        _controller = new TrustedContactsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetTrustedContacts_ReturnsOkWithContacts()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var contacts = new List<TrustedContactDto>
        {
            new TrustedContactDto
            {
                TrustedContactId = Guid.NewGuid(),
                UserId = userId,
                FullName = "John Doe",
                Email = "john@example.com",
                Relationship = "Brother"
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetTrustedContactsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(contacts);

        // Act
        var result = await _controller.GetTrustedContacts(userId, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(contacts);
    }

    [Test]
    public async Task GetTrustedContactById_WhenExists_ReturnsOkWithContact()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var contact = new TrustedContactDto
        {
            TrustedContactId = contactId,
            UserId = Guid.NewGuid(),
            FullName = "John Doe",
            Email = "john@example.com",
            Relationship = "Brother"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetTrustedContactByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(contact);

        // Act
        var result = await _controller.GetTrustedContactById(contactId, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(contact);
    }

    [Test]
    public async Task GetTrustedContactById_WhenNotExists_ReturnsNotFound()
    {
        // Arrange
        var contactId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetTrustedContactByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TrustedContactDto?)null);

        // Act
        var result = await _controller.GetTrustedContactById(contactId, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task CreateTrustedContact_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateTrustedContactCommand
        {
            UserId = Guid.NewGuid(),
            FullName = "John Doe",
            Email = "john@example.com",
            Relationship = "Brother"
        };

        var createdContact = new TrustedContactDto
        {
            TrustedContactId = Guid.NewGuid(),
            UserId = command.UserId,
            FullName = command.FullName,
            Email = command.Email,
            Relationship = command.Relationship
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateTrustedContactCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdContact);

        // Act
        var result = await _controller.CreateTrustedContact(command, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult.Value.Should().BeEquivalentTo(createdContact);
    }

    [Test]
    public async Task UpdateTrustedContact_WhenExists_ReturnsOkWithUpdatedContact()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var command = new UpdateTrustedContactCommand
        {
            TrustedContactId = contactId,
            FullName = "John Doe Updated",
            Email = "john.updated@example.com",
            Relationship = "Brother"
        };

        var updatedContact = new TrustedContactDto
        {
            TrustedContactId = contactId,
            UserId = Guid.NewGuid(),
            FullName = command.FullName,
            Email = command.Email,
            Relationship = command.Relationship
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateTrustedContactCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedContact);

        // Act
        var result = await _controller.UpdateTrustedContact(contactId, command, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(updatedContact);
    }

    [Test]
    public async Task UpdateTrustedContact_WhenIdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var command = new UpdateTrustedContactCommand
        {
            TrustedContactId = Guid.NewGuid(),
            FullName = "John Doe",
            Email = "john@example.com",
            Relationship = "Brother"
        };

        // Act
        var result = await _controller.UpdateTrustedContact(contactId, command, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public async Task DeleteTrustedContact_WhenExists_ReturnsNoContent()
    {
        // Arrange
        var contactId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteTrustedContactCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteTrustedContact(contactId, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task DeleteTrustedContact_WhenNotExists_ReturnsNotFound()
    {
        // Arrange
        var contactId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteTrustedContactCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteTrustedContact(contactId, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
