// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Api.Controllers;
using CryptoPortfolioManager.Api.Features.Transactions;
using CryptoPortfolioManager.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CryptoPortfolioManager.Api.Tests.Controllers;

[TestFixture]
public class TransactionsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private TransactionsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TransactionsController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetTransactions_ReturnsOkResultWithTransactions()
    {
        // Arrange
        var transactions = new List<TransactionDto>
        {
            new TransactionDto { TransactionId = Guid.NewGuid(), Symbol = "BTC", TransactionType = TransactionType.Buy }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTransactionsQuery>(), default))
            .ReturnsAsync(transactions);

        // Act
        var result = await _controller.GetTransactions(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(transactions));
    }

    [Test]
    public async Task GetTransaction_WithValidId_ReturnsOkResultWithTransaction()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var transaction = new TransactionDto { TransactionId = transactionId, Symbol = "BTC" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTransactionByIdQuery>(), default))
            .ReturnsAsync(transaction);

        // Act
        var result = await _controller.GetTransaction(transactionId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(transaction));
    }

    [Test]
    public async Task GetTransaction_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTransactionByIdQuery>(), default))
            .ReturnsAsync((TransactionDto?)null);

        // Act
        var result = await _controller.GetTransaction(Guid.NewGuid());

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateTransaction_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateTransactionCommand { Symbol = "BTC", TransactionType = TransactionType.Buy };
        var transaction = new TransactionDto { TransactionId = Guid.NewGuid(), Symbol = "BTC" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(transaction);

        // Act
        var result = await _controller.CreateTransaction(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(transaction));
    }

    [Test]
    public async Task UpdateTransaction_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var command = new UpdateTransactionCommand { Symbol = "ETH" };
        var transaction = new TransactionDto { TransactionId = transactionId, Symbol = "ETH" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTransactionCommand>(), default))
            .ReturnsAsync(transaction);

        // Act
        var result = await _controller.UpdateTransaction(transactionId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task UpdateTransaction_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var command = new UpdateTransactionCommand { Symbol = "ETH" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTransactionCommand>(), default))
            .ReturnsAsync((TransactionDto?)null);

        // Act
        var result = await _controller.UpdateTransaction(Guid.NewGuid(), command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task DeleteTransaction_WithValidId_ReturnsNoContent()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTransactionCommand>(), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteTransaction(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteTransaction_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTransactionCommand>(), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteTransaction(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
