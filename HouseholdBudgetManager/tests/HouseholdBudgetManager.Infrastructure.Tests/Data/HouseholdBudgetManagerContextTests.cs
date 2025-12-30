// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HouseholdBudgetManagerContext.
/// </summary>
[TestFixture]
public class HouseholdBudgetManagerContextTests
{
    private HouseholdBudgetManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HouseholdBudgetManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HouseholdBudgetManagerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Budgets can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Budgets_CanAddAndRetrieve()
    {
        // Arrange
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            Name = "Test Budget",
            Period = "January 2025",
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 31),
            TotalIncome = 5000m,
            TotalExpenses = 3000m,
            Status = BudgetStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Budgets.FindAsync(budget.BudgetId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Budget"));
        Assert.That(retrieved.Period, Is.EqualTo("January 2025"));
        Assert.That(retrieved.Status, Is.EqualTo(BudgetStatus.Active));
    }

    /// <summary>
    /// Tests that Expenses can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Expenses_CanAddAndRetrieve()
    {
        // Arrange
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            Name = "Test Budget",
            Period = "January 2025",
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 31),
            TotalIncome = 5000m,
            TotalExpenses = 3000m,
            Status = BudgetStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            BudgetId = budget.BudgetId,
            Description = "Test Expense",
            Amount = 100.00m,
            Category = ExpenseCategory.Food,
            ExpenseDate = DateTime.UtcNow,
            Payee = "Test Store",
            PaymentMethod = "Credit Card",
            IsRecurring = false,
        };

        // Act
        _context.Budgets.Add(budget);
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Expenses.FindAsync(expense.ExpenseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Test Expense"));
        Assert.That(retrieved.Amount, Is.EqualTo(100.00m));
        Assert.That(retrieved.Category, Is.EqualTo(ExpenseCategory.Food));
    }

    /// <summary>
    /// Tests that Incomes can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Incomes_CanAddAndRetrieve()
    {
        // Arrange
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            Name = "Test Budget",
            Period = "January 2025",
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 31),
            TotalIncome = 5000m,
            TotalExpenses = 3000m,
            Status = BudgetStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        var income = new Income
        {
            IncomeId = Guid.NewGuid(),
            BudgetId = budget.BudgetId,
            Description = "Test Income",
            Amount = 5000.00m,
            Source = "Employer",
            IncomeDate = DateTime.UtcNow,
            Notes = "Monthly salary",
            IsRecurring = true,
        };

        // Act
        _context.Budgets.Add(budget);
        _context.Incomes.Add(income);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Incomes.FindAsync(income.IncomeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Test Income"));
        Assert.That(retrieved.Amount, Is.EqualTo(5000.00m));
        Assert.That(retrieved.Source, Is.EqualTo("Employer"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            Name = "Test Budget",
            Period = "January 2025",
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 31),
            TotalIncome = 5000m,
            TotalExpenses = 3000m,
            Status = BudgetStatus.Active,
            CreatedAt = DateTime.UtcNow,
        };

        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            BudgetId = budget.BudgetId,
            Description = "Test Expense",
            Amount = 100.00m,
            Category = ExpenseCategory.Food,
            ExpenseDate = DateTime.UtcNow,
            IsRecurring = false,
        };

        _context.Budgets.Add(budget);
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();

        // Act
        _context.Budgets.Remove(budget);
        await _context.SaveChangesAsync();

        var retrievedExpense = await _context.Expenses.FindAsync(expense.ExpenseId);

        // Assert
        Assert.That(retrievedExpense, Is.Null);
    }
}
