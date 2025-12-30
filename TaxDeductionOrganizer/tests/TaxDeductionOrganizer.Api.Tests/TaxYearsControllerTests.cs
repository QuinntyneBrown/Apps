// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TaxDeductionOrganizer.Api.Controllers;
using TaxDeductionOrganizer.Api.Features.TaxYears;

namespace TaxDeductionOrganizer.Api.Tests;

[TestFixture]
public class TaxYearsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<TaxYearsController>> _loggerMock = null!;
    private TaxYearsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<TaxYearsController>>();
        _controller = new TaxYearsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ShouldReturnOkResultWithTaxYears()
    {
        // Arrange
        var expectedTaxYears = new List<TaxYearDto>
        {
            new TaxYearDto { TaxYearId = Guid.NewGuid(), Year = 2024 },
            new TaxYearDto { TaxYearId = Guid.NewGuid(), Year = 2023 }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTaxYears.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTaxYears);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(expectedTaxYears));
    }

    [Test]
    public async Task GetById_WithValidId_ShouldReturnOkResult()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        var expectedTaxYear = new TaxYearDto { TaxYearId = taxYearId, Year = 2024 };
        _mediatorMock.Setup(m => m.Send(It.Is<GetTaxYearById.Query>(q => q.TaxYearId == taxYearId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTaxYear);

        // Act
        var result = await _controller.GetById(taxYearId);

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(expectedTaxYear));
    }

    [Test]
    public async Task GetById_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetTaxYearById.Query>(q => q.TaxYearId == taxYearId), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException($"TaxYear with ID {taxYearId} not found."));

        // Act
        var result = await _controller.GetById(taxYearId);

        // Assert
        Assert.That(result.Result, Is.TypeOf<NotFoundObjectResult>());
    }

    [Test]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var command = new CreateTaxYear.Command { Year = 2024, Notes = "Test" };
        var expectedTaxYear = new TaxYearDto { TaxYearId = Guid.NewGuid(), Year = 2024 };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTaxYear);

        // Act
        var result = await _controller.Create(command);

        // Assert
        Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(expectedTaxYear));
        Assert.That(createdResult.ActionName, Is.EqualTo(nameof(TaxYearsController.GetById)));
    }

    [Test]
    public async Task Update_WithMatchingIds_ShouldReturnOkResult()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        var command = new UpdateTaxYear.Command
        {
            TaxYearId = taxYearId,
            Year = 2024,
            IsFiled = true
        };
        var expectedTaxYear = new TaxYearDto { TaxYearId = taxYearId, Year = 2024, IsFiled = true };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTaxYear);

        // Act
        var result = await _controller.Update(taxYearId, command);

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(expectedTaxYear));
    }

    [Test]
    public async Task Update_WithMismatchedIds_ShouldReturnBadRequest()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        var command = new UpdateTaxYear.Command
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
        };

        // Act
        var result = await _controller.Update(taxYearId, command);

        // Assert
        Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_WithValidId_ShouldReturnNoContent()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteTaxYear.Command>(c => c.TaxYearId == taxYearId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Delete(taxYearId);

        // Assert
        Assert.That(result, Is.TypeOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteTaxYear.Command>(c => c.TaxYearId == taxYearId), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException($"TaxYear with ID {taxYearId} not found."));

        // Act
        var result = await _controller.Delete(taxYearId);

        // Assert
        Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
    }
}
