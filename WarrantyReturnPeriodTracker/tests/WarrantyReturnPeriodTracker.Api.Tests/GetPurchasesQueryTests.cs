using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Api.Features.Purchases;
using WarrantyReturnPeriodTracker.Core;
using WarrantyReturnPeriodTracker.Infrastructure;

namespace WarrantyReturnPeriodTracker.Api.Tests;

[TestFixture]
public class GetPurchasesQueryTests
{
    private WarrantyReturnPeriodTrackerContext _context;
    private GetPurchasesQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WarrantyReturnPeriodTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WarrantyReturnPeriodTrackerContext(options);
        _handler = new GetPurchasesQueryHandler(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task Handle_ReturnsAllPurchases()
    {
        // Arrange
        var purchase1 = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Product 1",
            Category = ProductCategory.Electronics,
            StoreName = "Store 1",
            PurchaseDate = DateTime.UtcNow,
            Price = 99.99m,
            Status = PurchaseStatus.Active,
            CreatedAt = DateTime.UtcNow.AddDays(-2)
        };

        var purchase2 = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Product 2",
            Category = ProductCategory.Appliances,
            StoreName = "Store 2",
            PurchaseDate = DateTime.UtcNow,
            Price = 199.99m,
            Status = PurchaseStatus.Active,
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };

        _context.Purchases.AddRange(purchase1, purchase2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _handler.Handle(new GetPurchasesQuery(), CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].ProductName, Is.EqualTo("Product 2")); // Most recent first
        Assert.That(result[1].ProductName, Is.EqualTo("Product 1"));
    }

    [Test]
    public async Task Handle_NoPurchases_ReturnsEmptyList()
    {
        // Act
        var result = await _handler.Handle(new GetPurchasesQuery(), CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(0));
    }
}
