// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestmentPortfolioTracker.Api.Tests;

/// <summary>
/// Helper methods for unit tests.
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
        mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(item => data.Remove(item));

        return mockSet;
    }

    /// <summary>
    /// Creates a mock logger.
    /// </summary>
    public static Mock<ILogger<T>> CreateMockLogger<T>()
    {
        return new Mock<ILogger<T>>();
    }

    /// <summary>
    /// Creates a sample account for testing.
    /// </summary>
    public static Core.Account CreateSampleAccount()
    {
        return new Core.Account
        {
            AccountId = Guid.NewGuid(),
            Name = "Test Account",
            AccountType = AccountType.Taxable,
            Institution = "Test Bank",
            AccountNumber = "123456",
            CurrentBalance = 10000m,
            IsActive = true,
            OpenedDate = DateTime.UtcNow.AddYears(-1),
            Notes = "Test notes"
        };
    }

    /// <summary>
    /// Creates a sample holding for testing.
    /// </summary>
    public static Core.Holding CreateSampleHolding(Guid accountId)
    {
        return new Core.Holding
        {
            HoldingId = Guid.NewGuid(),
            AccountId = accountId,
            Symbol = "AAPL",
            Name = "Apple Inc.",
            Shares = 100m,
            AverageCost = 150m,
            CurrentPrice = 180m,
            LastPriceUpdate = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a sample transaction for testing.
    /// </summary>
    public static Core.Transaction CreateSampleTransaction(Guid accountId, Guid? holdingId = null)
    {
        return new Core.Transaction
        {
            TransactionId = Guid.NewGuid(),
            AccountId = accountId,
            HoldingId = holdingId,
            TransactionDate = DateTime.UtcNow,
            TransactionType = TransactionType.Buy,
            Symbol = "AAPL",
            Shares = 10m,
            PricePerShare = 150m,
            Amount = 1500m,
            Fees = 5m,
            Notes = "Test transaction"
        };
    }

    /// <summary>
    /// Creates a sample dividend for testing.
    /// </summary>
    public static Core.Dividend CreateSampleDividend(Guid holdingId)
    {
        return new Core.Dividend
        {
            DividendId = Guid.NewGuid(),
            HoldingId = holdingId,
            PaymentDate = DateTime.UtcNow,
            ExDividendDate = DateTime.UtcNow.AddDays(-7),
            AmountPerShare = 0.25m,
            TotalAmount = 25m,
            IsReinvested = false,
            Notes = "Test dividend"
        };
    }
}
