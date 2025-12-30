// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Api.Tests;

/// <summary>
/// Helper methods for testing.
/// </summary>
public static class TestHelpers
{
    /// <summary>
    /// Creates an in-memory database context for testing.
    /// </summary>
    /// <returns>A configured in-memory PersonalProjectPipelineContext.</returns>
    public static IPersonalProjectPipelineContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<PersonalProjectPipeline.Infrastructure.PersonalProjectPipelineContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new PersonalProjectPipeline.Infrastructure.PersonalProjectPipelineContext(options);
    }

    /// <summary>
    /// Creates a mock DbSet from a list of items.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="sourceList">The source list.</param>
    /// <returns>A mock DbSet.</returns>
    public static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        return mockSet;
    }
}
