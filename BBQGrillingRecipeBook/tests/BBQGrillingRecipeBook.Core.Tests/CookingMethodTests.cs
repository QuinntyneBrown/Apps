// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core.Tests;

public class CookingMethodTests
{
    [Test]
    public void CookingMethod_DirectGrilling_HasCorrectValue()
    {
        // Arrange & Act
        var cookingMethod = CookingMethod.DirectGrilling;

        // Assert
        Assert.That((int)cookingMethod, Is.EqualTo(0));
    }

    [Test]
    public void CookingMethod_IndirectGrilling_HasCorrectValue()
    {
        // Arrange & Act
        var cookingMethod = CookingMethod.IndirectGrilling;

        // Assert
        Assert.That((int)cookingMethod, Is.EqualTo(1));
    }

    [Test]
    public void CookingMethod_Smoking_HasCorrectValue()
    {
        // Arrange & Act
        var cookingMethod = CookingMethod.Smoking;

        // Assert
        Assert.That((int)cookingMethod, Is.EqualTo(2));
    }

    [Test]
    public void CookingMethod_Rotisserie_HasCorrectValue()
    {
        // Arrange & Act
        var cookingMethod = CookingMethod.Rotisserie;

        // Assert
        Assert.That((int)cookingMethod, Is.EqualTo(3));
    }

    [Test]
    public void CookingMethod_Searing_HasCorrectValue()
    {
        // Arrange & Act
        var cookingMethod = CookingMethod.Searing;

        // Assert
        Assert.That((int)cookingMethod, Is.EqualTo(4));
    }

    [Test]
    public void CookingMethod_SlowAndLow_HasCorrectValue()
    {
        // Arrange & Act
        var cookingMethod = CookingMethod.SlowAndLow;

        // Assert
        Assert.That((int)cookingMethod, Is.EqualTo(5));
    }

    [Test]
    public void CookingMethod_Combination_HasCorrectValue()
    {
        // Arrange & Act
        var cookingMethod = CookingMethod.Combination;

        // Assert
        Assert.That((int)cookingMethod, Is.EqualTo(6));
    }

    [Test]
    public void CookingMethod_CanBeAssignedToVariable()
    {
        // Arrange & Act
        CookingMethod cookingMethod = CookingMethod.Smoking;

        // Assert
        Assert.That(cookingMethod, Is.EqualTo(CookingMethod.Smoking));
    }

    [Test]
    public void CookingMethod_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var allValues = Enum.GetValues<CookingMethod>();

        // Assert
        Assert.That(allValues, Has.Length.EqualTo(7));
        Assert.That(allValues, Contains.Item(CookingMethod.DirectGrilling));
        Assert.That(allValues, Contains.Item(CookingMethod.IndirectGrilling));
        Assert.That(allValues, Contains.Item(CookingMethod.Smoking));
        Assert.That(allValues, Contains.Item(CookingMethod.Rotisserie));
        Assert.That(allValues, Contains.Item(CookingMethod.Searing));
        Assert.That(allValues, Contains.Item(CookingMethod.SlowAndLow));
        Assert.That(allValues, Contains.Item(CookingMethod.Combination));
    }
}
