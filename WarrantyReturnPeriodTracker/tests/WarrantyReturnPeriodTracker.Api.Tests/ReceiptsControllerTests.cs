using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarrantyReturnPeriodTracker.Api.Controllers;
using WarrantyReturnPeriodTracker.Api.Features.Receipts;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Tests;

[TestFixture]
public class ReceiptsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ReceiptsController>> _loggerMock;
    private ReceiptsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ReceiptsController>>();
        _controller = new ReceiptsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetReceipts_ReturnsOkWithReceipts()
    {
        // Arrange
        var expectedReceipts = new List<ReceiptDto>
        {
            new ReceiptDto
            {
                ReceiptId = Guid.NewGuid(),
                PurchaseId = Guid.NewGuid(),
                ReceiptNumber = "REC-001",
                ReceiptType = ReceiptType.Purchase,
                Format = ReceiptFormat.Pdf,
                StoreName = "Test Store",
                TotalAmount = 99.99m,
                PaymentMethod = PaymentMethod.CreditCard,
                Status = ReceiptStatus.Active,
                ReceiptDate = DateTime.UtcNow
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetReceiptsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedReceipts);

        // Act
        var result = await _controller.GetReceipts(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(expectedReceipts));
    }

    [Test]
    public async Task CreateReceipt_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateReceiptCommand
        {
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-002",
            ReceiptType = ReceiptType.Purchase,
            Format = ReceiptFormat.Digital,
            StoreName = "Online Store",
            TotalAmount = 149.99m,
            PaymentMethod = PaymentMethod.PayPal,
            ReceiptDate = DateTime.UtcNow
        };

        var createdReceipt = new ReceiptDto
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = command.PurchaseId,
            ReceiptNumber = command.ReceiptNumber,
            ReceiptType = command.ReceiptType,
            Format = command.Format,
            StoreName = command.StoreName,
            TotalAmount = command.TotalAmount,
            PaymentMethod = command.PaymentMethod,
            Status = ReceiptStatus.Active,
            IsVerified = false,
            ReceiptDate = command.ReceiptDate,
            UploadedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdReceipt);

        // Act
        var result = await _controller.CreateReceipt(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdReceipt));
    }
}
