// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Api.Controllers;
using BillPaymentScheduler.Api.Features.Bills;
using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BillPaymentScheduler.Api.Tests.Controllers;

[TestFixture]
public class BillsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<BillsController>> _loggerMock;
    private BillsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<BillsController>>();
        _controller = new BillsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetBills_ReturnsOkWithBills()
    {
        // Arrange
        var bills = new List<BillDto>
        {
            new BillDto
            {
                BillId = Guid.NewGuid(),
                Name = "Electric Bill",
                Amount = 150.00m,
                DueDate = DateTime.Now.AddDays(5),
                BillingFrequency = BillingFrequency.Monthly,
                Status = BillStatus.Pending
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetBills.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bills);

        // Act
        var result = await _controller.GetBills(CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(bills, okResult.Value);
    }

    [Test]
    public async Task GetBillById_WithValidId_ReturnsOkWithBill()
    {
        // Arrange
        var billId = Guid.NewGuid();
        var bill = new BillDto
        {
            BillId = billId,
            Name = "Water Bill",
            Amount = 75.50m,
            DueDate = DateTime.Now.AddDays(10),
            BillingFrequency = BillingFrequency.Monthly,
            Status = BillStatus.Pending
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetBillById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bill);

        // Act
        var result = await _controller.GetBillById(billId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(bill, okResult.Value);
    }

    [Test]
    public async Task GetBillById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var billId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetBillById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((BillDto?)null);

        // Act
        var result = await _controller.GetBillById(billId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreateBill_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateBill.Command
        {
            Name = "Gas Bill",
            Amount = 100.00m,
            DueDate = DateTime.Now.AddDays(15),
            BillingFrequency = BillingFrequency.Monthly,
            Status = BillStatus.Pending,
            PayeeId = Guid.NewGuid()
        };

        var createdBill = new BillDto
        {
            BillId = Guid.NewGuid(),
            Name = command.Name,
            Amount = command.Amount,
            DueDate = command.DueDate,
            BillingFrequency = command.BillingFrequency,
            Status = command.Status,
            PayeeId = command.PayeeId
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdBill);

        // Act
        var result = await _controller.CreateBill(command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(createdBill, createdResult.Value);
    }

    [Test]
    public async Task UpdateBill_WithValidId_ReturnsOkWithUpdatedBill()
    {
        // Arrange
        var billId = Guid.NewGuid();
        var command = new UpdateBill.Command
        {
            BillId = billId,
            Name = "Updated Bill",
            Amount = 200.00m,
            DueDate = DateTime.Now.AddDays(20),
            BillingFrequency = BillingFrequency.Quarterly,
            Status = BillStatus.Paid,
            PayeeId = Guid.NewGuid()
        };

        var updatedBill = new BillDto
        {
            BillId = billId,
            Name = command.Name,
            Amount = command.Amount,
            DueDate = command.DueDate,
            BillingFrequency = command.BillingFrequency,
            Status = command.Status,
            PayeeId = command.PayeeId
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedBill);

        // Act
        var result = await _controller.UpdateBill(billId, command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(updatedBill, okResult.Value);
    }

    [Test]
    public async Task UpdateBill_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var urlId = Guid.NewGuid();
        var command = new UpdateBill.Command { BillId = Guid.NewGuid() };

        // Act
        var result = await _controller.UpdateBill(urlId, command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task DeleteBill_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var billId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteBill.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteBill(billId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }

    [Test]
    public async Task DeleteBill_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var billId = Guid.NewGuid();
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteBill.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteBill(billId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}
