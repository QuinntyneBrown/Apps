// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Api.Tests;

/// <summary>
/// Helper methods for creating test doubles and mocks.
/// </summary>
public static class TestHelpers
{
    /// <summary>
    /// Creates a mock DbSet from a list of entities.
    /// </summary>
    public static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class
    {
        var queryable = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.Add);
        mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(entity => data.Remove(entity));

        return mockSet;
    }

    /// <summary>
    /// Creates a mock IKidsActivitySportsTrackerContext with in-memory DbSets.
    /// </summary>
    public static Mock<IKidsActivitySportsTrackerContext> CreateMockContext()
    {
        var mockContext = new Mock<IKidsActivitySportsTrackerContext>();

        var activities = new List<Core.Activity>();
        var schedules = new List<Core.Schedule>();
        var carpools = new List<Core.Carpool>();

        mockContext.Setup(c => c.Activities).Returns(CreateMockDbSet(activities).Object);
        mockContext.Setup(c => c.Schedules).Returns(CreateMockDbSet(schedules).Object);
        mockContext.Setup(c => c.Carpools).Returns(CreateMockDbSet(carpools).Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        return mockContext;
    }
}
