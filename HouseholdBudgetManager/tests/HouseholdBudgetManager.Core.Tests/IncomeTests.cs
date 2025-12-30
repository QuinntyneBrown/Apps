// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core.Tests;

public class IncomeTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesIncome()
    {
        // Arrange
        var incomeId = Guid.NewGuid();
        var budgetId = Guid.NewGuid();
        var description = "Monthly salary";
        var amount = 5000m;
        var source = "Employer";
        var incomeDate = new DateTime(2025, 1, 1);
        var notes = "Regular paycheck";
        var isRecurring = true;

        // Act
        var income = new Income
        {
            IncomeId = incomeId,
            BudgetId = budgetId,
            Description = description,
            Amount = amount,
            Source = source,
            IncomeDate = incomeDate,
            Notes = notes,
            IsRecurring = isRecurring
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(income.IncomeId, Is.EqualTo(incomeId));
            Assert.That(income.BudgetId, Is.EqualTo(budgetId));
            Assert.That(income.Description, Is.EqualTo(description));
            Assert.That(income.Amount, Is.EqualTo(amount));
            Assert.That(income.Source, Is.EqualTo(source));
            Assert.That(income.IncomeDate, Is.EqualTo(incomeDate));
            Assert.That(income.Notes, Is.EqualTo(notes));
            Assert.That(income.IsRecurring, Is.EqualTo(isRecurring));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var income = new Income();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(income.IncomeId, Is.EqualTo(Guid.Empty));
            Assert.That(income.BudgetId, Is.EqualTo(Guid.Empty));
            Assert.That(income.Description, Is.EqualTo(string.Empty));
            Assert.That(income.Amount, Is.EqualTo(0m));
            Assert.That(income.Source, Is.EqualTo(string.Empty));
            Assert.That(income.IsRecurring, Is.False);
        });
    }

    [Test]
    public void ValidateAmount_PositiveAmount_DoesNotThrow()
    {
        // Arrange
        var income = new Income { Amount = 1000m };

        // Act & Assert
        Assert.DoesNotThrow(() => income.ValidateAmount());
    }

    [Test]
    public void ValidateAmount_ZeroAmount_ThrowsArgumentException()
    {
        // Arrange
        var income = new Income { Amount = 0m };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => income.ValidateAmount());
        Assert.Multiple(() =>
        {
            Assert.That(ex.ParamName, Is.EqualTo("Amount"));
            Assert.That(ex.Message, Does.Contain("Income amount must be positive"));
        });
    }

    [Test]
    public void ValidateAmount_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var income = new Income { Amount = -100m };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => income.ValidateAmount());
        Assert.Multiple(() =>
        {
            Assert.That(ex.ParamName, Is.EqualTo("Amount"));
            Assert.That(ex.Message, Does.Contain("Income amount must be positive"));
        });
    }

    [Test]
    public void UpdateDetails_ValidParameters_UpdatesIncome()
    {
        // Arrange
        var income = new Income
        {
            Description = "Old description",
            Amount = 1000m,
            Source = "Old source"
        };

        // Act
        income.UpdateDetails("New description", 2000m, "New source");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(income.Description, Is.EqualTo("New description"));
            Assert.That(income.Amount, Is.EqualTo(2000m));
            Assert.That(income.Source, Is.EqualTo("New source"));
        });
    }

    [Test]
    public void UpdateDetails_EmptyStrings_SetsEmptyStrings()
    {
        // Arrange
        var income = new Income
        {
            Description = "Old description",
            Amount = 1000m,
            Source = "Old source"
        };

        // Act
        income.UpdateDetails("", 1500m, "");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(income.Description, Is.EqualTo(""));
            Assert.That(income.Amount, Is.EqualTo(1500m));
            Assert.That(income.Source, Is.EqualTo(""));
        });
    }

    [Test]
    public void IsRecurring_CanBeToggled()
    {
        // Arrange
        var income = new Income { IsRecurring = false };

        // Act
        income.IsRecurring = true;

        // Assert
        Assert.That(income.IsRecurring, Is.True);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var income = new Income
        {
            Notes = null
        };

        // Assert
        Assert.That(income.Notes, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeSet()
    {
        // Arrange & Act
        var income = new Income
        {
            Notes = "Important note"
        };

        // Assert
        Assert.That(income.Notes, Is.EqualTo("Important note"));
    }

    [Test]
    public void Budget_NavigationProperty_CanBeSet()
    {
        // Arrange
        var budget = new Budget { BudgetId = Guid.NewGuid(), Name = "Test Budget" };
        var income = new Income();

        // Act
        income.Budget = budget;

        // Assert
        Assert.That(income.Budget, Is.EqualTo(budget));
    }

    [Test]
    public void IncomeDate_CanBeSetToAnyDate()
    {
        // Arrange
        var income = new Income();
        var futureDate = new DateTime(2026, 12, 31);
        var pastDate = new DateTime(2020, 1, 1);

        // Act & Assert
        income.IncomeDate = futureDate;
        Assert.That(income.IncomeDate, Is.EqualTo(futureDate));

        income.IncomeDate = pastDate;
        Assert.That(income.IncomeDate, Is.EqualTo(pastDate));
    }

    [Test]
    public void Source_RequiredField_CanBeSet()
    {
        // Arrange
        var income = new Income();
        var source = "Freelance Work";

        // Act
        income.Source = source;

        // Assert
        Assert.That(income.Source, Is.EqualTo(source));
    }
}
