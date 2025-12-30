// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core.Tests;

public class ReadingProgressTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesReadingProgress()
    {
        // Arrange & Act
        var progress = new ReadingProgress();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.ReadingProgressId, Is.EqualTo(Guid.Empty));
            Assert.That(progress.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(progress.ResourceId, Is.EqualTo(Guid.Empty));
            Assert.That(progress.Status, Is.EqualTo("Not Started"));
            Assert.That(progress.CurrentPage, Is.Null);
            Assert.That(progress.ProgressPercentage, Is.EqualTo(0));
            Assert.That(progress.StartDate, Is.Null);
            Assert.That(progress.CompletionDate, Is.Null);
            Assert.That(progress.Rating, Is.Null);
            Assert.That(progress.Review, Is.Null);
            Assert.That(progress.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(progress.UpdatedAt, Is.Null);
            Assert.That(progress.Resource, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesReadingProgress()
    {
        // Arrange
        var progressId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();

        // Act
        var progress = new ReadingProgress
        {
            ReadingProgressId = progressId,
            UserId = userId,
            ResourceId = resourceId,
            Status = "Reading",
            CurrentPage = 150,
            ProgressPercentage = 50
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.ReadingProgressId, Is.EqualTo(progressId));
            Assert.That(progress.UserId, Is.EqualTo(userId));
            Assert.That(progress.ResourceId, Is.EqualTo(resourceId));
            Assert.That(progress.Status, Is.EqualTo("Reading"));
            Assert.That(progress.CurrentPage, Is.EqualTo(150));
            Assert.That(progress.ProgressPercentage, Is.EqualTo(50));
        });
    }

    [Test]
    public void StartReading_NotStartedProgress_SetsStatusAndStartDate()
    {
        // Arrange
        var progress = new ReadingProgress();
        var beforeStart = DateTime.UtcNow;

        // Act
        progress.StartReading();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.Status, Is.EqualTo("Reading"));
            Assert.That(progress.StartDate, Is.Not.Null);
            Assert.That(progress.StartDate.Value, Is.GreaterThanOrEqualTo(beforeStart));
            Assert.That(progress.StartDate.Value, Is.LessThanOrEqualTo(DateTime.UtcNow));
            Assert.That(progress.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateProgress_ValidParameters_UpdatesProgressAndPage()
    {
        // Arrange
        var progress = new ReadingProgress();
        var currentPage = 100;
        var percentage = 50;

        // Act
        progress.UpdateProgress(currentPage, percentage);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.CurrentPage, Is.EqualTo(currentPage));
            Assert.That(progress.ProgressPercentage, Is.EqualTo(percentage));
            Assert.That(progress.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateProgress_PercentageAbove100_ClampsTo100()
    {
        // Arrange
        var progress = new ReadingProgress();

        // Act
        progress.UpdateProgress(300, 150);

        // Assert
        Assert.That(progress.ProgressPercentage, Is.EqualTo(100));
    }

    [Test]
    public void UpdateProgress_PercentageBelow0_ClampsTo0()
    {
        // Arrange
        var progress = new ReadingProgress();

        // Act
        progress.UpdateProgress(0, -10);

        // Assert
        Assert.That(progress.ProgressPercentage, Is.EqualTo(0));
    }

    [Test]
    public void UpdateProgress_100Percent_AutomaticallyCompletesReading()
    {
        // Arrange
        var progress = new ReadingProgress { Status = "Reading" };
        var beforeComplete = DateTime.UtcNow;

        // Act
        progress.UpdateProgress(300, 100);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.Status, Is.EqualTo("Completed"));
            Assert.That(progress.ProgressPercentage, Is.EqualTo(100));
            Assert.That(progress.CompletionDate, Is.Not.Null);
            Assert.That(progress.CompletionDate.Value, Is.GreaterThanOrEqualTo(beforeComplete));
        });
    }

    [Test]
    public void UpdateProgress_NullCurrentPage_UpdatesOnlyPercentage()
    {
        // Arrange
        var progress = new ReadingProgress();

        // Act
        progress.UpdateProgress(null, 25);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.CurrentPage, Is.Null);
            Assert.That(progress.ProgressPercentage, Is.EqualTo(25));
        });
    }

    [Test]
    public void Complete_InProgressReading_MarksAsCompleted()
    {
        // Arrange
        var progress = new ReadingProgress { Status = "Reading" };
        var beforeComplete = DateTime.UtcNow;

        // Act
        progress.Complete();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.Status, Is.EqualTo("Completed"));
            Assert.That(progress.ProgressPercentage, Is.EqualTo(100));
            Assert.That(progress.CompletionDate, Is.Not.Null);
            Assert.That(progress.CompletionDate.Value, Is.GreaterThanOrEqualTo(beforeComplete));
            Assert.That(progress.CompletionDate.Value, Is.LessThanOrEqualTo(DateTime.UtcNow));
            Assert.That(progress.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void AddRating_ValidRating_SetsRatingAndReview()
    {
        // Arrange
        var progress = new ReadingProgress { Status = "Completed" };
        var rating = 4;
        var review = "Excellent book with practical insights";

        // Act
        progress.AddRating(rating, review);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.Rating, Is.EqualTo(rating));
            Assert.That(progress.Review, Is.EqualTo(review));
            Assert.That(progress.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void AddRating_RatingAbove5_ClampsTo5()
    {
        // Arrange
        var progress = new ReadingProgress();

        // Act
        progress.AddRating(10, "Great");

        // Assert
        Assert.That(progress.Rating, Is.EqualTo(5));
    }

    [Test]
    public void AddRating_RatingBelow1_ClampsTo1()
    {
        // Arrange
        var progress = new ReadingProgress();

        // Act
        progress.AddRating(-5, "Poor");

        // Assert
        Assert.That(progress.Rating, Is.EqualTo(1));
    }

    [Test]
    public void AddRating_Rating1_SetsCorrectly()
    {
        // Arrange
        var progress = new ReadingProgress();

        // Act
        progress.AddRating(1, "Not recommended");

        // Assert
        Assert.That(progress.Rating, Is.EqualTo(1));
    }

    [Test]
    public void AddRating_Rating5_SetsCorrectly()
    {
        // Arrange
        var progress = new ReadingProgress();

        // Act
        progress.AddRating(5, "Must read!");

        // Assert
        Assert.That(progress.Rating, Is.EqualTo(5));
    }

    [Test]
    public void AddRating_NullReview_SetsRatingOnly()
    {
        // Arrange
        var progress = new ReadingProgress();

        // Act
        progress.AddRating(3, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.Rating, Is.EqualTo(3));
            Assert.That(progress.Review, Is.Null);
        });
    }

    [Test]
    public void ReadingProgress_FullWorkflow_WorksCorrectly()
    {
        // Arrange
        var progress = new ReadingProgress
        {
            ReadingProgressId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid()
        };

        // Act & Assert - Start reading
        progress.StartReading();
        Assert.That(progress.Status, Is.EqualTo("Reading"));
        Assert.That(progress.StartDate, Is.Not.Null);

        // Update progress
        progress.UpdateProgress(100, 33);
        Assert.That(progress.ProgressPercentage, Is.EqualTo(33));

        progress.UpdateProgress(200, 66);
        Assert.That(progress.ProgressPercentage, Is.EqualTo(66));

        // Complete
        progress.Complete();
        Assert.That(progress.Status, Is.EqualTo("Completed"));
        Assert.That(progress.ProgressPercentage, Is.EqualTo(100));

        // Add rating
        progress.AddRating(5, "Excellent resource!");
        Assert.That(progress.Rating, Is.EqualTo(5));
    }
}
