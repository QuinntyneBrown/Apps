// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using BillPaymentScheduler.Core.Model.UserAggregate;
using BillPaymentScheduler.Core.Model.UserAggregate.Entities;
using BillPaymentScheduler.Core.Services;
namespace BillPaymentScheduler.Infrastructure;

/// <summary>
/// Provides seed data for the BillPaymentScheduler database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(BillPaymentSchedulerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Payees.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedPayeesAsync(context);
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

    private static async Task SeedPayeesAsync(BillPaymentSchedulerContext context)
    {
        var payees = new List<Payee>
        {
            new Payee
            {
                PayeeId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "City Electric Utility",
                AccountNumber = "123-456-7890",
                Website = "https://cityelectric.example.com",
                PhoneNumber = "(555) 123-4567",
                Notes = "Auto-pay enabled",
            },
            new Payee
            {
                PayeeId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Internet Service Provider",
                AccountNumber = "ISP-999888777",
                Website = "https://isp.example.com",
                PhoneNumber = "(555) 987-6543",
            },
            new Payee
            {
                PayeeId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Water & Sewer Department",
                AccountNumber = "WS-111222333",
                PhoneNumber = "(555) 555-5555",
                Notes = "Quarterly billing",
            },
            new Payee
            {
                PayeeId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Name = "Mobile Phone Company",
                AccountNumber = "MPH-444555666",
                Website = "https://mobile.example.com",
                PhoneNumber = "(555) 321-7654",
            },
        };

        context.Payees.AddRange(payees);

        // Add sample bills
        var bills = new List<Bill>
        {
            new Bill
            {
                BillId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PayeeId = payees[0].PayeeId,
                Name = "Monthly Electric Bill",
                Amount = 125.50m,
                DueDate = new DateTime(2025, 1, 15),
                BillingFrequency = BillingFrequency.Monthly,
                Status = BillStatus.Pending,
                IsAutoPay = true,
            },
            new Bill
            {
                BillId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PayeeId = payees[1].PayeeId,
                Name = "Internet Service",
                Amount = 79.99m,
                DueDate = new DateTime(2025, 1, 10),
                BillingFrequency = BillingFrequency.Monthly,
                Status = BillStatus.Pending,
                IsAutoPay = false,
            },
            new Bill
            {
                BillId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PayeeId = payees[2].PayeeId,
                Name = "Water & Sewer Bill",
                Amount = 65.00m,
                DueDate = new DateTime(2025, 1, 30),
                BillingFrequency = BillingFrequency.Quarterly,
                Status = BillStatus.Pending,
                IsAutoPay = false,
            },
            new Bill
            {
                BillId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PayeeId = payees[3].PayeeId,
                Name = "Mobile Phone Service",
                Amount = 55.00m,
                DueDate = new DateTime(2024, 12, 28),
                BillingFrequency = BillingFrequency.Monthly,
                Status = BillStatus.Overdue,
                IsAutoPay = false,
                Notes = "Payment pending",
            },
        };

        context.Bills.AddRange(bills);

        // Add sample payments
        var payments = new List<Payment>
        {
            new Payment
            {
                PaymentId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BillId = bills[0].BillId,
                Amount = 125.50m,
                PaymentDate = new DateTime(2024, 12, 15),
                ConfirmationNumber = "CONF-123456789",
                PaymentMethod = "Auto-Pay - Bank Account",
            },
            new Payment
            {
                PaymentId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BillId = bills[1].BillId,
                Amount = 79.99m,
                PaymentDate = new DateTime(2024, 12, 9),
                ConfirmationNumber = "CONF-987654321",
                PaymentMethod = "Credit Card",
                Notes = "Paid on time",
            },
        };

        context.Payments.AddRange(payments);

        await context.SaveChangesAsync();
    }
}
