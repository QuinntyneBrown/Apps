// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Api.Controllers;
using CryptoPortfolioManager.Api.Features.TaxLots;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CryptoPortfolioManager.Api.Tests.Controllers;

[TestFixture]
public class TaxLotsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private TaxLotsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TaxLotsController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetTaxLots_ReturnsOkResultWithTaxLots()
    {
        // Arrange
        var taxLots = new List<TaxLotDto>
        {
            new TaxLotDto { TaxLotId = Guid.NewGuid(), Quantity = 1.5m }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTaxLotsQuery>(), default))
            .ReturnsAsync(taxLots);

        // Act
        var result = await _controller.GetTaxLots(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(taxLots));
    }

    [Test]
    public async Task GetTaxLot_WithValidId_ReturnsOkResultWithTaxLot()
    {
        // Arrange
        var taxLotId = Guid.NewGuid();
        var taxLot = new TaxLotDto { TaxLotId = taxLotId, Quantity = 1.5m };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTaxLotByIdQuery>(), default))
            .ReturnsAsync(taxLot);

        // Act
        var result = await _controller.GetTaxLot(taxLotId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(taxLot));
    }

    [Test]
    public async Task GetTaxLot_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTaxLotByIdQuery>(), default))
            .ReturnsAsync((TaxLotDto?)null);

        // Act
        var result = await _controller.GetTaxLot(Guid.NewGuid());

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateTaxLot_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateTaxLotCommand { Quantity = 1.5m };
        var taxLot = new TaxLotDto { TaxLotId = Guid.NewGuid(), Quantity = 1.5m };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(taxLot);

        // Act
        var result = await _controller.CreateTaxLot(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(taxLot));
    }

    [Test]
    public async Task UpdateTaxLot_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var taxLotId = Guid.NewGuid();
        var command = new UpdateTaxLotCommand { Quantity = 2.0m };
        var taxLot = new TaxLotDto { TaxLotId = taxLotId, Quantity = 2.0m };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTaxLotCommand>(), default))
            .ReturnsAsync(taxLot);

        // Act
        var result = await _controller.UpdateTaxLot(taxLotId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task UpdateTaxLot_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var command = new UpdateTaxLotCommand { Quantity = 2.0m };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTaxLotCommand>(), default))
            .ReturnsAsync((TaxLotDto?)null);

        // Act
        var result = await _controller.UpdateTaxLot(Guid.NewGuid(), command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task DeleteTaxLot_WithValidId_ReturnsNoContent()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTaxLotCommand>(), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteTaxLot(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteTaxLot_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTaxLotCommand>(), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteTaxLot(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
