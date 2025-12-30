// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Api.Controllers;
using CollegeSavingsPlanner.Api.Features.Beneficiaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CollegeSavingsPlanner.Api.Tests;

/// <summary>
/// Tests for BeneficiariesController.
/// </summary>
[TestFixture]
public class BeneficiariesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<BeneficiariesController>> _loggerMock;
    private BeneficiariesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<BeneficiariesController>>();
        _controller = new BeneficiariesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetBeneficiaries_ReturnsOkWithListOfBeneficiaries()
    {
        // Arrange
        var beneficiaries = new List<BeneficiaryDto>
        {
            new BeneficiaryDto { BeneficiaryId = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
            new BeneficiaryDto { BeneficiaryId = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetBeneficiariesQuery>(), default))
            .ReturnsAsync(beneficiaries);

        // Act
        var result = await _controller.GetBeneficiaries();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(beneficiaries));
    }

    [Test]
    public async Task GetBeneficiary_WithValidId_ReturnsOkWithBeneficiary()
    {
        // Arrange
        var beneficiaryId = Guid.NewGuid();
        var beneficiary = new BeneficiaryDto { BeneficiaryId = beneficiaryId, FirstName = "John", LastName = "Doe" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetBeneficiaryByIdQuery>(q => q.BeneficiaryId == beneficiaryId), default))
            .ReturnsAsync(beneficiary);

        // Act
        var result = await _controller.GetBeneficiary(beneficiaryId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(beneficiary));
    }

    [Test]
    public async Task GetBeneficiary_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var beneficiaryId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetBeneficiaryByIdQuery>(q => q.BeneficiaryId == beneficiaryId), default))
            .ReturnsAsync((BeneficiaryDto?)null);

        // Act
        var result = await _controller.GetBeneficiary(beneficiaryId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateBeneficiary_ReturnsCreatedAtAction()
    {
        // Arrange
        var createBeneficiaryDto = new CreateBeneficiaryDto { FirstName = "John", LastName = "Doe" };
        var createdBeneficiary = new BeneficiaryDto { BeneficiaryId = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBeneficiaryCommand>(), default))
            .ReturnsAsync(createdBeneficiary);

        // Act
        var result = await _controller.CreateBeneficiary(createBeneficiaryDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdBeneficiary));
    }

    [Test]
    public async Task UpdateBeneficiary_WithValidId_ReturnsOkWithUpdatedBeneficiary()
    {
        // Arrange
        var beneficiaryId = Guid.NewGuid();
        var updateBeneficiaryDto = new UpdateBeneficiaryDto { FirstName = "John", LastName = "Updated" };
        var updatedBeneficiary = new BeneficiaryDto { BeneficiaryId = beneficiaryId, FirstName = "John", LastName = "Updated" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBeneficiaryCommand>(), default))
            .ReturnsAsync(updatedBeneficiary);

        // Act
        var result = await _controller.UpdateBeneficiary(beneficiaryId, updateBeneficiaryDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedBeneficiary));
    }

    [Test]
    public async Task DeleteBeneficiary_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var beneficiaryId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteBeneficiaryCommand>(c => c.BeneficiaryId == beneficiaryId), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteBeneficiary(beneficiaryId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteBeneficiary_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var beneficiaryId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteBeneficiaryCommand>(c => c.BeneficiaryId == beneficiaryId), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteBeneficiary(beneficiaryId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
