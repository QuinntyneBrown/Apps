// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PetCareManager.Core.Tests;

public class PetTypeTests
{
    [Test]
    public void Dog_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PetType.Dog, Is.EqualTo(0));
    }

    [Test]
    public void Cat_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PetType.Cat, Is.EqualTo(1));
    }

    [Test]
    public void Bird_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PetType.Bird, Is.EqualTo(2));
    }

    [Test]
    public void Fish_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PetType.Fish, Is.EqualTo(3));
    }

    [Test]
    public void Reptile_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PetType.Reptile, Is.EqualTo(4));
    }

    [Test]
    public void SmallMammal_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PetType.SmallMammal, Is.EqualTo(5));
    }

    [Test]
    public void Other_HasCorrectValue()
    {
        // Assert
        Assert.That((int)PetType.Other, Is.EqualTo(6));
    }

    [Test]
    public void AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var dog = PetType.Dog;
        var cat = PetType.Cat;
        var bird = PetType.Bird;
        var fish = PetType.Fish;
        var reptile = PetType.Reptile;
        var smallMammal = PetType.SmallMammal;
        var other = PetType.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dog, Is.EqualTo(PetType.Dog));
            Assert.That(cat, Is.EqualTo(PetType.Cat));
            Assert.That(bird, Is.EqualTo(PetType.Bird));
            Assert.That(fish, Is.EqualTo(PetType.Fish));
            Assert.That(reptile, Is.EqualTo(PetType.Reptile));
            Assert.That(smallMammal, Is.EqualTo(PetType.SmallMammal));
            Assert.That(other, Is.EqualTo(PetType.Other));
        });
    }
}
