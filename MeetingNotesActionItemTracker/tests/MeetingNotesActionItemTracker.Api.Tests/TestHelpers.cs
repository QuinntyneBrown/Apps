// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq.Expressions;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Tests;

public static class TestHelpers
{
    public static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class
    {
        var queryable = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.Add);
        mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(item => data.Remove(item));

        return mockSet;
    }

    public static Mock<IMeetingNotesActionItemTrackerContext> CreateMockContext()
    {
        var mockContext = new Mock<IMeetingNotesActionItemTrackerContext>();
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(1);
        return mockContext;
    }
}
