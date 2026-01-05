// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using HouseholdBudgetManager.Core.Models.UserAggregate;
using HouseholdBudgetManager.Core.Models.UserAggregate.Entities;
using HouseholdBudgetManager.Core.Services;
namespace HouseholdBudgetManager.Infrastructure;

/// <summary>
/// Provides seed data for the HouseholdBudgetManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HouseholdBudgetManagerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Budgets.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedBudgetsAsync(context);
                logger.LogInformation("Initial data seeded successfully.");
            }
            else
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private static async Task SeedBudgetsAsync(HouseholdBudgetManagerContext context)
    {
        var currentMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

        var budget = new Budget
        {
            BudgetId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Name = "January 2025 Budget",
            Period = "January 2025",
            StartDate = new DateTime(2025, 1, 1),
            EndDate = new DateTime(2025, 1, 31),
            TotalIncome = 5000.00m,
            TotalExpenses = 4200.00m,
            Status = BudgetStatus.Active,
            Notes = "Monthly household budget",
            CreatedAt = DateTime.UtcNow,
        };

        context.Budgets.Add(budget);

        var expenses = new List<Expense>
        {
            new Expense
            {
                ExpenseId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BudgetId = budget.BudgetId,
                Description = "Rent Payment",
                Amount = 1500.00m,
                Category = ExpenseCategory.Housing,
                ExpenseDate = new DateTime(2025, 1, 1),
                Payee = "Landlord",
                PaymentMethod = "Bank Transfer",
                IsRecurring = true,
            },
            new Expense
            {
                ExpenseId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BudgetId = budget.BudgetId,
                Description = "Grocery Shopping",
                Amount = 450.00m,
                Category = ExpenseCategory.Food,
                ExpenseDate = new DateTime(2025, 1, 5),
                Payee = "Supermarket",
                PaymentMethod = "Credit Card",
                IsRecurring = false,
            },
            new Expense
            {
                ExpenseId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BudgetId = budget.BudgetId,
                Description = "Electric Bill",
                Amount = 120.00m,
                Category = ExpenseCategory.Utilities,
                ExpenseDate = new DateTime(2025, 1, 10),
                Payee = "Electric Company",
                PaymentMethod = "Auto-pay",
                IsRecurring = true,
            },
            new Expense
            {
                ExpenseId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BudgetId = budget.BudgetId,
                Description = "Gas Bill",
                Amount = 80.00m,
                Category = ExpenseCategory.Utilities,
                ExpenseDate = new DateTime(2025, 1, 12),
                Payee = "Gas Company",
                PaymentMethod = "Auto-pay",
                IsRecurring = true,
            },
            new Expense
            {
                ExpenseId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BudgetId = budget.BudgetId,
                Description = "Car Insurance",
                Amount = 150.00m,
                Category = ExpenseCategory.Transportation,
                ExpenseDate = new DateTime(2025, 1, 15),
                Payee = "Insurance Company",
                PaymentMethod = "Credit Card",
                IsRecurring = true,
            },
        };

        context.Expenses.AddRange(expenses);

        var incomes = new List<Income>
        {
            new Income
            {
                IncomeId = Guid.Parse("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BudgetId = budget.BudgetId,
                Description = "Monthly Salary",
                Amount = 4500.00m,
                Source = "Employer",
                IncomeDate = new DateTime(2025, 1, 1),
                Notes = "Regular monthly paycheck",
                IsRecurring = true,
            },
            new Income
            {
                IncomeId = Guid.Parse("bbbb1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BudgetId = budget.BudgetId,
                Description = "Freelance Project",
                Amount = 500.00m,
                Source = "Client XYZ",
                IncomeDate = new DateTime(2025, 1, 15),
                Notes = "Web development project",
                IsRecurring = false,
            },
        };

        context.Incomes.AddRange(incomes);

        await context.SaveChangesAsync();
    }
}
