// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core.Tests;

public class ScreeningTypeTests
{
    [Test]
    public void ScreeningType_PhysicalExam_HasCorrectValue()
    {
        // Arrange & Act
        var screeningType = ScreeningType.PhysicalExam;

        // Assert
        Assert.That((int)screeningType, Is.EqualTo(0));
    }

    [Test]
    public void ScreeningType_DentalCheckup_HasCorrectValue()
    {
        // Arrange & Act
        var screeningType = ScreeningType.DentalCheckup;

        // Assert
        Assert.That((int)screeningType, Is.EqualTo(1));
    }

    [Test]
    public void ScreeningType_VisionTest_HasCorrectValue()
    {
        // Arrange & Act
        var screeningType = ScreeningType.VisionTest;

        // Assert
        Assert.That((int)screeningType, Is.EqualTo(2));
    }

    [Test]
    public void ScreeningType_Mammogram_HasCorrectValue()
    {
        // Arrange & Act
        var screeningType = ScreeningType.Mammogram;

        // Assert
        Assert.That((int)screeningType, Is.EqualTo(3));
    }

    [Test]
    public void ScreeningType_Colonoscopy_HasCorrectValue()
    {
        // Arrange & Act
        var screeningType = ScreeningType.Colonoscopy;

        // Assert
        Assert.That((int)screeningType, Is.EqualTo(4));
    }

    [Test]
    public void ScreeningType_BloodWork_HasCorrectValue()
    {
        // Arrange & Act
        var screeningType = ScreeningType.BloodWork;

        // Assert
        Assert.That((int)screeningType, Is.EqualTo(5));
    }

    [Test]
    public void ScreeningType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var screeningType = ScreeningType.Other;

        // Assert
        Assert.That((int)screeningType, Is.EqualTo(6));
    }

    [Test]
    public void ScreeningType_CanBeAssignedToVariable()
    {
        // Arrange & Act
        ScreeningType screeningType = ScreeningType.PhysicalExam;

        // Assert
        Assert.That(screeningType, Is.EqualTo(ScreeningType.PhysicalExam));
    }

    [Test]
    public void ScreeningType_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var allValues = Enum.GetValues<ScreeningType>();

        // Assert
        Assert.That(allValues, Has.Length.EqualTo(7));
        Assert.That(allValues, Contains.Item(ScreeningType.PhysicalExam));
        Assert.That(allValues, Contains.Item(ScreeningType.DentalCheckup));
        Assert.That(allValues, Contains.Item(ScreeningType.VisionTest));
        Assert.That(allValues, Contains.Item(ScreeningType.Mammogram));
        Assert.That(allValues, Contains.Item(ScreeningType.Colonoscopy));
        Assert.That(allValues, Contains.Item(ScreeningType.BloodWork));
        Assert.That(allValues, Contains.Item(ScreeningType.Other));
    }
}
