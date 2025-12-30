// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core.Tests;

public class TechniqueTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTechnique()
    {
        // Arrange
        var techniqueId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Reverse Sear";
        var description = "Indirect heat then sear";
        var category = "Beef Techniques";
        var difficultyLevel = 3;
        var instructions = "1. Smoke low\n2. Sear high";
        var tips = "Use a meat thermometer";

        // Act
        var technique = new Technique
        {
            TechniqueId = techniqueId,
            UserId = userId,
            Name = name,
            Description = description,
            Category = category,
            DifficultyLevel = difficultyLevel,
            Instructions = instructions,
            Tips = tips,
            IsFavorite = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(technique.TechniqueId, Is.EqualTo(techniqueId));
            Assert.That(technique.UserId, Is.EqualTo(userId));
            Assert.That(technique.Name, Is.EqualTo(name));
            Assert.That(technique.Description, Is.EqualTo(description));
            Assert.That(technique.Category, Is.EqualTo(category));
            Assert.That(technique.DifficultyLevel, Is.EqualTo(difficultyLevel));
            Assert.That(technique.Instructions, Is.EqualTo(instructions));
            Assert.That(technique.Tips, Is.EqualTo(tips));
            Assert.That(technique.IsFavorite, Is.True);
            Assert.That(technique.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var technique = new Technique();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(technique.Name, Is.EqualTo(string.Empty));
            Assert.That(technique.Description, Is.EqualTo(string.Empty));
            Assert.That(technique.Category, Is.EqualTo(string.Empty));
            Assert.That(technique.DifficultyLevel, Is.EqualTo(1));
            Assert.That(technique.Instructions, Is.EqualTo(string.Empty));
            Assert.That(technique.Tips, Is.Null);
            Assert.That(technique.IsFavorite, Is.False);
            Assert.That(technique.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ToggleFavorite_WhenFalse_BecomesTrue()
    {
        // Arrange
        var technique = new Technique { IsFavorite = false };

        // Act
        technique.ToggleFavorite();

        // Assert
        Assert.That(technique.IsFavorite, Is.True);
    }

    [Test]
    public void ToggleFavorite_WhenTrue_BecomesFalse()
    {
        // Arrange
        var technique = new Technique { IsFavorite = true };

        // Act
        technique.ToggleFavorite();

        // Assert
        Assert.That(technique.IsFavorite, Is.False);
    }

    [Test]
    public void ToggleFavorite_CalledTwice_ReturnsToOriginalValue()
    {
        // Arrange
        var technique = new Technique { IsFavorite = false };

        // Act
        technique.ToggleFavorite();
        technique.ToggleFavorite();

        // Assert
        Assert.That(technique.IsFavorite, Is.False);
    }

    [Test]
    public void Name_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var technique = new Technique();
        var name = "Low and Slow Smoking";

        // Act
        technique.Name = name;

        // Assert
        Assert.That(technique.Name, Is.EqualTo(name));
    }

    [Test]
    public void DifficultyLevel_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var technique = new Technique();

        // Act
        technique.DifficultyLevel = 5;

        // Assert
        Assert.That(technique.DifficultyLevel, Is.EqualTo(5));
    }

    [Test]
    public void Category_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var technique = new Technique();
        var category = "Pork Techniques";

        // Act
        technique.Category = category;

        // Assert
        Assert.That(technique.Category, Is.EqualTo(category));
    }

    [Test]
    public void Instructions_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var technique = new Technique();
        var instructions = "Step by step guide";

        // Act
        technique.Instructions = instructions;

        // Assert
        Assert.That(technique.Instructions, Is.EqualTo(instructions));
    }

    [Test]
    public void Tips_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var technique = new Technique();
        var tips = "Always let meat rest";

        // Act
        technique.Tips = tips;

        // Assert
        Assert.That(technique.Tips, Is.EqualTo(tips));
    }

    [Test]
    public void Tips_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var technique = new Technique { Tips = "Some tips" };

        // Act
        technique.Tips = null;

        // Assert
        Assert.That(technique.Tips, Is.Null);
    }
}
