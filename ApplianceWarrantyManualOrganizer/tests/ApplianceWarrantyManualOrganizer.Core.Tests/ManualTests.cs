// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Core.Tests;

public class ManualTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesManual()
    {
        // Arrange
        var manualId = Guid.NewGuid();
        var applianceId = Guid.NewGuid();
        var title = "Samsung Refrigerator User Manual";
        var fileUrl = "https://example.com/manuals/samsung-rf28.pdf";
        var fileType = "application/pdf";

        // Act
        var manual = new Manual
        {
            ManualId = manualId,
            ApplianceId = applianceId,
            Title = title,
            FileUrl = fileUrl,
            FileType = fileType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(manual.ManualId, Is.EqualTo(manualId));
            Assert.That(manual.ApplianceId, Is.EqualTo(applianceId));
            Assert.That(manual.Title, Is.EqualTo(title));
            Assert.That(manual.FileUrl, Is.EqualTo(fileUrl));
            Assert.That(manual.FileType, Is.EqualTo(fileType));
            Assert.That(manual.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var manual = new Manual();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(manual.Title, Is.Null);
            Assert.That(manual.FileUrl, Is.Null);
            Assert.That(manual.FileType, Is.Null);
            Assert.That(manual.Appliance, Is.Null);
            Assert.That(manual.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Title_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual();
        var title = "Installation Guide";

        // Act
        manual.Title = title;

        // Assert
        Assert.That(manual.Title, Is.EqualTo(title));
    }

    [Test]
    public void FileUrl_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual();
        var fileUrl = "https://example.com/manual.pdf";

        // Act
        manual.FileUrl = fileUrl;

        // Assert
        Assert.That(manual.FileUrl, Is.EqualTo(fileUrl));
    }

    [Test]
    public void FileType_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual();
        var fileType = "application/pdf";

        // Act
        manual.FileType = fileType;

        // Assert
        Assert.That(manual.FileType, Is.EqualTo(fileType));
    }

    [Test]
    public void Appliance_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual();
        var appliance = new Appliance { ApplianceId = Guid.NewGuid() };

        // Act
        manual.Appliance = appliance;

        // Assert
        Assert.That(manual.Appliance, Is.EqualTo(appliance));
    }

    [Test]
    public void ApplianceId_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual();
        var applianceId = Guid.NewGuid();

        // Act
        manual.ApplianceId = applianceId;

        // Assert
        Assert.That(manual.ApplianceId, Is.EqualTo(applianceId));
    }

    [Test]
    public void Title_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual { Title = "Some Title" };

        // Act
        manual.Title = null;

        // Assert
        Assert.That(manual.Title, Is.Null);
    }

    [Test]
    public void FileUrl_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual { FileUrl = "https://example.com/manual.pdf" };

        // Act
        manual.FileUrl = null;

        // Assert
        Assert.That(manual.FileUrl, Is.Null);
    }

    [Test]
    public void FileType_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual { FileType = "application/pdf" };

        // Act
        manual.FileType = null;

        // Assert
        Assert.That(manual.FileType, Is.Null);
    }

    [Test]
    public void ManualId_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var manual = new Manual();
        var manualId = Guid.NewGuid();

        // Act
        manual.ManualId = manualId;

        // Assert
        Assert.That(manual.ManualId, Is.EqualTo(manualId));
    }
}
