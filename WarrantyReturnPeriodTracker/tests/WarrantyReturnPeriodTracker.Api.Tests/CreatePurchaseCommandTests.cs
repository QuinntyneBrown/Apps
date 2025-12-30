using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Api.Features.Purchases;
using WarrantyReturnPeriodTracker.Core;
using WarrantyReturnPeriodTracker.Infrastructure;

namespace WarrantyReturnPeriodTracker.Api.Tests;

[TestFixture]
public class CreatePurchaseCommandTests
{
    private WarrantyReturnPeriodTrackerContext _context;
    private CreatePurchaseCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WarrantyReturnPeriodTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WarrantyReturnPeriodTrackerContext(options);
        _handler = new CreatePurchaseCommandHandler(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesPurchase()
    {
        // Arrange
        var command = new CreatePurchaseCommand
        {
            UserId = Guid.NewGuid(),
            ProductName = "Test Product",
            Category = ProductCategory.Electronics,
            StoreName = "Test Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 99.99m,
            ModelNumber = "MODEL123",
            Notes = "Test notes"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ProductName, Is.EqualTo(command.ProductName));
        Assert.That(result.Category, Is.EqualTo(command.Category));
        Assert.That(result.StoreName, Is.EqualTo(command.StoreName));
        Assert.That(result.Price, Is.EqualTo(command.Price));
        Assert.That(result.Status, Is.EqualTo(PurchaseStatus.Active));

        var savedPurchase = await _context.Purchases.FirstOrDefaultAsync();
        Assert.That(savedPurchase, Is.Not.Null);
        Assert.That(savedPurchase!.ProductName, Is.EqualTo(command.ProductName));
    }

    [Test]
    public async Task Handle_ValidCommand_SetsDefaultValues()
    {
        // Arrange
        var command = new CreatePurchaseCommand
        {
            UserId = Guid.NewGuid(),
            ProductName = "Test Product",
            Category = ProductCategory.Electronics,
            StoreName = "Test Store",
            PurchaseDate = DateTime.UtcNow,
            Price = 99.99m
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.PurchaseId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(result.Status, Is.EqualTo(PurchaseStatus.Active));
        Assert.That(result.CreatedAt, Is.Not.EqualTo(DateTime.MinValue));
    }
}
