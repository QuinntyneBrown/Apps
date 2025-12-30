// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core.Tests;

public class ExpenseTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesExpense()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var budgetId = Guid.NewGuid();
        var description = "Grocery shopping";
        var amount = 150.50m;
        var category = ExpenseCategory.Food;
        var expenseDate = new DateTime(2025, 1, 15);
        var payee = "Supermarket";
        var paymentMethod = "Credit Card";
        var notes = "Weekly groceries";
        var isRecurring = true;

        // Act
        var expense = new Expense
        {
            ExpenseId = expenseId,
            BudgetId = budgetId,
            Description = description,
            Amount = amount,
            Category = category,
            ExpenseDate = expenseDate,
            Payee = payee,
            PaymentMethod = paymentMethod,
            Notes = notes,
            IsRecurring = isRecurring
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.ExpenseId, Is.EqualTo(expenseId));
            Assert.That(expense.BudgetId, Is.EqualTo(budgetId));
            Assert.That(expense.Description, Is.EqualTo(description));
            Assert.That(expense.Amount, Is.EqualTo(amount));
            Assert.That(expense.Category, Is.EqualTo(category));
            Assert.That(expense.ExpenseDate, Is.EqualTo(expenseDate));
            Assert.That(expense.Payee, Is.EqualTo(payee));
            Assert.That(expense.PaymentMethod, Is.EqualTo(paymentMethod));
            Assert.That(expense.Notes, Is.EqualTo(notes));
            Assert.That(expense.IsRecurring, Is.EqualTo(isRecurring));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var expense = new Expense();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.ExpenseId, Is.EqualTo(Guid.Empty));
            Assert.That(expense.BudgetId, Is.EqualTo(Guid.Empty));
            Assert.That(expense.Description, Is.EqualTo(string.Empty));
            Assert.That(expense.Amount, Is.EqualTo(0m));
            Assert.That(expense.Category, Is.EqualTo(ExpenseCategory.Housing));
            Assert.That(expense.IsRecurring, Is.False);
        });
    }

    [Test]
    public void ValidateAmount_PositiveAmount_DoesNotThrow()
    {
        // Arrange
        var expense = new Expense { Amount = 100m };

        // Act & Assert
        Assert.DoesNotThrow(() => expense.ValidateAmount());
    }

    [Test]
    public void ValidateAmount_ZeroAmount_ThrowsArgumentException()
    {
        // Arrange
        var expense = new Expense { Amount = 0m };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => expense.ValidateAmount());
        Assert.Multiple(() =>
        {
            Assert.That(ex.ParamName, Is.EqualTo("Amount"));
            Assert.That(ex.Message, Does.Contain("Expense amount must be positive"));
        });
    }

    [Test]
    public void ValidateAmount_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var expense = new Expense { Amount = -50m };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => expense.ValidateAmount());
        Assert.Multiple(() =>
        {
            Assert.That(ex.ParamName, Is.EqualTo("Amount"));
            Assert.That(ex.Message, Does.Contain("Expense amount must be positive"));
        });
    }

    [Test]
    public void UpdateDetails_ValidParameters_UpdatesExpense()
    {
        // Arrange
        var expense = new Expense
        {
            Description = "Old description",
            Amount = 100m,
            Category = ExpenseCategory.Housing
        };

        // Act
        expense.UpdateDetails("New description", 200m, ExpenseCategory.Food);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.Description, Is.EqualTo("New description"));
            Assert.That(expense.Amount, Is.EqualTo(200m));
            Assert.That(expense.Category, Is.EqualTo(ExpenseCategory.Food));
        });
    }

    [Test]
    public void UpdateDetails_EmptyDescription_SetsEmptyDescription()
    {
        // Arrange
        var expense = new Expense
        {
            Description = "Old description",
            Amount = 100m,
            Category = ExpenseCategory.Housing
        };

        // Act
        expense.UpdateDetails("", 150m, ExpenseCategory.Transportation);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.Description, Is.EqualTo(""));
            Assert.That(expense.Amount, Is.EqualTo(150m));
            Assert.That(expense.Category, Is.EqualTo(ExpenseCategory.Transportation));
        });
    }

    [Test]
    public void Category_AllCategories_CanBeAssigned()
    {
        // Arrange
        var expense = new Expense();

        // Act & Assert - Test all enum values
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.Housing);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.Transportation);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.Food);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.Healthcare);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.Entertainment);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.PersonalCare);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.Education);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.DebtPayment);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.Savings);
        Assert.DoesNotThrow(() => expense.Category = ExpenseCategory.Other);
    }

    [Test]
    public void IsRecurring_CanBeToggled()
    {
        // Arrange
        var expense = new Expense { IsRecurring = false };

        // Act
        expense.IsRecurring = true;

        // Assert
        Assert.That(expense.IsRecurring, Is.True);
    }

    [Test]
    public void Payee_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Payee = null
        };

        // Assert
        Assert.That(expense.Payee, Is.Null);
    }

    [Test]
    public void PaymentMethod_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var expense = new Expense
        {
            PaymentMethod = null
        };

        // Assert
        Assert.That(expense.PaymentMethod, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Notes = null
        };

        // Assert
        Assert.That(expense.Notes, Is.Null);
    }

    [Test]
    public void Budget_NavigationProperty_CanBeSet()
    {
        // Arrange
        var budget = new Budget { BudgetId = Guid.NewGuid(), Name = "Test Budget" };
        var expense = new Expense();

        // Act
        expense.Budget = budget;

        // Assert
        Assert.That(expense.Budget, Is.EqualTo(budget));
    }
}
