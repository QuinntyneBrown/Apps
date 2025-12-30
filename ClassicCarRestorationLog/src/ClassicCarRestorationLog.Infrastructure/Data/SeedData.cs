// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;

namespace ClassicCarRestorationLog.Infrastructure.Data;

/// <summary>
/// Provides seed data for the ClassicCarRestorationLog system.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Gets sample projects for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <returns>A collection of sample projects.</returns>
    public static IEnumerable<Project> GetSampleProjects(Guid userId)
    {
        return new List<Project>
        {
            new()
            {
                ProjectId = Guid.NewGuid(),
                UserId = userId,
                CarMake = "Ford",
                CarModel = "Mustang",
                Year = 1967,
                Phase = ProjectPhase.Repair,
                StartDate = DateTime.UtcNow.AddMonths(-6),
                EstimatedBudget = 25000m,
                ActualCost = 18500m,
                Notes = "Classic muscle car restoration project. Engine and transmission work in progress.",
                CreatedAt = DateTime.UtcNow.AddMonths(-6)
            },
            new()
            {
                ProjectId = Guid.NewGuid(),
                UserId = userId,
                CarMake = "Chevrolet",
                CarModel = "Corvette",
                Year = 1972,
                Phase = ProjectPhase.Planning,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EstimatedBudget = 35000m,
                ActualCost = 5000m,
                Notes = "Starting restoration on this Stingray. Purchased at auction.",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                ProjectId = Guid.NewGuid(),
                UserId = userId,
                CarMake = "Porsche",
                CarModel = "911",
                Year = 1985,
                Phase = ProjectPhase.Completed,
                StartDate = DateTime.UtcNow.AddYears(-2),
                CompletionDate = DateTime.UtcNow.AddMonths(-1),
                EstimatedBudget = 40000m,
                ActualCost = 42500m,
                Notes = "Complete restoration finished. Car is now in pristine condition.",
                CreatedAt = DateTime.UtcNow.AddYears(-2)
            }
        };
    }

    /// <summary>
    /// Gets sample parts for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <param name="projectId">The project ID to associate with the parts.</param>
    /// <returns>A collection of sample parts.</returns>
    public static IEnumerable<Part> GetSampleParts(Guid userId, Guid projectId)
    {
        return new List<Part>
        {
            new()
            {
                PartId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                Name = "Carburetor Rebuild Kit",
                PartNumber = "CARB-1967-KIT",
                Supplier = "Classic Mustang Parts Co.",
                Cost = 150.00m,
                OrderedDate = DateTime.UtcNow.AddDays(-45),
                ReceivedDate = DateTime.UtcNow.AddDays(-38),
                IsInstalled = true,
                Notes = "Complete rebuild kit with all gaskets and seals",
                CreatedAt = DateTime.UtcNow.AddDays(-45)
            },
            new()
            {
                PartId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                Name = "Front Brake Pads",
                PartNumber = "BRK-FRT-67",
                Supplier = "AutoZone",
                Cost = 89.99m,
                OrderedDate = DateTime.UtcNow.AddDays(-30),
                ReceivedDate = DateTime.UtcNow.AddDays(-25),
                IsInstalled = false,
                Notes = "High-performance ceramic brake pads",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                PartId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                Name = "Windshield Weatherstrip",
                PartNumber = "WS-1967-SEAL",
                Supplier = "Classic Mustang Parts Co.",
                Cost = 65.00m,
                OrderedDate = DateTime.UtcNow.AddDays(-20),
                ReceivedDate = DateTime.UtcNow.AddDays(-15),
                IsInstalled = false,
                Notes = "OEM quality replacement weatherstrip",
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            }
        };
    }

    /// <summary>
    /// Gets sample work logs for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <param name="projectId">The project ID to associate with the work logs.</param>
    /// <returns>A collection of sample work logs.</returns>
    public static IEnumerable<WorkLog> GetSampleWorkLogs(Guid userId, Guid projectId)
    {
        return new List<WorkLog>
        {
            new()
            {
                WorkLogId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                WorkDate = DateTime.UtcNow.AddDays(-7),
                HoursWorked = 4,
                Description = "Engine disassembly",
                WorkPerformed = "Removed engine from vehicle. Drained all fluids. Tagged and bagged all bolts and hardware for reassembly.",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new()
            {
                WorkLogId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                WorkDate = DateTime.UtcNow.AddDays(-5),
                HoursWorked = 3,
                Description = "Carburetor rebuild",
                WorkPerformed = "Completely disassembled and cleaned carburetor. Installed new gaskets and jets from rebuild kit.",
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new()
            {
                WorkLogId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                WorkDate = DateTime.UtcNow.AddDays(-2),
                HoursWorked = 5,
                Description = "Interior cleaning",
                WorkPerformed = "Deep cleaned all interior surfaces. Removed old carpet. Started prep work for new carpet installation.",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            }
        };
    }

    /// <summary>
    /// Gets sample photo logs for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <param name="projectId">The project ID to associate with the photo logs.</param>
    /// <returns>A collection of sample photo logs.</returns>
    public static IEnumerable<PhotoLog> GetSamplePhotoLogs(Guid userId, Guid projectId)
    {
        return new List<PhotoLog>
        {
            new()
            {
                PhotoLogId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                PhotoDate = DateTime.UtcNow.AddMonths(-6),
                Description = "Initial condition - exterior front view",
                PhotoUrl = "/photos/mustang-initial-front.jpg",
                Phase = ProjectPhase.Planning,
                CreatedAt = DateTime.UtcNow.AddMonths(-6)
            },
            new()
            {
                PhotoLogId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                PhotoDate = DateTime.UtcNow.AddMonths(-5),
                Description = "Engine bay before disassembly",
                PhotoUrl = "/photos/mustang-engine-before.jpg",
                Phase = ProjectPhase.Disassembly,
                CreatedAt = DateTime.UtcNow.AddMonths(-5)
            },
            new()
            {
                PhotoLogId = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId,
                PhotoDate = DateTime.UtcNow.AddDays(-7),
                Description = "Engine removed from vehicle",
                PhotoUrl = "/photos/mustang-engine-out.jpg",
                Phase = ProjectPhase.Repair,
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            }
        };
    }
}
