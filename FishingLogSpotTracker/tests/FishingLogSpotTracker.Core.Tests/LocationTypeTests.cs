// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;

namespace FishingLogSpotTracker.Core.Tests;

public class LocationTypeTests
{
    [Test]
    public void LocationType_Lake_HasCorrectValue()
    {
        // Assert
        Assert.That(LocationType.Lake, Is.EqualTo((LocationType)0));
    }

    [Test]
    public void LocationType_River_HasCorrectValue()
    {
        // Assert
        Assert.That(LocationType.River, Is.EqualTo((LocationType)1));
    }

    [Test]
    public void LocationType_Stream_HasCorrectValue()
    {
        // Assert
        Assert.That(LocationType.Stream, Is.EqualTo((LocationType)2));
    }

    [Test]
    public void LocationType_Pond_HasCorrectValue()
    {
        // Assert
        Assert.That(LocationType.Pond, Is.EqualTo((LocationType)3));
    }

    [Test]
    public void LocationType_Ocean_HasCorrectValue()
    {
        // Assert
        Assert.That(LocationType.Ocean, Is.EqualTo((LocationType)4));
    }

    [Test]
    public void LocationType_Bay_HasCorrectValue()
    {
        // Assert
        Assert.That(LocationType.Bay, Is.EqualTo((LocationType)5));
    }

    [Test]
    public void LocationType_Reservoir_HasCorrectValue()
    {
        // Assert
        Assert.That(LocationType.Reservoir, Is.EqualTo((LocationType)6));
    }

    [Test]
    public void LocationType_Other_HasCorrectValue()
    {
        // Assert
        Assert.That(LocationType.Other, Is.EqualTo((LocationType)7));
    }

    [Test]
    public void LocationType_AllValues_CanBeAssigned()
    {
        // Arrange
        var allLocationTypes = new[]
        {
            LocationType.Lake,
            LocationType.River,
            LocationType.Stream,
            LocationType.Pond,
            LocationType.Ocean,
            LocationType.Bay,
            LocationType.Reservoir,
            LocationType.Other
        };

        // Act & Assert
        foreach (var locationType in allLocationTypes)
        {
            var spot = new Spot { LocationType = locationType };
            Assert.That(spot.LocationType, Is.EqualTo(locationType));
        }
    }

    [Test]
    public void LocationType_HasExpectedNumberOfValues()
    {
        // Arrange
        var enumValues = Enum.GetValues(typeof(LocationType));

        // Assert
        Assert.That(enumValues.Length, Is.EqualTo(8));
    }
}
