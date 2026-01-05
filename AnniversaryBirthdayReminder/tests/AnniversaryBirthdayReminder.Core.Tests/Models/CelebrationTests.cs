// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core.Tests;

/// <summary>
/// Unit tests for the Celebration entity.
/// </summary>
[TestFixture]
public class CelebrationTests
{
    /// <summary>
    /// Tests that MarkAsCompleted sets the status correctly.
    /// </summary>
    [Test]
    public void MarkAsCompleted_SetsStatusToCompleted()
    {
        // Arrange
        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            CelebrationDate = DateTime.UtcNow,
        };

        // Act
        celebration.MarkAsCompleted("Great celebration!", 5);

        // Assert
        Assert.That(celebration.Status, Is.EqualTo(CelebrationStatus.Completed));
        Assert.That(celebration.Notes, Is.EqualTo("Great celebration!"));
        Assert.That(celebration.Rating, Is.EqualTo(5));
    }

    /// <summary>
    /// Tests that MarkAsSkipped sets the status correctly.
    /// </summary>
    [Test]
    public void MarkAsSkipped_SetsStatusToSkipped()
    {
        // Arrange
        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            CelebrationDate = DateTime.UtcNow,
        };

        // Act
        celebration.MarkAsSkipped("Out of town");

        // Assert
        Assert.That(celebration.Status, Is.EqualTo(CelebrationStatus.Skipped));
        Assert.That(celebration.Notes, Is.EqualTo("Out of town"));
    }

    /// <summary>
    /// Tests that SetRating throws for invalid rating below 1.
    /// </summary>
    [Test]
    public void SetRating_WhenRatingBelowOne_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            CelebrationDate = DateTime.UtcNow,
        };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => celebration.SetRating(0));
    }

    /// <summary>
    /// Tests that SetRating throws for invalid rating above 5.
    /// </summary>
    [Test]
    public void SetRating_WhenRatingAboveFive_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            CelebrationDate = DateTime.UtcNow,
        };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => celebration.SetRating(6));
    }

    /// <summary>
    /// Tests that SetRating succeeds for valid rating.
    /// </summary>
    [Test]
    public void SetRating_WhenValidRating_SetsRating()
    {
        // Arrange
        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            CelebrationDate = DateTime.UtcNow,
        };

        // Act
        celebration.SetRating(4);

        // Assert
        Assert.That(celebration.Rating, Is.EqualTo(4));
    }

    /// <summary>
    /// Tests that AddPhotos adds photos to the collection.
    /// </summary>
    [Test]
    public void AddPhotos_AddsToCollection()
    {
        // Arrange
        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            CelebrationDate = DateTime.UtcNow,
        };
        var photos = new[] { "photo1.jpg", "photo2.jpg" };

        // Act
        celebration.AddPhotos(photos);

        // Assert
        Assert.That(celebration.Photos, Has.Count.EqualTo(2));
        Assert.That(celebration.Photos, Contains.Item("photo1.jpg"));
        Assert.That(celebration.Photos, Contains.Item("photo2.jpg"));
    }

    /// <summary>
    /// Tests that AddAttendees adds attendees to the collection.
    /// </summary>
    [Test]
    public void AddAttendees_AddsToCollection()
    {
        // Arrange
        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            CelebrationDate = DateTime.UtcNow,
        };
        var attendees = new[] { "John", "Jane" };

        // Act
        celebration.AddAttendees(attendees);

        // Assert
        Assert.That(celebration.Attendees, Has.Count.EqualTo(2));
        Assert.That(celebration.Attendees, Contains.Item("John"));
        Assert.That(celebration.Attendees, Contains.Item("Jane"));
    }
}
