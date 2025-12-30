using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Api.Features.Warranties;
using WarrantyReturnPeriodTracker.Core;
using WarrantyReturnPeriodTracker.Infrastructure;

namespace WarrantyReturnPeriodTracker.Api.Tests;

[TestFixture]
public class CreateWarrantyCommandTests
{
    private WarrantyReturnPeriodTrackerContext _context;
    private CreateWarrantyCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WarrantyReturnPeriodTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WarrantyReturnPeriodTrackerContext(options);
        _handler = new CreateWarrantyCommandHandler(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesWarranty()
    {
        // Arrange
        var command = new CreateWarrantyCommand
        {
            PurchaseId = Guid.NewGuid(),
            WarrantyType = WarrantyType.Extended,
            Provider = "Extended Warranty Co",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(2),
            DurationMonths = 24,
            CoverageDetails = "Full coverage",
            Terms = "Standard terms apply"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Provider, Is.EqualTo(command.Provider));
        Assert.That(result.WarrantyType, Is.EqualTo(command.WarrantyType));
        Assert.That(result.DurationMonths, Is.EqualTo(command.DurationMonths));
        Assert.That(result.Status, Is.EqualTo(WarrantyStatus.Active));

        var savedWarranty = await _context.Warranties.FirstOrDefaultAsync();
        Assert.That(savedWarranty, Is.Not.Null);
        Assert.That(savedWarranty!.Provider, Is.EqualTo(command.Provider));
    }
}
