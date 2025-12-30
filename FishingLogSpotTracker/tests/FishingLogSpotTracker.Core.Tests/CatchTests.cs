// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;

namespace FishingLogSpotTracker.Core.Tests;

public class CatchTests
{
    [Test]
    public void Catch_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var catchId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var species = FishSpecies.Bass;
        var length = 18.5m;
        var weight = 3.2m;

        // Act
        var fishCatch = new Catch
        {
            CatchId = catchId,
            UserId = userId,
            TripId = tripId,
            Species = species,
            Length = length,
            Weight = weight,
            BaitUsed = "Rubber worm",
            WasReleased = true,
            Notes = "Beautiful fish",
            PhotoUrl = "https://example.com/photo.jpg"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fishCatch.CatchId, Is.EqualTo(catchId));
            Assert.That(fishCatch.UserId, Is.EqualTo(userId));
            Assert.That(fishCatch.TripId, Is.EqualTo(tripId));
            Assert.That(fishCatch.Species, Is.EqualTo(species));
            Assert.That(fishCatch.Length, Is.EqualTo(length));
            Assert.That(fishCatch.Weight, Is.EqualTo(weight));
            Assert.That(fishCatch.BaitUsed, Is.EqualTo("Rubber worm"));
            Assert.That(fishCatch.WasReleased, Is.True);
            Assert.That(fishCatch.Notes, Is.EqualTo("Beautiful fish"));
            Assert.That(fishCatch.PhotoUrl, Is.EqualTo("https://example.com/photo.jpg"));
            Assert.That(fishCatch.CatchTime, Is.Not.EqualTo(default(DateTime)));
            Assert.That(fishCatch.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Catch_DefaultValues_AreSetCorrectly()
    {
        // Act
        var fishCatch = new Catch();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fishCatch.WasReleased, Is.False);
            Assert.That(fishCatch.CatchTime, Is.Not.EqualTo(default(DateTime)));
            Assert.That(fishCatch.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsReleased_WhenNotReleased_SetsWasReleasedToTrue()
    {
        // Arrange
        var fishCatch = new Catch { WasReleased = false };

        // Act
        fishCatch.MarkAsReleased();

        // Assert
        Assert.That(fishCatch.WasReleased, Is.True);
    }

    [Test]
    public void MarkAsReleased_WhenAlreadyReleased_RemainsTrue()
    {
        // Arrange
        var fishCatch = new Catch { WasReleased = true };

        // Act
        fishCatch.MarkAsReleased();

        // Assert
        Assert.That(fishCatch.WasReleased, Is.True);
    }

    [Test]
    public void Catch_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var fishCatch = new Catch
        {
            Length = null,
            Weight = null,
            BaitUsed = null,
            Notes = null,
            PhotoUrl = null,
            Trip = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fishCatch.Length, Is.Null);
            Assert.That(fishCatch.Weight, Is.Null);
            Assert.That(fishCatch.BaitUsed, Is.Null);
            Assert.That(fishCatch.Notes, Is.Null);
            Assert.That(fishCatch.PhotoUrl, Is.Null);
            Assert.That(fishCatch.Trip, Is.Null);
        });
    }

    [Test]
    public void Catch_Species_CanBeSetToAllValues()
    {
        // Arrange
        var fishCatch = new Catch();

        // Act & Assert
        foreach (FishSpecies species in Enum.GetValues(typeof(FishSpecies)))
        {
            fishCatch.Species = species;
            Assert.That(fishCatch.Species, Is.EqualTo(species));
        }
    }

    [Test]
    public void Catch_WithZeroLength_IsValid()
    {
        // Arrange & Act
        var fishCatch = new Catch { Length = 0m };

        // Assert
        Assert.That(fishCatch.Length, Is.EqualTo(0m));
    }

    [Test]
    public void Catch_WithZeroWeight_IsValid()
    {
        // Arrange & Act
        var fishCatch = new Catch { Weight = 0m };

        // Assert
        Assert.That(fishCatch.Weight, Is.EqualTo(0m));
    }

    [Test]
    public void Catch_WithLargeLength_IsValid()
    {
        // Arrange & Act
        var fishCatch = new Catch { Length = 100.5m };

        // Assert
        Assert.That(fishCatch.Length, Is.EqualTo(100.5m));
    }

    [Test]
    public void Catch_WithLargeWeight_IsValid()
    {
        // Arrange & Act
        var fishCatch = new Catch { Weight = 50.75m };

        // Assert
        Assert.That(fishCatch.Weight, Is.EqualTo(50.75m));
    }

    [Test]
    public void Catch_CatchTime_CanBeSetToSpecificDateTime()
    {
        // Arrange
        var specificTime = new DateTime(2024, 6, 15, 10, 30, 0);
        var fishCatch = new Catch();

        // Act
        fishCatch.CatchTime = specificTime;

        // Assert
        Assert.That(fishCatch.CatchTime, Is.EqualTo(specificTime));
    }
}
