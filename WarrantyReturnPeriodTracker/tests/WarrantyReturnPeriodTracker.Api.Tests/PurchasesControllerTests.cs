using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarrantyReturnPeriodTracker.Api.Controllers;
using WarrantyReturnPeriodTracker.Api.Features.Purchases;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Tests;

[TestFixture]
public class PurchasesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<PurchasesController>> _loggerMock;
    private PurchasesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<PurchasesController>>();
        _controller = new PurchasesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetPurchases_ReturnsOkWithPurchases()
    {
        // Arrange
        var expectedPurchases = new List<PurchaseDto>
        {
            new PurchaseDto
            {
                PurchaseId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ProductName = "Test Product",
                Category = ProductCategory.Electronics,
                StoreName = "Test Store",
                PurchaseDate = DateTime.UtcNow,
                Price = 99.99m,
                Status = PurchaseStatus.Active
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetPurchasesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPurchases);

        // Act
        var result = await _controller.GetPurchases(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(expectedPurchases));
    }

    [Test]
    public async Task GetPurchaseById_ExistingId_ReturnsOkWithPurchase()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();
        var expectedPurchase = new PurchaseDto
        {
            PurchaseId = purchaseId,
            UserId = Guid.NewGuid(),
            ProductName = "Test Product",
            Category = ProductCategory.Electronics,
            StoreName = "Test Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 99.99m,
            Status = PurchaseStatus.Active
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetPurchaseByIdQuery>(q => q.PurchaseId == purchaseId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPurchase);

        // Act
        var result = await _controller.GetPurchaseById(purchaseId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(expectedPurchase));
    }

    [Test]
    public async Task GetPurchaseById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetPurchaseByIdQuery>(q => q.PurchaseId == purchaseId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PurchaseDto?)null);

        // Act
        var result = await _controller.GetPurchaseById(purchaseId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreatePurchase_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreatePurchaseCommand
        {
            UserId = Guid.NewGuid(),
            ProductName = "New Product",
            Category = ProductCategory.Electronics,
            StoreName = "Test Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 199.99m
        };

        var createdPurchase = new PurchaseDto
        {
            PurchaseId = Guid.NewGuid(),
            UserId = command.UserId,
            ProductName = command.ProductName,
            Category = command.Category,
            StoreName = command.StoreName,
            PurchaseDate = command.PurchaseDate,
            Price = command.Price,
            Status = PurchaseStatus.Active
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdPurchase);

        // Act
        var result = await _controller.CreatePurchase(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdPurchase));
    }

    [Test]
    public async Task UpdatePurchase_ValidCommand_ReturnsOkWithUpdatedPurchase()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();
        var command = new UpdatePurchaseCommand
        {
            PurchaseId = purchaseId,
            ProductName = "Updated Product",
            Category = ProductCategory.Electronics,
            StoreName = "Test Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 299.99m,
            Status = PurchaseStatus.Active
        };

        var updatedPurchase = new PurchaseDto
        {
            PurchaseId = purchaseId,
            UserId = Guid.NewGuid(),
            ProductName = command.ProductName,
            Category = command.Category,
            StoreName = command.StoreName,
            PurchaseDate = command.PurchaseDate,
            Price = command.Price,
            Status = command.Status
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedPurchase);

        // Act
        var result = await _controller.UpdatePurchase(purchaseId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(updatedPurchase));
    }

    [Test]
    public async Task UpdatePurchase_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();
        var command = new UpdatePurchaseCommand
        {
            PurchaseId = Guid.NewGuid(), // Different ID
            ProductName = "Updated Product",
            Category = ProductCategory.Electronics,
            StoreName = "Test Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 299.99m,
            Status = PurchaseStatus.Active
        };

        // Act
        var result = await _controller.UpdatePurchase(purchaseId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeletePurchase_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeletePurchaseCommand>(c => c.PurchaseId == purchaseId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeletePurchase(purchaseId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeletePurchase_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeletePurchaseCommand>(c => c.PurchaseId == purchaseId), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Purchase not found"));

        // Act
        var result = await _controller.DeletePurchase(purchaseId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }
}
