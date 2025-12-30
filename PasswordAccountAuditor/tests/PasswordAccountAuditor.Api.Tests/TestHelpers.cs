// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Api.Tests;

/// <summary>
/// Helper methods for testing.
/// </summary>
public static class TestHelpers
{
    /// <summary>
    /// Creates a mock DbSet from a list of entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="data">The list of entities.</param>
    /// <returns>A mock DbSet.</returns>
    public static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class
    {
        var queryable = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

        mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.Add);
        mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(entity => data.Remove(entity));

        return mockSet;
    }

    /// <summary>
    /// Creates a mock IPasswordAccountAuditorContext with in-memory data.
    /// </summary>
    /// <param name="accounts">Optional list of accounts.</param>
    /// <param name="breachAlerts">Optional list of breach alerts.</param>
    /// <param name="securityAudits">Optional list of security audits.</param>
    /// <returns>A mock context.</returns>
    public static Mock<IPasswordAccountAuditorContext> CreateMockContext(
        List<Core.Account>? accounts = null,
        List<Core.BreachAlert>? breachAlerts = null,
        List<Core.SecurityAudit>? securityAudits = null)
    {
        var mockContext = new Mock<IPasswordAccountAuditorContext>();

        var accountData = accounts ?? new List<Core.Account>();
        var breachAlertData = breachAlerts ?? new List<Core.BreachAlert>();
        var securityAuditData = securityAudits ?? new List<Core.SecurityAudit>();

        mockContext.Setup(c => c.Accounts).Returns(CreateMockDbSet(accountData).Object);
        mockContext.Setup(c => c.BreachAlerts).Returns(CreateMockDbSet(breachAlertData).Object);
        mockContext.Setup(c => c.SecurityAudits).Returns(CreateMockDbSet(securityAuditData).Object);

        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        return mockContext;
    }
}
