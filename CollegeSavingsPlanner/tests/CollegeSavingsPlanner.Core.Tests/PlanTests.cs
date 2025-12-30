// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core.Tests;

public class PlanTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var plan = new Plan();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(plan.Name, Is.EqualTo(string.Empty));
            Assert.That(plan.State, Is.EqualTo(string.Empty));
            Assert.That(plan.CurrentBalance, Is.EqualTo(0));
            Assert.That(plan.IsActive, Is.True);
            Assert.That(plan.AccountNumber, Is.Null);
            Assert.That(plan.Administrator, Is.Null);
            Assert.That(plan.Notes, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var planId = Guid.NewGuid();
        var openedDate = DateTime.UtcNow.AddYears(-2);

        // Act
        var plan = new Plan
        {
            PlanId = planId,
            Name = "My 529 Plan",
            State = "California",
            AccountNumber = "ACC-12345",
            CurrentBalance = 25000m,
            OpenedDate = openedDate,
            Administrator = "Vanguard",
            IsActive = true,
            Notes = "Started for daughter's college fund"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(plan.PlanId, Is.EqualTo(planId));
            Assert.That(plan.Name, Is.EqualTo("My 529 Plan"));
            Assert.That(plan.State, Is.EqualTo("California"));
            Assert.That(plan.AccountNumber, Is.EqualTo("ACC-12345"));
            Assert.That(plan.CurrentBalance, Is.EqualTo(25000m));
            Assert.That(plan.OpenedDate, Is.EqualTo(openedDate));
            Assert.That(plan.Administrator, Is.EqualTo("Vanguard"));
            Assert.That(plan.IsActive, Is.True);
            Assert.That(plan.Notes, Is.EqualTo("Started for daughter's college fund"));
        });
    }

    [Test]
    public void UpdateBalance_ValidPositiveBalance_UpdatesBalance()
    {
        // Arrange
        var plan = new Plan { CurrentBalance = 1000m };

        // Act
        plan.UpdateBalance(5000m);

        // Assert
        Assert.That(plan.CurrentBalance, Is.EqualTo(5000m));
    }

    [Test]
    public void UpdateBalance_ZeroBalance_UpdatesBalance()
    {
        // Arrange
        var plan = new Plan { CurrentBalance = 1000m };

        // Act
        plan.UpdateBalance(0m);

        // Assert
        Assert.That(plan.CurrentBalance, Is.EqualTo(0m));
    }

    [Test]
    public void UpdateBalance_NegativeBalance_ThrowsArgumentException()
    {
        // Arrange
        var plan = new Plan { CurrentBalance = 1000m };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => plan.UpdateBalance(-100m));
        Assert.Multiple(() =>
        {
            Assert.That(ex.Message, Does.Contain("Balance cannot be negative"));
            Assert.That(ex.ParamName, Is.EqualTo("newBalance"));
            Assert.That(plan.CurrentBalance, Is.EqualTo(1000m)); // Balance unchanged
        });
    }

    [Test]
    public void UpdateBalance_LargeBalance_UpdatesBalance()
    {
        // Arrange
        var plan = new Plan { CurrentBalance = 1000m };

        // Act
        plan.UpdateBalance(999999.99m);

        // Assert
        Assert.That(plan.CurrentBalance, Is.EqualTo(999999.99m));
    }

    [Test]
    public void Close_SetsIsActiveToFalse()
    {
        // Arrange
        var plan = new Plan { IsActive = true };

        // Act
        plan.Close();

        // Assert
        Assert.That(plan.IsActive, Is.False);
    }

    [Test]
    public void Close_AlreadyClosed_RemainsInactive()
    {
        // Arrange
        var plan = new Plan { IsActive = false };

        // Act
        plan.Close();

        // Assert
        Assert.That(plan.IsActive, Is.False);
    }
}
