// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the InvestmentPortfolioTrackerContext.
/// </summary>
[TestFixture]
public class InvestmentPortfolioTrackerContextTests
{
    private InvestmentPortfolioTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<InvestmentPortfolioTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new InvestmentPortfolioTrackerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Accounts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Accounts_CanAddAndRetrieve()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            Name = "Test IRA",
            AccountType = AccountType.IRA,
            Institution = "Vanguard",
            CurrentBalance = 50000m,
            IsActive = true,
            OpenedDate = DateTime.UtcNow,
        };

        // Act
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Accounts.FindAsync(account.AccountId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test IRA"));
        Assert.That(retrieved.AccountType, Is.EqualTo(AccountType.IRA));
        Assert.That(retrieved.Institution, Is.EqualTo("Vanguard"));
    }

    /// <summary>
    /// Tests that Holdings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Holdings_CanAddAndRetrieve()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            Name = "Test Account",
            AccountType = AccountType.Brokerage,
            Institution = "Fidelity",
            CurrentBalance = 25000m,
            IsActive = true,
            OpenedDate = DateTime.UtcNow,
        };

        var holding = new Holding
        {
            HoldingId = Guid.NewGuid(),
            AccountId = account.AccountId,
            Symbol = "AAPL",
            Name = "Apple Inc.",
            Shares = 100m,
            AverageCost = 150.00m,
            CurrentPrice = 175.00m,
            LastPriceUpdate = DateTime.UtcNow,
        };

        // Act
        _context.Accounts.Add(account);
        _context.Holdings.Add(holding);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Holdings.FindAsync(holding.HoldingId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Symbol, Is.EqualTo("AAPL"));
        Assert.That(retrieved.Shares, Is.EqualTo(100m));
        Assert.That(retrieved.CurrentPrice, Is.EqualTo(175.00m));
    }

    /// <summary>
    /// Tests that Transactions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Transactions_CanAddAndRetrieve()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            Name = "Test Account",
            AccountType = AccountType.Brokerage,
            Institution = "Fidelity",
            CurrentBalance = 25000m,
            IsActive = true,
            OpenedDate = DateTime.UtcNow,
        };

        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            AccountId = account.AccountId,
            TransactionDate = DateTime.UtcNow,
            TransactionType = TransactionType.Buy,
            Symbol = "MSFT",
            Shares = 50m,
            PricePerShare = 300.00m,
            Amount = 15000.00m,
            Fees = 6.95m,
        };

        // Act
        _context.Accounts.Add(account);
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Transactions.FindAsync(transaction.TransactionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Symbol, Is.EqualTo("MSFT"));
        Assert.That(retrieved.TransactionType, Is.EqualTo(TransactionType.Buy));
        Assert.That(retrieved.Amount, Is.EqualTo(15000.00m));
    }

    /// <summary>
    /// Tests that Dividends can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Dividends_CanAddAndRetrieve()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            Name = "Test Account",
            AccountType = AccountType.Brokerage,
            Institution = "Fidelity",
            CurrentBalance = 25000m,
            IsActive = true,
            OpenedDate = DateTime.UtcNow,
        };

        var holding = new Holding
        {
            HoldingId = Guid.NewGuid(),
            AccountId = account.AccountId,
            Symbol = "AAPL",
            Name = "Apple Inc.",
            Shares = 100m,
            AverageCost = 150.00m,
            CurrentPrice = 175.00m,
            LastPriceUpdate = DateTime.UtcNow,
        };

        var dividend = new Dividend
        {
            DividendId = Guid.NewGuid(),
            HoldingId = holding.HoldingId,
            PaymentDate = DateTime.UtcNow,
            ExDividendDate = DateTime.UtcNow.AddDays(-10),
            AmountPerShare = 0.24m,
            TotalAmount = 24.00m,
            IsReinvested = false,
        };

        // Act
        _context.Accounts.Add(account);
        _context.Holdings.Add(holding);
        _context.Dividends.Add(dividend);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Dividends.FindAsync(dividend.DividendId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.AmountPerShare, Is.EqualTo(0.24m));
        Assert.That(retrieved.TotalAmount, Is.EqualTo(24.00m));
        Assert.That(retrieved.IsReinvested, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            Name = "Test Account",
            AccountType = AccountType.Brokerage,
            Institution = "Fidelity",
            CurrentBalance = 25000m,
            IsActive = true,
            OpenedDate = DateTime.UtcNow,
        };

        var holding = new Holding
        {
            HoldingId = Guid.NewGuid(),
            AccountId = account.AccountId,
            Symbol = "AAPL",
            Name = "Apple Inc.",
            Shares = 100m,
            AverageCost = 150.00m,
            CurrentPrice = 175.00m,
            LastPriceUpdate = DateTime.UtcNow,
        };

        _context.Accounts.Add(account);
        _context.Holdings.Add(holding);
        await _context.SaveChangesAsync();

        // Act
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        var retrievedHolding = await _context.Holdings.FindAsync(holding.HoldingId);

        // Assert
        Assert.That(retrievedHolding, Is.Null);
    }
}
