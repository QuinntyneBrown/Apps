// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core.Tests;

public class ReceiptUploadedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var deductionId = Guid.NewGuid();
        var fileName = "receipt.pdf";
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new ReceiptUploadedEvent
        {
            ReceiptId = receiptId,
            DeductionId = deductionId,
            FileName = fileName,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ReceiptId, Is.EqualTo(receiptId));
            Assert.That(eventData.DeductionId, Is.EqualTo(deductionId));
            Assert.That(eventData.FileName, Is.EqualTo(fileName));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var eventData = new ReceiptUploadedEvent
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = "receipt.pdf"
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Event_WithVariousFileNames_StoresCorrectly()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var pdfEvent = new ReceiptUploadedEvent { ReceiptId = Guid.NewGuid(), DeductionId = Guid.NewGuid(), FileName = "receipt.pdf" };
            Assert.That(pdfEvent.FileName, Is.EqualTo("receipt.pdf"));

            var jpgEvent = new ReceiptUploadedEvent { ReceiptId = Guid.NewGuid(), DeductionId = Guid.NewGuid(), FileName = "receipt.jpg" };
            Assert.That(jpgEvent.FileName, Is.EqualTo("receipt.jpg"));

            var pngEvent = new ReceiptUploadedEvent { ReceiptId = Guid.NewGuid(), DeductionId = Guid.NewGuid(), FileName = "scan_001.png" };
            Assert.That(pngEvent.FileName, Is.EqualTo("scan_001.png"));
        });
    }

    [Test]
    public void Event_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var deductionId = Guid.NewGuid();
        var fileName = "receipt.pdf";
        var timestamp = DateTime.UtcNow;

        var event1 = new ReceiptUploadedEvent
        {
            ReceiptId = receiptId,
            DeductionId = deductionId,
            FileName = fileName,
            Timestamp = timestamp
        };

        var event2 = new ReceiptUploadedEvent
        {
            ReceiptId = receiptId,
            DeductionId = deductionId,
            FileName = fileName,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithLongFileName_StoresCorrectly()
    {
        // Arrange
        var longFileName = "medical_expense_receipt_doctor_visit_2024_03_15_final.pdf";

        // Act
        var eventData = new ReceiptUploadedEvent
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = Guid.NewGuid(),
            FileName = longFileName
        };

        // Assert
        Assert.That(eventData.FileName, Is.EqualTo(longFileName));
    }
}
