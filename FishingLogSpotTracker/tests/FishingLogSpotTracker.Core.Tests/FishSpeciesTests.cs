// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;

namespace FishingLogSpotTracker.Core.Tests;

public class FishSpeciesTests
{
    [Test]
    public void FishSpecies_Bass_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Bass, Is.EqualTo((FishSpecies)0));
    }

    [Test]
    public void FishSpecies_Trout_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Trout, Is.EqualTo((FishSpecies)1));
    }

    [Test]
    public void FishSpecies_Salmon_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Salmon, Is.EqualTo((FishSpecies)2));
    }

    [Test]
    public void FishSpecies_Pike_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Pike, Is.EqualTo((FishSpecies)3));
    }

    [Test]
    public void FishSpecies_Walleye_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Walleye, Is.EqualTo((FishSpecies)4));
    }

    [Test]
    public void FishSpecies_Catfish_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Catfish, Is.EqualTo((FishSpecies)5));
    }

    [Test]
    public void FishSpecies_Perch_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Perch, Is.EqualTo((FishSpecies)6));
    }

    [Test]
    public void FishSpecies_Muskie_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Muskie, Is.EqualTo((FishSpecies)7));
    }

    [Test]
    public void FishSpecies_Crappie_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Crappie, Is.EqualTo((FishSpecies)8));
    }

    [Test]
    public void FishSpecies_Bluegill_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Bluegill, Is.EqualTo((FishSpecies)9));
    }

    [Test]
    public void FishSpecies_Sunfish_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Sunfish, Is.EqualTo((FishSpecies)10));
    }

    [Test]
    public void FishSpecies_Other_HasCorrectValue()
    {
        // Assert
        Assert.That(FishSpecies.Other, Is.EqualTo((FishSpecies)11));
    }

    [Test]
    public void FishSpecies_AllValues_CanBeAssigned()
    {
        // Arrange
        var allSpecies = new[]
        {
            FishSpecies.Bass,
            FishSpecies.Trout,
            FishSpecies.Salmon,
            FishSpecies.Pike,
            FishSpecies.Walleye,
            FishSpecies.Catfish,
            FishSpecies.Perch,
            FishSpecies.Muskie,
            FishSpecies.Crappie,
            FishSpecies.Bluegill,
            FishSpecies.Sunfish,
            FishSpecies.Other
        };

        // Act & Assert
        foreach (var species in allSpecies)
        {
            var fishCatch = new Catch { Species = species };
            Assert.That(fishCatch.Species, Is.EqualTo(species));
        }
    }

    [Test]
    public void FishSpecies_HasExpectedNumberOfValues()
    {
        // Arrange
        var enumValues = Enum.GetValues(typeof(FishSpecies));

        // Assert
        Assert.That(enumValues.Length, Is.EqualTo(12));
    }
}
