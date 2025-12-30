// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Api.Features.Receipts;
using TaxDeductionOrganizer.Core;
using TaxDeductionOrganizer.Infrastructure;

namespace TaxDeductionOrganizer.Api.Tests;

[TestFixture]
public class ReceiptCommandTests
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
    public async Task CreateReceipt_ShouldCreateReceipt()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Test deduction",
            Amount = 100m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Other,
            HasReceipt = false
        };
        context.Deductions.Add(deduction);
        await context.SaveChangesAsync();

        var handler = new CreateReceipt.Handler(context);
        var command = new CreateReceipt.Command
        {
            DeductionId = deduction.DeductionId,
            FileName = "receipt.pdf",
            FileUrl = "https://example.com/receipt.pdf",
            Notes = "Test receipt"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FileName, Is.EqualTo("receipt.pdf"));
        Assert.That(result.FileUrl, Is.EqualTo("https://example.com/receipt.pdf"));
        Assert.That(result.Notes, Is.EqualTo("Test receipt"));

        var savedReceipt = await context.Receipts.FindAsync(result.ReceiptId);
        Assert.That(savedReceipt, Is.Not.Null);

        // Verify deduction was updated
        var updatedDeduction = await context.Deductions.FindAsync(deduction.DeductionId);
        Assert.That(updatedDeduction!.HasReceipt, Is.True);
    }

    [Test]
    public async Task UpdateReceipt_ShouldUpdateReceipt()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = "original.pdf",
            FileUrl = "https://example.com/original.pdf",
            UploadDate = DateTime.UtcNow
        };
        context.Receipts.Add(receipt);
        await context.SaveChangesAsync();

        var handler = new UpdateReceipt.Handler(context);
        var command = new UpdateReceipt.Command
        {
            ReceiptId = receipt.ReceiptId,
            FileName = "updated.pdf",
            FileUrl = "https://example.com/updated.pdf",
            Notes = "Updated notes"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FileName, Is.EqualTo("updated.pdf"));
        Assert.That(result.FileUrl, Is.EqualTo("https://example.com/updated.pdf"));
        Assert.That(result.Notes, Is.EqualTo("Updated notes"));
    }

    [Test]
    public async Task UpdateReceipt_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new UpdateReceipt.Handler(context);
        var command = new UpdateReceipt.Command
        {
            ReceiptId = Guid.NewGuid(),
            FileName = "test.pdf",
            FileUrl = "https://example.com/test.pdf"
        };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }

    [Test]
    public async Task DeleteReceipt_ShouldDeleteReceipt()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = "test.pdf",
            FileUrl = "https://example.com/test.pdf",
            UploadDate = DateTime.UtcNow
        };
        context.Receipts.Add(receipt);
        await context.SaveChangesAsync();

        var handler = new DeleteReceipt.Handler(context);
        var command = new DeleteReceipt.Command { ReceiptId = receipt.ReceiptId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedReceipt = await context.Receipts.FindAsync(receipt.ReceiptId);
        Assert.That(deletedReceipt, Is.Null);
    }

    [Test]
    public async Task DeleteReceipt_WithInvalidId_ShouldThrowException()
    {
        // Arrange
        await using var context = new TaxDeductionOrganizerContext(_options);
        var handler = new DeleteReceipt.Handler(context);
        var command = new DeleteReceipt.Command { ReceiptId = Guid.NewGuid() };

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidOperationException>(
            async () => await handler.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain("not found"));
    }
}
