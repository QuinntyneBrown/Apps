// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Api.Controllers;
using CryptoPortfolioManager.Api.Features.Wallets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CryptoPortfolioManager.Api.Tests.Controllers;

[TestFixture]
public class WalletsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private WalletsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new WalletsController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetWallets_ReturnsOkResultWithWallets()
    {
        // Arrange
        var wallets = new List<WalletDto>
        {
            new WalletDto { WalletId = Guid.NewGuid(), Name = "Test Wallet" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWalletsQuery>(), default))
            .ReturnsAsync(wallets);

        // Act
        var result = await _controller.GetWallets();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(wallets));
    }

    [Test]
    public async Task GetWallet_WithValidId_ReturnsOkResultWithWallet()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var wallet = new WalletDto { WalletId = walletId, Name = "Test Wallet" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWalletByIdQuery>(), default))
            .ReturnsAsync(wallet);

        // Act
        var result = await _controller.GetWallet(walletId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(wallet));
    }

    [Test]
    public async Task GetWallet_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWalletByIdQuery>(), default))
            .ReturnsAsync((WalletDto?)null);

        // Act
        var result = await _controller.GetWallet(Guid.NewGuid());

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateWallet_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateWalletCommand { Name = "New Wallet" };
        var wallet = new WalletDto { WalletId = Guid.NewGuid(), Name = "New Wallet" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(wallet);

        // Act
        var result = await _controller.CreateWallet(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(wallet));
    }

    [Test]
    public async Task UpdateWallet_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var command = new UpdateWalletCommand { Name = "Updated Wallet" };
        var wallet = new WalletDto { WalletId = walletId, Name = "Updated Wallet" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateWalletCommand>(), default))
            .ReturnsAsync(wallet);

        // Act
        var result = await _controller.UpdateWallet(walletId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task UpdateWallet_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var command = new UpdateWalletCommand { Name = "Updated Wallet" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateWalletCommand>(), default))
            .ReturnsAsync((WalletDto?)null);

        // Act
        var result = await _controller.UpdateWallet(Guid.NewGuid(), command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task DeleteWallet_WithValidId_ReturnsNoContent()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteWalletCommand>(), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteWallet(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteWallet_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteWalletCommand>(), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteWallet(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
