// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core.Tests;

public class IntakeTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesIntake()
    {
        // Arrange
        var intakeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var beverageType = BeverageType.Water;
        var amountMl = 500m;
        var intakeTime = new DateTime(2025, 1, 15, 10, 30, 0);
        var notes = "Morning hydration";

        // Act
        var intake = new Intake
        {
            IntakeId = intakeId,
            UserId = userId,
            BeverageType = beverageType,
            AmountMl = amountMl,
            IntakeTime = intakeTime,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(intake.IntakeId, Is.EqualTo(intakeId));
            Assert.That(intake.UserId, Is.EqualTo(userId));
            Assert.That(intake.BeverageType, Is.EqualTo(beverageType));
            Assert.That(intake.AmountMl, Is.EqualTo(amountMl));
            Assert.That(intake.IntakeTime, Is.EqualTo(intakeTime));
            Assert.That(intake.Notes, Is.EqualTo(notes));
            Assert.That(intake.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var intake = new Intake();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(intake.IntakeId, Is.EqualTo(Guid.Empty));
            Assert.That(intake.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(intake.BeverageType, Is.EqualTo(BeverageType.Water));
            Assert.That(intake.AmountMl, Is.EqualTo(0m));
            Assert.That(intake.IntakeTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(intake.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void GetAmountInOz_ValidAmount_ReturnsCorrectConversion()
    {
        // Arrange
        var intake = new Intake { AmountMl = 500m };

        // Act
        var result = intake.GetAmountInOz();

        // Assert
        Assert.That(result, Is.EqualTo(16.907m).Within(0.001m));
    }

    [Test]
    public void GetAmountInOz_ZeroAmount_ReturnsZero()
    {
        // Arrange
        var intake = new Intake { AmountMl = 0m };

        // Act
        var result = intake.GetAmountInOz();

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void GetAmountInOz_LargeAmount_ReturnsCorrectConversion()
    {
        // Arrange
        var intake = new Intake { AmountMl = 1000m };

        // Act
        var result = intake.GetAmountInOz();

        // Assert
        Assert.That(result, Is.EqualTo(33.814m).Within(0.001m));
    }

    [Test]
    public void GetAmountInOz_SmallAmount_ReturnsCorrectConversion()
    {
        // Arrange
        var intake = new Intake { AmountMl = 100m };

        // Act
        var result = intake.GetAmountInOz();

        // Assert
        Assert.That(result, Is.EqualTo(3.3814m).Within(0.0001m));
    }

    [Test]
    public void BeverageType_AllTypes_CanBeAssigned()
    {
        // Arrange
        var intake = new Intake();

        // Act & Assert
        Assert.DoesNotThrow(() => intake.BeverageType = BeverageType.Water);
        Assert.DoesNotThrow(() => intake.BeverageType = BeverageType.Tea);
        Assert.DoesNotThrow(() => intake.BeverageType = BeverageType.Coffee);
        Assert.DoesNotThrow(() => intake.BeverageType = BeverageType.Juice);
        Assert.DoesNotThrow(() => intake.BeverageType = BeverageType.Sports);
        Assert.DoesNotThrow(() => intake.BeverageType = BeverageType.Other);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var intake = new Intake { Notes = null };

        // Assert
        Assert.That(intake.Notes, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeSet()
    {
        // Arrange & Act
        var intake = new Intake { Notes = "After workout" };

        // Assert
        Assert.That(intake.Notes, Is.EqualTo("After workout"));
    }

    [Test]
    public void IntakeTime_CanBeSetToAnyDateTime()
    {
        // Arrange
        var intake = new Intake();
        var pastTime = new DateTime(2024, 1, 1, 8, 0, 0);
        var futureTime = new DateTime(2026, 12, 31, 23, 59, 59);

        // Act & Assert
        intake.IntakeTime = pastTime;
        Assert.That(intake.IntakeTime, Is.EqualTo(pastTime));

        intake.IntakeTime = futureTime;
        Assert.That(intake.IntakeTime, Is.EqualTo(futureTime));
    }

    [Test]
    public void AmountMl_DecimalPrecision_IsPreserved()
    {
        // Arrange
        var intake = new Intake();
        var amount = 250.75m;

        // Act
        intake.AmountMl = amount;

        // Assert
        Assert.That(intake.AmountMl, Is.EqualTo(amount));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsCurrentUtcTime()
    {
        // Act
        var intake = new Intake();

        // Assert
        Assert.That(intake.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void UserId_CanBeSetAndRetrieved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var intake = new Intake();

        // Act
        intake.UserId = userId;

        // Assert
        Assert.That(intake.UserId, Is.EqualTo(userId));
    }
}
