// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Api.Controllers;
using CharitableGivingTracker.Api.Features.TaxReports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CharitableGivingTracker.Api.Tests;

public class TaxReportsControllerTests
{
    private Mock<IMediator> _mockMediator = null!;
    private Mock<ILogger<TaxReportsController>> _mockLogger = null!;
    private TaxReportsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _mockLogger = new Mock<ILogger<TaxReportsController>>();
        _controller = new TaxReportsController(_mockMediator.Object, _mockLogger.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithTaxReports()
    {
        // Arrange
        var taxReports = new List<TaxReportDto>
        {
            new TaxReportDto { TaxReportId = Guid.NewGuid(), TaxYear = 2024, TotalDeductibleAmount = 5000 }
        };
        _mockMediator.Setup(m => m.Send(It.IsAny<GetAllTaxReports.Query>(), default))
            .ReturnsAsync(taxReports);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(taxReports));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var taxReportId = Guid.NewGuid();
        var taxReport = new TaxReportDto { TaxReportId = taxReportId, TaxYear = 2024 };
        _mockMediator.Setup(m => m.Send(It.Is<GetTaxReportById.Query>(q => q.TaxReportId == taxReportId), default))
            .ReturnsAsync(taxReport);

        // Act
        var result = await _controller.GetById(taxReportId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(taxReport));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var taxReportId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<GetTaxReportById.Query>(q => q.TaxReportId == taxReportId), default))
            .ReturnsAsync((TaxReportDto?)null);

        // Act
        var result = await _controller.GetById(taxReportId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task GetByYear_WithValidYear_ReturnsOkResult()
    {
        // Arrange
        var year = 2024;
        var taxReport = new TaxReportDto { TaxReportId = Guid.NewGuid(), TaxYear = year };
        _mockMediator.Setup(m => m.Send(It.Is<GetTaxReportByYear.Query>(q => q.TaxYear == year), default))
            .ReturnsAsync(taxReport);

        // Act
        var result = await _controller.GetByYear(year);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(taxReport));
    }

    [Test]
    public async Task GetByYear_WithInvalidYear_ReturnsNotFound()
    {
        // Arrange
        var year = 2024;
        _mockMediator.Setup(m => m.Send(It.Is<GetTaxReportByYear.Query>(q => q.TaxYear == year), default))
            .ReturnsAsync((TaxReportDto?)null);

        // Act
        var result = await _controller.GetByYear(year);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateTaxReport.Command
        {
            TaxYear = 2024,
            TotalCashDonations = 3000,
            TotalNonCashDonations = 2000
        };
        var createdTaxReport = new TaxReportDto
        {
            TaxReportId = Guid.NewGuid(),
            TaxYear = 2024,
            TotalDeductibleAmount = 5000
        };
        _mockMediator.Setup(m => m.Send(command, default))
            .ReturnsAsync(createdTaxReport);

        // Act
        var result = await _controller.Create(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdTaxReport));
    }

    [Test]
    public async Task Update_WithMatchingIds_ReturnsOkResult()
    {
        // Arrange
        var taxReportId = Guid.NewGuid();
        var command = new UpdateTaxReport.Command
        {
            TaxReportId = taxReportId,
            TaxYear = 2024,
            TotalCashDonations = 3500,
            TotalNonCashDonations = 2500
        };
        var updatedTaxReport = new TaxReportDto
        {
            TaxReportId = taxReportId,
            TaxYear = 2024,
            TotalDeductibleAmount = 6000
        };
        _mockMediator.Setup(m => m.Send(command, default))
            .ReturnsAsync(updatedTaxReport);

        // Act
        var result = await _controller.Update(taxReportId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(updatedTaxReport));
    }

    [Test]
    public async Task Update_WithMismatchedIds_ReturnsBadRequest()
    {
        // Arrange
        var routeId = Guid.NewGuid();
        var commandId = Guid.NewGuid();
        var command = new UpdateTaxReport.Command
        {
            TaxReportId = commandId,
            TaxYear = 2024
        };

        // Act
        var result = await _controller.Update(routeId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var taxReportId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<DeleteTaxReport.Command>(c => c.TaxReportId == taxReportId), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(taxReportId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var taxReportId = Guid.NewGuid();
        _mockMediator.Setup(m => m.Send(It.Is<DeleteTaxReport.Command>(c => c.TaxReportId == taxReportId), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(taxReportId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
