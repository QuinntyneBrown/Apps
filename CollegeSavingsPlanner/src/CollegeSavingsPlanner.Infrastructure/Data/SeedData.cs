// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Core;

using CollegeSavingsPlanner.Core.Models.UserAggregate;
using CollegeSavingsPlanner.Core.Models.UserAggregate.Entities;
using CollegeSavingsPlanner.Core.Services;
namespace CollegeSavingsPlanner.Infrastructure.Data;

/// <summary>
/// Provides seed data for the CollegeSavingsPlanner system.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Gets sample 529 plans for seeding.
    /// </summary>
    /// <returns>A collection of sample plans.</returns>
    public static IEnumerable<Plan> GetSamplePlans()
    {
        return new List<Plan>
        {
            new()
            {
                PlanId = Guid.NewGuid(),
                Name = "New York 529 College Savings Program",
                State = "New York",
                AccountNumber = "NY-529-001234",
                CurrentBalance = 15000.00m,
                OpenedDate = DateTime.UtcNow.AddYears(-3),
                Administrator = "Vanguard",
                IsActive = true,
                Notes = "Direct-sold 529 plan with low fees"
            },
            new()
            {
                PlanId = Guid.NewGuid(),
                Name = "California ScholarShare 529",
                State = "California",
                AccountNumber = "CA-SS-567890",
                CurrentBalance = 25000.00m,
                OpenedDate = DateTime.UtcNow.AddYears(-5),
                Administrator = "TIAA-CREF",
                IsActive = true,
                Notes = "Age-based portfolio option selected"
            },
            new()
            {
                PlanId = Guid.NewGuid(),
                Name = "Utah my529",
                State = "Utah",
                AccountNumber = "UT-529-111222",
                CurrentBalance = 8500.00m,
                OpenedDate = DateTime.UtcNow.AddYears(-2),
                Administrator = "Utah Educational Savings Plan",
                IsActive = true,
                Notes = "One of the top-rated 529 plans nationally"
            }
        };
    }

    /// <summary>
    /// Gets sample contributions for seeding.
    /// </summary>
    /// <param name="planId">The plan ID to associate with the contributions.</param>
    /// <returns>A collection of sample contributions.</returns>
    public static IEnumerable<Contribution> GetSampleContributions(Guid planId)
    {
        return new List<Contribution>
        {
            new()
            {
                ContributionId = Guid.NewGuid(),
                PlanId = planId,
                Amount = 500.00m,
                ContributionDate = DateTime.UtcNow.AddMonths(-3),
                Contributor = "John Smith",
                Notes = "Monthly contribution",
                IsRecurring = true
            },
            new()
            {
                ContributionId = Guid.NewGuid(),
                PlanId = planId,
                Amount = 1000.00m,
                ContributionDate = DateTime.UtcNow.AddMonths(-2),
                Contributor = "Grandparent Gift",
                Notes = "Birthday gift contribution",
                IsRecurring = false
            },
            new()
            {
                ContributionId = Guid.NewGuid(),
                PlanId = planId,
                Amount = 500.00m,
                ContributionDate = DateTime.UtcNow.AddMonths(-1),
                Contributor = "John Smith",
                Notes = "Monthly contribution",
                IsRecurring = true
            },
            new()
            {
                ContributionId = Guid.NewGuid(),
                PlanId = planId,
                Amount = 500.00m,
                ContributionDate = DateTime.UtcNow,
                Contributor = "John Smith",
                Notes = "Monthly contribution",
                IsRecurring = true
            }
        };
    }

    /// <summary>
    /// Gets sample beneficiaries for seeding.
    /// </summary>
    /// <param name="planId">The plan ID to associate with the beneficiaries.</param>
    /// <returns>A collection of sample beneficiaries.</returns>
    public static IEnumerable<Beneficiary> GetSampleBeneficiaries(Guid planId)
    {
        return new List<Beneficiary>
        {
            new()
            {
                BeneficiaryId = Guid.NewGuid(),
                PlanId = planId,
                FirstName = "Emma",
                LastName = "Smith",
                DateOfBirth = DateTime.UtcNow.AddYears(-10),
                Relationship = "Daughter",
                ExpectedCollegeStartYear = DateTime.UtcNow.Year + 8,
                IsPrimary = true
            },
            new()
            {
                BeneficiaryId = Guid.NewGuid(),
                PlanId = planId,
                FirstName = "Liam",
                LastName = "Smith",
                DateOfBirth = DateTime.UtcNow.AddYears(-7),
                Relationship = "Son",
                ExpectedCollegeStartYear = DateTime.UtcNow.Year + 11,
                IsPrimary = false
            }
        };
    }

    /// <summary>
    /// Gets sample projections for seeding.
    /// </summary>
    /// <param name="planId">The plan ID to associate with the projections.</param>
    /// <returns>A collection of sample projections.</returns>
    public static IEnumerable<Projection> GetSampleProjections(Guid planId)
    {
        return new List<Projection>
        {
            new()
            {
                ProjectionId = Guid.NewGuid(),
                PlanId = planId,
                Name = "Conservative Projection (5% return)",
                CurrentSavings = 15000.00m,
                MonthlyContribution = 500.00m,
                ExpectedReturnRate = 5.0m,
                YearsUntilCollege = 8,
                TargetGoal = 100000.00m,
                ProjectedBalance = 78500.00m,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                ProjectionId = Guid.NewGuid(),
                PlanId = planId,
                Name = "Moderate Projection (7% return)",
                CurrentSavings = 15000.00m,
                MonthlyContribution = 500.00m,
                ExpectedReturnRate = 7.0m,
                YearsUntilCollege = 8,
                TargetGoal = 100000.00m,
                ProjectedBalance = 92300.00m,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                ProjectionId = Guid.NewGuid(),
                PlanId = planId,
                Name = "Aggressive Projection (9% return)",
                CurrentSavings = 15000.00m,
                MonthlyContribution = 500.00m,
                ExpectedReturnRate = 9.0m,
                YearsUntilCollege = 8,
                TargetGoal = 100000.00m,
                ProjectedBalance = 108700.00m,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            }
        };
    }
}
