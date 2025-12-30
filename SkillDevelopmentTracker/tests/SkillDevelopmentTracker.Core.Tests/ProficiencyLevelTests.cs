// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core.Tests;

public class ProficiencyLevelTests
{
    [Test]
    public void ProficiencyLevel_Beginner_HasValue0()
    {
        // Arrange & Act
        var level = ProficiencyLevel.Beginner;

        // Assert
        Assert.That((int)level, Is.EqualTo(0));
    }

    [Test]
    public void ProficiencyLevel_Novice_HasValue1()
    {
        // Arrange & Act
        var level = ProficiencyLevel.Novice;

        // Assert
        Assert.That((int)level, Is.EqualTo(1));
    }

    [Test]
    public void ProficiencyLevel_Intermediate_HasValue2()
    {
        // Arrange & Act
        var level = ProficiencyLevel.Intermediate;

        // Assert
        Assert.That((int)level, Is.EqualTo(2));
    }

    [Test]
    public void ProficiencyLevel_Advanced_HasValue3()
    {
        // Arrange & Act
        var level = ProficiencyLevel.Advanced;

        // Assert
        Assert.That((int)level, Is.EqualTo(3));
    }

    [Test]
    public void ProficiencyLevel_Expert_HasValue4()
    {
        // Arrange & Act
        var level = ProficiencyLevel.Expert;

        // Assert
        Assert.That((int)level, Is.EqualTo(4));
    }

    [Test]
    public void ProficiencyLevel_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var values = Enum.GetValues<ProficiencyLevel>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(values, Has.Length.EqualTo(5));
            Assert.That(values, Contains.Item(ProficiencyLevel.Beginner));
            Assert.That(values, Contains.Item(ProficiencyLevel.Novice));
            Assert.That(values, Contains.Item(ProficiencyLevel.Intermediate));
            Assert.That(values, Contains.Item(ProficiencyLevel.Advanced));
            Assert.That(values, Contains.Item(ProficiencyLevel.Expert));
        });
    }

    [Test]
    public void ProficiencyLevel_CanCompareValues_InAscendingOrder()
    {
        // Arrange & Act & Assert
        Assert.That(ProficiencyLevel.Beginner < ProficiencyLevel.Novice, Is.True);
        Assert.That(ProficiencyLevel.Novice < ProficiencyLevel.Intermediate, Is.True);
        Assert.That(ProficiencyLevel.Intermediate < ProficiencyLevel.Advanced, Is.True);
        Assert.That(ProficiencyLevel.Advanced < ProficiencyLevel.Expert, Is.True);
    }
}
