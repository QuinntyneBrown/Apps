// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class DifficultyLevelTests
{
    [Test]
    public void DifficultyLevel_Easy_CanBeAssigned()
    {
        var level = DifficultyLevel.Easy;
        Assert.That(level, Is.EqualTo(DifficultyLevel.Easy));
        Assert.That((int)level, Is.EqualTo(0));
    }

    [Test]
    public void DifficultyLevel_Medium_CanBeAssigned()
    {
        var level = DifficultyLevel.Medium;
        Assert.That(level, Is.EqualTo(DifficultyLevel.Medium));
        Assert.That((int)level, Is.EqualTo(1));
    }

    [Test]
    public void DifficultyLevel_Hard_CanBeAssigned()
    {
        var level = DifficultyLevel.Hard;
        Assert.That(level, Is.EqualTo(DifficultyLevel.Hard));
        Assert.That((int)level, Is.EqualTo(2));
    }

    [Test]
    public void DifficultyLevel_Expert_CanBeAssigned()
    {
        var level = DifficultyLevel.Expert;
        Assert.That(level, Is.EqualTo(DifficultyLevel.Expert));
        Assert.That((int)level, Is.EqualTo(3));
    }

    [Test]
    public void DifficultyLevel_AllValues_AreUnique()
    {
        var values = Enum.GetValues<DifficultyLevel>();
        var uniqueValues = values.Distinct().ToList();
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }

    [Test]
    public void DifficultyLevel_HasExpectedNumberOfValues()
    {
        var values = Enum.GetValues<DifficultyLevel>();
        Assert.That(values.Length, Is.EqualTo(4));
    }
}
