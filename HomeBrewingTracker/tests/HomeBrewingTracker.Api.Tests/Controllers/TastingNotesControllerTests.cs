// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Api.Controllers;
using HomeBrewingTracker.Api.Features.TastingNotes;
using HomeBrewingTracker.Api.Features.TastingNotes.Commands;
using HomeBrewingTracker.Api.Features.TastingNotes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeBrewingTracker.Api.Tests.Controllers;

/// <summary>
/// Tests for TastingNotesController.
/// </summary>
[TestFixture]
public class TastingNotesControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<TastingNotesController>> _loggerMock = null!;
    private TastingNotesController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<TastingNotesController>>();
        _controller = new TastingNotesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetTastingNotes_ReturnsOkResult_WithListOfTastingNotes()
    {
        // Arrange
        var tastingNotes = new List<TastingNoteDto>
        {
            new() { TastingNoteId = Guid.NewGuid(), Rating = 4, Flavor = "Hoppy" },
            new() { TastingNoteId = Guid.NewGuid(), Rating = 5, Flavor = "Smooth" },
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetTastingNotesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tastingNotes);

        // Act
        var result = await _controller.GetTastingNotes(null, null, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(tastingNotes));
    }

    [Test]
    public async Task GetTastingNote_WithValidId_ReturnsOkResult_WithTastingNote()
    {
        // Arrange
        var tastingNoteId = Guid.NewGuid();
        var tastingNote = new TastingNoteDto { TastingNoteId = tastingNoteId, Rating = 4, Flavor = "Hoppy" };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetTastingNoteByIdQuery>(q => q.TastingNoteId == tastingNoteId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tastingNote);

        // Act
        var result = await _controller.GetTastingNote(tastingNoteId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(tastingNote));
    }

    [Test]
    public async Task GetTastingNote_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var tastingNoteId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetTastingNoteByIdQuery>(q => q.TastingNoteId == tastingNoteId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TastingNoteDto?)null);

        // Act
        var result = await _controller.GetTastingNote(tastingNoteId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateTastingNote_ReturnsCreatedAtAction_WithTastingNote()
    {
        // Arrange
        var command = new CreateTastingNoteCommand
        {
            UserId = Guid.NewGuid(),
            BatchId = Guid.NewGuid(),
            Rating = 4,
            Flavor = "Hoppy",
        };

        var tastingNote = new TastingNoteDto
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = command.UserId,
            BatchId = command.BatchId,
            Rating = command.Rating,
            Flavor = command.Flavor,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateTastingNoteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tastingNote);

        // Act
        var result = await _controller.CreateTastingNote(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(tastingNote));
    }

    [Test]
    public async Task UpdateTastingNote_WithValidId_ReturnsOkResult_WithUpdatedTastingNote()
    {
        // Arrange
        var tastingNoteId = Guid.NewGuid();
        var command = new UpdateTastingNoteCommand
        {
            TastingNoteId = tastingNoteId,
            Rating = 5,
            Flavor = "Updated Flavor",
        };

        var tastingNote = new TastingNoteDto
        {
            TastingNoteId = tastingNoteId,
            Rating = command.Rating,
            Flavor = command.Flavor,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateTastingNoteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tastingNote);

        // Act
        var result = await _controller.UpdateTastingNote(tastingNoteId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(tastingNote));
    }

    [Test]
    public async Task DeleteTastingNote_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var tastingNoteId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteTastingNoteCommand>(c => c.TastingNoteId == tastingNoteId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteTastingNote(tastingNoteId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}
