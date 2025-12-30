// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core.Tests;

public class BudgetTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesBudget()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var name = "Monthly Budget";
        var period = "January 2025";
        var startDate = new DateTime(2025, 1, 1);
        var endDate = new DateTime(2025, 1, 31);
        var totalIncome = 5000m;
        var totalExpenses = 3000m;
        var status = BudgetStatus.Draft;
        var notes = "Test notes";

        // Act
        var budget = new Budget
        {
            BudgetId = budgetId,
            Name = name,
            Period = period,
            StartDate = startDate,
            EndDate = endDate,
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            Status = status,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.BudgetId, Is.EqualTo(budgetId));
            Assert.That(budget.Name, Is.EqualTo(name));
            Assert.That(budget.Period, Is.EqualTo(period));
            Assert.That(budget.StartDate, Is.EqualTo(startDate));
            Assert.That(budget.EndDate, Is.EqualTo(endDate));
            Assert.That(budget.TotalIncome, Is.EqualTo(totalIncome));
            Assert.That(budget.TotalExpenses, Is.EqualTo(totalExpenses));
            Assert.That(budget.Status, Is.EqualTo(status));
            Assert.That(budget.Notes, Is.EqualTo(notes));
            Assert.That(budget.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var budget = new Budget();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.BudgetId, Is.EqualTo(Guid.Empty));
            Assert.That(budget.Name, Is.EqualTo(string.Empty));
            Assert.That(budget.Period, Is.EqualTo(string.Empty));
            Assert.That(budget.TotalIncome, Is.EqualTo(0m));
            Assert.That(budget.TotalExpenses, Is.EqualTo(0m));
            Assert.That(budget.Status, Is.EqualTo(BudgetStatus.Draft));
            Assert.That(budget.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void CalculateSurplusDeficit_PositiveBalance_ReturnsSurplus()
    {
        // Arrange
        var budget = new Budget
        {
            TotalIncome = 5000m,
            TotalExpenses = 3000m
        };

        // Act
        var result = budget.CalculateSurplusDeficit();

        // Assert
        Assert.That(result, Is.EqualTo(2000m));
    }

    [Test]
    public void CalculateSurplusDeficit_NegativeBalance_ReturnsDeficit()
    {
        // Arrange
        var budget = new Budget
        {
            TotalIncome = 3000m,
            TotalExpenses = 5000m
        };

        // Act
        var result = budget.CalculateSurplusDeficit();

        // Assert
        Assert.That(result, Is.EqualTo(-2000m));
    }

    [Test]
    public void CalculateSurplusDeficit_EqualIncomeAndExpenses_ReturnsZero()
    {
        // Arrange
        var budget = new Budget
        {
            TotalIncome = 3000m,
            TotalExpenses = 3000m
        };

        // Act
        var result = budget.CalculateSurplusDeficit();

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void UpdateTotals_ValidAmounts_UpdatesTotals()
    {
        // Arrange
        var budget = new Budget
        {
            TotalIncome = 1000m,
            TotalExpenses = 500m
        };

        // Act
        budget.UpdateTotals(5000m, 3000m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.TotalIncome, Is.EqualTo(5000m));
            Assert.That(budget.TotalExpenses, Is.EqualTo(3000m));
        });
    }

    [Test]
    public void UpdateTotals_ZeroAmounts_UpdatesToZero()
    {
        // Arrange
        var budget = new Budget
        {
            TotalIncome = 1000m,
            TotalExpenses = 500m
        };

        // Act
        budget.UpdateTotals(0m, 0m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.TotalIncome, Is.EqualTo(0m));
            Assert.That(budget.TotalExpenses, Is.EqualTo(0m));
        });
    }

    [Test]
    public void Activate_ChangesStatusToActive()
    {
        // Arrange
        var budget = new Budget
        {
            Status = BudgetStatus.Draft
        };

        // Act
        budget.Activate();

        // Assert
        Assert.That(budget.Status, Is.EqualTo(BudgetStatus.Active));
    }

    [Test]
    public void Activate_FromCompletedStatus_ChangesToActive()
    {
        // Arrange
        var budget = new Budget
        {
            Status = BudgetStatus.Completed
        };

        // Act
        budget.Activate();

        // Assert
        Assert.That(budget.Status, Is.EqualTo(BudgetStatus.Active));
    }

    [Test]
    public void Complete_ChangesStatusToCompleted()
    {
        // Arrange
        var budget = new Budget
        {
            Status = BudgetStatus.Active
        };

        // Act
        budget.Complete();

        // Assert
        Assert.That(budget.Status, Is.EqualTo(BudgetStatus.Completed));
    }

    [Test]
    public void Complete_FromDraftStatus_ChangesToCompleted()
    {
        // Arrange
        var budget = new Budget
        {
            Status = BudgetStatus.Draft
        };

        // Act
        budget.Complete();

        // Assert
        Assert.That(budget.Status, Is.EqualTo(BudgetStatus.Completed));
    }

    [Test]
    public void Period_CanBeSetAndRetrieved()
    {
        // Arrange
        var budget = new Budget();
        var period = "Q1 2025";

        // Act
        budget.Period = period;

        // Assert
        Assert.That(budget.Period, Is.EqualTo(period));
    }

    [Test]
    public void DateRange_ValidRange_CanBeSet()
    {
        // Arrange
        var budget = new Budget();
        var startDate = new DateTime(2025, 1, 1);
        var endDate = new DateTime(2025, 12, 31);

        // Act
        budget.StartDate = startDate;
        budget.EndDate = endDate;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.StartDate, Is.EqualTo(startDate));
            Assert.That(budget.EndDate, Is.EqualTo(endDate));
            Assert.That(budget.EndDate, Is.GreaterThan(budget.StartDate));
        });
    }
}
