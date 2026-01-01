// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalLoanComparisonTool.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PersonalLoanComparisonTool.Core.Model.UserAggregate;
using PersonalLoanComparisonTool.Core.Model.UserAggregate.Entities;
using PersonalLoanComparisonTool.Core.Services;
namespace PersonalLoanComparisonTool.Infrastructure;

/// <summary>
/// Provides seed data for the PersonalLoanComparisonTool database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PersonalLoanComparisonToolContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Loans.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedLoansAsync(context);
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

    private static async Task SeedLoansAsync(PersonalLoanComparisonToolContext context)
    {
        // Add sample loans
        var loans = new List<Loan>
        {
            new Loan
            {
                LoanId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Home Renovation Loan",
                LoanType = LoanType.Personal,
                RequestedAmount = 25000m,
                Purpose = "Kitchen and bathroom remodeling",
                CreditScore = 720,
                Notes = "Shopping for best rate for home improvement project",
            },
            new Loan
            {
                LoanId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Car Purchase Loan",
                LoanType = LoanType.Auto,
                RequestedAmount = 30000m,
                Purpose = "New vehicle purchase",
                CreditScore = 680,
                Notes = "Looking for pre-approval for car shopping",
            },
            new Loan
            {
                LoanId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Debt Consolidation",
                LoanType = LoanType.DebtConsolidation,
                RequestedAmount = 15000m,
                Purpose = "Consolidate credit card debt",
                CreditScore = 650,
                Notes = "Need to reduce monthly payments and interest",
            },
        };

        context.Loans.AddRange(loans);

        // Add sample offers for the first loan
        var offers = new List<Offer>
        {
            new Offer
            {
                OfferId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                LoanId = loans[0].LoanId,
                LenderName = "First National Bank",
                LoanAmount = 25000m,
                InterestRate = 6.99m,
                TermMonths = 60,
                MonthlyPayment = 495.00m,
                TotalCost = 29700m,
                Fees = 250m,
                Notes = "No prepayment penalty",
            },
            new Offer
            {
                OfferId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                LoanId = loans[0].LoanId,
                LenderName = "Community Credit Union",
                LoanAmount = 25000m,
                InterestRate = 5.99m,
                TermMonths = 60,
                MonthlyPayment = 483.00m,
                TotalCost = 28980m,
                Fees = 0m,
                Notes = "Existing member rate, no fees",
            },
            new Offer
            {
                OfferId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                LoanId = loans[0].LoanId,
                LenderName = "Online Lender Plus",
                LoanAmount = 25000m,
                InterestRate = 7.49m,
                TermMonths = 60,
                MonthlyPayment = 501.00m,
                TotalCost = 30560m,
                Fees = 500m,
                Notes = "Quick approval, higher rate",
            },
        };

        context.Offers.AddRange(offers);

        // Add sample payment schedule for the best offer
        var bestOffer = offers[1]; // Community Credit Union
        var paymentSchedules = new List<PaymentSchedule>();

        decimal remainingBalance = bestOffer.LoanAmount;
        DateTime dueDate = DateTime.UtcNow.AddMonths(1).Date;

        for (int i = 1; i <= Math.Min(12, bestOffer.TermMonths); i++)
        {
            decimal interestAmount = remainingBalance * (bestOffer.InterestRate / 100 / 12);
            decimal principalAmount = bestOffer.MonthlyPayment - interestAmount;
            remainingBalance -= principalAmount;

            paymentSchedules.Add(new PaymentSchedule
            {
                PaymentScheduleId = Guid.NewGuid(),
                OfferId = bestOffer.OfferId,
                PaymentNumber = i,
                DueDate = dueDate,
                PaymentAmount = bestOffer.MonthlyPayment,
                PrincipalAmount = Math.Round(principalAmount, 2),
                InterestAmount = Math.Round(interestAmount, 2),
                RemainingBalance = Math.Round(Math.Max(remainingBalance, 0), 2),
            });

            dueDate = dueDate.AddMonths(1);
        }

        context.PaymentSchedules.AddRange(paymentSchedules);

        await context.SaveChangesAsync();
    }
}
