// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using JobSearchOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Infrastructure;

/// <summary>
/// Provides seed data for the JobSearchOrganizer database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(JobSearchOrganizerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Companies.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedJobSearchDataAsync(context);
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

    private static async Task SeedJobSearchDataAsync(JobSearchOrganizerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Seed Companies
        var companies = new List<Company>
        {
            new Company
            {
                CompanyId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "TechCorp Solutions",
                Industry = "Software Development",
                Website = "https://techcorp.example.com",
                Location = "San Francisco, CA",
                CompanySize = "500-1000 employees",
                CultureNotes = "Innovative, fast-paced environment with focus on work-life balance",
                ResearchNotes = "Leading provider of cloud solutions, recently raised Series C funding",
                IsTargetCompany = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Company
            {
                CompanyId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "DataStream Analytics",
                Industry = "Data Science",
                Website = "https://datastream.example.com",
                Location = "New York, NY",
                CompanySize = "100-500 employees",
                CultureNotes = "Data-driven culture, emphasis on continuous learning",
                ResearchNotes = "Specializes in big data analytics for financial services",
                IsTargetCompany = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Company
            {
                CompanyId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "InnovateTech Inc",
                Industry = "Artificial Intelligence",
                Website = "https://innovatetech.example.com",
                Location = "Austin, TX",
                CompanySize = "50-100 employees",
                CultureNotes = "Startup culture, flexible hours, hybrid work",
                ResearchNotes = "AI/ML startup focused on healthcare applications",
                IsTargetCompany = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Companies.AddRange(companies);

        // Seed Applications
        var applications = new List<Application>
        {
            new Application
            {
                ApplicationId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                CompanyId = companies[0].CompanyId,
                JobTitle = "Senior Software Engineer",
                JobUrl = "https://techcorp.example.com/jobs/123",
                Status = ApplicationStatus.Interview,
                AppliedDate = DateTime.UtcNow.AddDays(-20),
                SalaryRange = "$140,000 - $180,000",
                Location = "San Francisco, CA",
                JobType = "Full-time",
                IsRemote = false,
                ContactPerson = "Jane Smith, Engineering Manager",
                Notes = "Great opportunity, aligns well with my skills",
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                UpdatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new Application
            {
                ApplicationId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                CompanyId = companies[1].CompanyId,
                JobTitle = "Data Engineer",
                JobUrl = "https://datastream.example.com/careers/de-456",
                Status = ApplicationStatus.Applied,
                AppliedDate = DateTime.UtcNow.AddDays(-10),
                SalaryRange = "$130,000 - $160,000",
                Location = "New York, NY",
                JobType = "Full-time",
                IsRemote = true,
                ContactPerson = "Mike Johnson, Head of Engineering",
                Notes = "Remote position, strong compensation",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
            },
            new Application
            {
                ApplicationId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                CompanyId = companies[2].CompanyId,
                JobTitle = "Machine Learning Engineer",
                JobUrl = "https://innovatetech.example.com/jobs/mle-789",
                Status = ApplicationStatus.Screening,
                AppliedDate = DateTime.UtcNow.AddDays(-15),
                SalaryRange = "$120,000 - $150,000",
                Location = "Austin, TX",
                JobType = "Full-time",
                IsRemote = true,
                ContactPerson = "Sarah Lee, CTO",
                Notes = "Startup environment, equity opportunities",
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-7),
            },
        };

        context.Applications.AddRange(applications);

        // Seed Interviews
        var interviews = new List<Interview>
        {
            new Interview
            {
                InterviewId = Guid.Parse("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ApplicationId = applications[0].ApplicationId,
                InterviewType = "Phone Screen",
                ScheduledDateTime = DateTime.UtcNow.AddDays(-12),
                DurationMinutes = 30,
                Interviewers = new List<string> { "Jane Smith" },
                Location = "Phone Call",
                PreparationNotes = "Review cloud architecture experience, prepare STAR examples",
                Feedback = "Went well, discussed past projects in detail",
                IsCompleted = true,
                CompletedDate = DateTime.UtcNow.AddDays(-12),
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-12),
            },
            new Interview
            {
                InterviewId = Guid.Parse("bbbb1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ApplicationId = applications[0].ApplicationId,
                InterviewType = "Technical Interview",
                ScheduledDateTime = DateTime.UtcNow.AddDays(-5),
                DurationMinutes = 90,
                Interviewers = new List<string> { "John Doe", "Alice Brown" },
                Location = "Zoom Meeting",
                PreparationNotes = "System design and coding challenges, review distributed systems",
                Feedback = "Challenging but fair, covered microservices architecture",
                IsCompleted = true,
                CompletedDate = DateTime.UtcNow.AddDays(-5),
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                UpdatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new Interview
            {
                InterviewId = Guid.Parse("cccc1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ApplicationId = applications[0].ApplicationId,
                InterviewType = "Final Interview",
                ScheduledDateTime = DateTime.UtcNow.AddDays(3),
                DurationMinutes = 60,
                Interviewers = new List<string> { "Robert Chen, VP Engineering" },
                Location = "On-site - San Francisco Office",
                PreparationNotes = "Prepare questions about team culture and growth opportunities",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new Interview
            {
                InterviewId = Guid.Parse("dddd1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ApplicationId = applications[2].ApplicationId,
                InterviewType = "Phone Screen",
                ScheduledDateTime = DateTime.UtcNow.AddDays(2),
                DurationMinutes = 30,
                Interviewers = new List<string> { "Sarah Lee" },
                Location = "Phone Call",
                PreparationNotes = "Research ML projects, prepare to discuss healthcare AI applications",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
        };

        context.Interviews.AddRange(interviews);

        // Seed Offers
        var offers = new List<Offer>
        {
            new Offer
            {
                OfferId = Guid.Parse("eeee1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ApplicationId = applications[0].ApplicationId,
                Salary = 165000m,
                Currency = "USD",
                Bonus = 20000m,
                Equity = "RSUs worth $50,000 vesting over 4 years",
                Benefits = "Health, Dental, Vision, 401k match, Unlimited PTO",
                VacationDays = null,
                OfferDate = DateTime.UtcNow.AddDays(-2),
                ExpirationDate = DateTime.UtcNow.AddDays(5),
                IsAccepted = false,
                Notes = "Competitive offer, considering other opportunities",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
        };

        context.Offers.AddRange(offers);

        await context.SaveChangesAsync();
    }
}
