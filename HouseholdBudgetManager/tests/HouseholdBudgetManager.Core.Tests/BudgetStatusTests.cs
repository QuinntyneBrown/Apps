// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core.Tests;

public class BudgetStatusTests
{
    [Test]
    public void BudgetStatus_DraftValue_EqualsZero()
    {
        // Assert
        Assert.That((int)BudgetStatus.Draft, Is.EqualTo(0));
    }

    [Test]
    public void BudgetStatus_ActiveValue_EqualsOne()
    {
        // Assert
        Assert.That((int)BudgetStatus.Active, Is.EqualTo(1));
    }

    [Test]
    public void BudgetStatus_CompletedValue_EqualsTwo()
    {
        // Assert
        Assert.That((int)BudgetStatus.Completed, Is.EqualTo(2));
    }

    [Test]
    public void BudgetStatus_AllValues_CanBeAssigned()
    {
        // Act & Assert
        BudgetStatus status;

        Assert.DoesNotThrow(() => status = BudgetStatus.Draft);
        Assert.DoesNotThrow(() => status = BudgetStatus.Active);
        Assert.DoesNotThrow(() => status = BudgetStatus.Completed);
    }

    [Test]
    public void BudgetStatus_CanCompareValues()
    {
        // Arrange
        var draft = BudgetStatus.Draft;
        var active = BudgetStatus.Active;
        var completed = BudgetStatus.Completed;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(draft, Is.Not.EqualTo(active));
            Assert.That(active, Is.Not.EqualTo(completed));
            Assert.That(draft, Is.Not.EqualTo(completed));
            Assert.That(draft, Is.EqualTo(BudgetStatus.Draft));
        });
    }

    [Test]
    public void BudgetStatus_DefaultValue_IsDraft()
    {
        // Arrange
        BudgetStatus status = default;

        // Assert
        Assert.That(status, Is.EqualTo(BudgetStatus.Draft));
    }
}
