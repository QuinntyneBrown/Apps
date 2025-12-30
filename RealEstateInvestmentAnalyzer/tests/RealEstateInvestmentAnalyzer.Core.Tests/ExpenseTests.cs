// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class ExpenseTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesExpense()
    {
        // Arrange & Act
        var expense = new Expense();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.ExpenseId, Is.EqualTo(Guid.Empty));
            Assert.That(expense.PropertyId, Is.EqualTo(Guid.Empty));
            Assert.That(expense.Description, Is.EqualTo(string.Empty));
            Assert.That(expense.Amount, Is.EqualTo(0m));
            Assert.That(expense.Date, Is.EqualTo(default(DateTime)));
            Assert.That(expense.Category, Is.EqualTo(string.Empty));
            Assert.That(expense.IsRecurring, Is.False);
            Assert.That(expense.Notes, Is.Null);
            Assert.That(expense.Property, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesExpense()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var description = "Property Tax";
        var amount = 2500m;
        var date = new DateTime(2024, 6, 1);
        var category = "Tax";

        // Act
        var expense = new Expense
        {
            ExpenseId = expenseId,
            PropertyId = propertyId,
            Description = description,
            Amount = amount,
            Date = date,
            Category = category,
            IsRecurring = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.ExpenseId, Is.EqualTo(expenseId));
            Assert.That(expense.PropertyId, Is.EqualTo(propertyId));
            Assert.That(expense.Description, Is.EqualTo(description));
            Assert.That(expense.Amount, Is.EqualTo(amount));
            Assert.That(expense.Date, Is.EqualTo(date));
            Assert.That(expense.Category, Is.EqualTo(category));
            Assert.That(expense.IsRecurring, Is.True);
        });
    }

    [Test]
    public void Expense_MaintenanceCategory_SetsCorrectly()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Description = "HVAC Repair",
            Amount = 850m,
            Category = "Maintenance"
        };

        // Assert
        Assert.That(expense.Category, Is.EqualTo("Maintenance"));
    }

    [Test]
    public void Expense_UtilityCategory_SetsCorrectly()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Description = "Water Bill",
            Amount = 120m,
            Category = "Utilities"
        };

        // Assert
        Assert.That(expense.Category, Is.EqualTo("Utilities"));
    }

    [Test]
    public void Expense_InsuranceCategory_SetsCorrectly()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Description = "Property Insurance",
            Amount = 1500m,
            Category = "Insurance",
            IsRecurring = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.Category, Is.EqualTo("Insurance"));
            Assert.That(expense.IsRecurring, Is.True);
        });
    }

    [Test]
    public void Expense_RecurringExpense_SetsIsRecurringTrue()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Description = "HOA Fees",
            Amount = 250m,
            Category = "HOA",
            IsRecurring = true
        };

        // Assert
        Assert.That(expense.IsRecurring, Is.True);
    }

    [Test]
    public void Expense_OneTimeExpense_SetsIsRecurringFalse()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Description = "Roof Replacement",
            Amount = 15000m,
            Category = "Maintenance",
            IsRecurring = false
        };

        // Assert
        Assert.That(expense.IsRecurring, Is.False);
    }

    [Test]
    public void Expense_WithNotes_SetsCorrectly()
    {
        // Arrange
        var notes = "Emergency repair needed due to storm damage";

        // Act
        var expense = new Expense
        {
            Description = "Window Replacement",
            Amount = 3500m,
            Notes = notes
        };

        // Assert
        Assert.That(expense.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Expense_SmallAmount_StoresCorrectly()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Description = "Lawn Care",
            Amount = 45.50m,
            Category = "Maintenance"
        };

        // Assert
        Assert.That(expense.Amount, Is.EqualTo(45.50m));
    }

    [Test]
    public void Expense_LargeAmount_StoresCorrectly()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Description = "Major Renovation",
            Amount = 50000m,
            Category = "Improvement"
        };

        // Assert
        Assert.That(expense.Amount, Is.EqualTo(50000m));
    }

    [Test]
    public void Expense_WithAllProperties_SetsAllCorrectly()
    {
        // Arrange & Act
        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            Description = "Complete Expense Record",
            Amount = 1200m,
            Date = new DateTime(2024, 5, 15),
            Category = "Repair",
            IsRecurring = false,
            Notes = "All details included"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.Description, Is.EqualTo("Complete Expense Record"));
            Assert.That(expense.Amount, Is.EqualTo(1200m));
            Assert.That(expense.Date, Is.EqualTo(new DateTime(2024, 5, 15)));
            Assert.That(expense.Category, Is.EqualTo("Repair"));
            Assert.That(expense.IsRecurring, Is.False);
            Assert.That(expense.Notes, Is.EqualTo("All details included"));
        });
    }

    [Test]
    public void Expense_DifferentCategories_StoreCorrectly()
    {
        // Arrange & Act
        var tax = new Expense { Category = "Tax" };
        var insurance = new Expense { Category = "Insurance" };
        var utilities = new Expense { Category = "Utilities" };
        var maintenance = new Expense { Category = "Maintenance" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tax.Category, Is.EqualTo("Tax"));
            Assert.That(insurance.Category, Is.EqualTo("Insurance"));
            Assert.That(utilities.Category, Is.EqualTo("Utilities"));
            Assert.That(maintenance.Category, Is.EqualTo("Maintenance"));
        });
    }
}
