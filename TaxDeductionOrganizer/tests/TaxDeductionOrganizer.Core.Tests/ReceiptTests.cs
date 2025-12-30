// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core.Tests;

public class ReceiptTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesReceipt()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var deductionId = Guid.NewGuid();
        var fileName = "receipt.pdf";
        var fileUrl = "https://example.com/receipt.pdf";
        var uploadDate = new DateTime(2024, 3, 15, 10, 30, 0, DateTimeKind.Utc);
        var notes = "Receipt notes";

        // Act
        var receipt = new Receipt
        {
            ReceiptId = receiptId,
            DeductionId = deductionId,
            FileName = fileName,
            FileUrl = fileUrl,
            UploadDate = uploadDate,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(receipt.ReceiptId, Is.EqualTo(receiptId));
            Assert.That(receipt.DeductionId, Is.EqualTo(deductionId));
            Assert.That(receipt.FileName, Is.EqualTo(fileName));
            Assert.That(receipt.FileUrl, Is.EqualTo(fileUrl));
            Assert.That(receipt.UploadDate, Is.EqualTo(uploadDate));
            Assert.That(receipt.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Receipt_UploadDate_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = "test.pdf",
            FileUrl = "https://example.com/test.pdf"
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(receipt.UploadDate, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(receipt.UploadDate, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Receipt_WithoutNotes_DefaultsToNull()
    {
        // Arrange & Act
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = "receipt.jpg",
            FileUrl = "https://example.com/receipt.jpg"
        };

        // Assert
        Assert.That(receipt.Notes, Is.Null);
    }

    [Test]
    public void Receipt_DeductionNavigation_CanBeSet()
    {
        // Arrange
        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = Guid.NewGuid(),
            Description = "Test deduction",
            Amount = 100m,
            Date = DateTime.Now,
            Category = DeductionCategory.Other
        };

        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = deduction.DeductionId,
            FileName = "receipt.pdf",
            FileUrl = "https://example.com/receipt.pdf"
        };

        // Act
        receipt.Deduction = deduction;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(receipt.Deduction, Is.Not.Null);
            Assert.That(receipt.Deduction.DeductionId, Is.EqualTo(deduction.DeductionId));
        });
    }

    [Test]
    public void Receipt_FileName_SupportsVariousFileTypes()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var pdfReceipt = new Receipt { ReceiptId = Guid.NewGuid(), DeductionId = Guid.NewGuid(), FileName = "receipt.pdf", FileUrl = "url" };
            Assert.That(pdfReceipt.FileName, Is.EqualTo("receipt.pdf"));

            var jpgReceipt = new Receipt { ReceiptId = Guid.NewGuid(), DeductionId = Guid.NewGuid(), FileName = "receipt.jpg", FileUrl = "url" };
            Assert.That(jpgReceipt.FileName, Is.EqualTo("receipt.jpg"));

            var pngReceipt = new Receipt { ReceiptId = Guid.NewGuid(), DeductionId = Guid.NewGuid(), FileName = "receipt.png", FileUrl = "url" };
            Assert.That(pngReceipt.FileName, Is.EqualTo("receipt.png"));
        });
    }

    [Test]
    public void Receipt_FileUrl_CanStoreLongUrls()
    {
        // Arrange
        var longUrl = "https://example.com/very/long/path/to/receipt/file/with/many/segments/receipt.pdf?param1=value1&param2=value2";

        // Act
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = "receipt.pdf",
            FileUrl = longUrl
        };

        // Assert
        Assert.That(receipt.FileUrl, Is.EqualTo(longUrl));
    }

    [Test]
    public void Receipt_FileName_CanContainSpaces()
    {
        // Arrange
        var fileNameWithSpaces = "my receipt file.pdf";

        // Act
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = fileNameWithSpaces,
            FileUrl = "https://example.com/receipt.pdf"
        };

        // Assert
        Assert.That(receipt.FileName, Is.EqualTo(fileNameWithSpaces));
    }

    [Test]
    public void Receipt_WithLongNotes_StoresCorrectly()
    {
        // Arrange
        var longNotes = "This is a very long note that contains detailed information about the receipt and the associated deduction. " +
                       "It may include multiple sentences and important details about the transaction.";

        // Act
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = "receipt.pdf",
            FileUrl = "https://example.com/receipt.pdf",
            Notes = longNotes
        };

        // Assert
        Assert.That(receipt.Notes, Is.EqualTo(longNotes));
    }
}
