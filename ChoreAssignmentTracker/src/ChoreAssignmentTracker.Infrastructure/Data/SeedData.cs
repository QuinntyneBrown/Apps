// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;

namespace ChoreAssignmentTracker.Infrastructure.Data;

/// <summary>
/// Provides seed data for the ChoreAssignmentTracker system.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Gets sample family members for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <returns>A collection of sample family members.</returns>
    public static IEnumerable<FamilyMember> GetSampleFamilyMembers(Guid userId)
    {
        return new List<FamilyMember>
        {
            new()
            {
                FamilyMemberId = Guid.NewGuid(),
                UserId = userId,
                Name = "Emma",
                Age = 12,
                Avatar = "girl-icon",
                TotalPoints = 150,
                AvailablePoints = 75,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                FamilyMemberId = Guid.NewGuid(),
                UserId = userId,
                Name = "Liam",
                Age = 9,
                Avatar = "boy-icon",
                TotalPoints = 100,
                AvailablePoints = 50,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                FamilyMemberId = Guid.NewGuid(),
                UserId = userId,
                Name = "Olivia",
                Age = 15,
                Avatar = "teen-girl-icon",
                TotalPoints = 220,
                AvailablePoints = 120,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            }
        };
    }

    /// <summary>
    /// Gets sample chores for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <returns>A collection of sample chores.</returns>
    public static IEnumerable<Chore> GetSampleChores(Guid userId)
    {
        return new List<Chore>
        {
            new()
            {
                ChoreId = Guid.NewGuid(),
                UserId = userId,
                Name = "Wash Dishes",
                Description = "Clean and dry all dishes after dinner",
                Frequency = ChoreFrequency.Daily,
                EstimatedMinutes = 20,
                Points = 10,
                Category = "Kitchen",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new()
            {
                ChoreId = Guid.NewGuid(),
                UserId = userId,
                Name = "Vacuum Living Room",
                Description = "Vacuum the entire living room and hallway",
                Frequency = ChoreFrequency.Weekly,
                EstimatedMinutes = 30,
                Points = 20,
                Category = "Cleaning",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new()
            {
                ChoreId = Guid.NewGuid(),
                UserId = userId,
                Name = "Take Out Trash",
                Description = "Take all trash bins to the curb",
                Frequency = ChoreFrequency.BiWeekly,
                EstimatedMinutes = 10,
                Points = 15,
                Category = "Outdoor",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new()
            {
                ChoreId = Guid.NewGuid(),
                UserId = userId,
                Name = "Clean Bedroom",
                Description = "Organize and clean bedroom, including making bed",
                Frequency = ChoreFrequency.Weekly,
                EstimatedMinutes = 40,
                Points = 25,
                Category = "Bedroom",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            }
        };
    }

    /// <summary>
    /// Gets sample rewards for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <returns>A collection of sample rewards.</returns>
    public static IEnumerable<Reward> GetSampleRewards(Guid userId)
    {
        return new List<Reward>
        {
            new()
            {
                RewardId = Guid.NewGuid(),
                UserId = userId,
                Name = "Extra Screen Time (30 min)",
                Description = "30 extra minutes of screen time",
                PointCost = 50,
                Category = "Entertainment",
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new()
            {
                RewardId = Guid.NewGuid(),
                UserId = userId,
                Name = "Choose Dinner Menu",
                Description = "Pick what's for dinner one night",
                PointCost = 75,
                Category = "Food",
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new()
            {
                RewardId = Guid.NewGuid(),
                UserId = userId,
                Name = "Movie Night Pick",
                Description = "Choose the movie for family movie night",
                PointCost = 100,
                Category = "Entertainment",
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new()
            {
                RewardId = Guid.NewGuid(),
                UserId = userId,
                Name = "Sleepover Permission",
                Description = "Have a friend over for a sleepover",
                PointCost = 150,
                Category = "Social",
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            }
        };
    }
}
