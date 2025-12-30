// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Api.Features.Receipts;
using TaxDeductionOrganizer.Core;
using TaxDeductionOrganizer.Infrastructure;

namespace TaxDeductionOrganizer.Api.Tests;

[TestFixture]
public class ReceiptQueryTests
{
    private DbContextOptions<TaxDeductionOrganizerContext> _options = null!;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<TaxDeductionOrganizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Test]
    public async Task GetAllReceipts_ShouldReturnAllReceipts()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var deductionId = Guid.NewGuid();
        context.Receipts.AddRange(
            new Receipt
            {
                ReceiptId = Guid.NewGuid(),
                DeductionId = deductionId,
                FileName = "receipt1.pdf",
                FileUrl = "https://example.com/receipt1.pdf",
                UploadDate = DateTime.UtcNow.AddDays(-1)
            },
            new Receipt
            {
                ReceiptId = Guid.NewGuid(),
                DeductionId = deductionId,
                FileName = "receipt2.pdf",
                FileUrl = "https://example.com/receipt2.pdf",
                UploadDate = DateTime.UtcNow
            }
        );
        await context.SaveChangesAsync();

        var handler = new GetAllReceipts.Handler(context);
        var query = new GetAllReceipts.Query();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetAllReceipts_FilteredByDeduction_ShouldReturnFilteredReceipts()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var deductionId1 = Guid.NewGuid();
        var deductionId2 = Guid.NewGuid();
        context.Receipts.AddRange(
            new Receipt
            {
                ReceiptId = Guid.NewGuid(),
                DeductionId = deductionId1,
                FileName = "receipt1.pdf",
                FileUrl = "https://example.com/receipt1.pdf",
                UploadDate = DateTime.UtcNow
            },
            new Receipt
            {
                ReceiptId = Guid.NewGuid(),
                DeductionId = deductionId2,
                FileName = "receipt2.pdf",
                FileUrl = "https://example.com/receipt2.pdf",
                UploadDate = DateTime.UtcNow
            }
        );
        await context.SaveChangesAsync();

        var handler = new GetAllReceipts.Handler(context);
        var query = new GetAllReceipts.Query { DeductionId = deductionId1 };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].DeductionId, Is.EqualTo(deductionId1));
    }

    [Test]
    public async Task GetReceiptById_ShouldReturnReceipt()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = "test-receipt.pdf",
            FileUrl = "https://example.com/test-receipt.pdf",
            UploadDate = DateTime.UtcNow,
            Notes = "Test notes"
        };
        context.Receipts.Add(receipt);
        await context.SaveChangesAsync();

        var handler = new GetReceiptById.Handler(context);
        var query = new GetReceiptById.Query { ReceiptId = receipt.ReceiptId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ReceiptId, Is.EqualTo(receipt.ReceiptId));
        Assert.That(result.FileName, Is.EqualTo("test-receipt.pdf"));
        Assert.That(result.FileUrl, Is.EqualTo("https://example.com/test-receipt.pdf"));
    }

    [Test]
    public async Task GetReceiptById_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new GetReceiptById.Handler(context);
        var query = new GetReceiptById.Query { ReceiptId = Guid.NewGuid() };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(query, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }
}
