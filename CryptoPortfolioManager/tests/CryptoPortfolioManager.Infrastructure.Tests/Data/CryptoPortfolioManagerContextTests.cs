// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CryptoPortfolioManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the CryptoPortfolioManagerContext.
/// </summary>
[TestFixture]
public class CryptoPortfolioManagerContextTests
{
    private CryptoPortfolioManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CryptoPortfolioManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CryptoPortfolioManagerContext(options);
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
    /// Tests that Wallets can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Wallets_CanAddAndRetrieve()
    {
        // Arrange
        var wallet = new Wallet
        {
            WalletId = Guid.NewGuid(),
            Name = "Test Wallet",
            Address = "0x123456789",
            WalletType = "Hardware",
            IsActive = true,
            Notes = "Test wallet notes",
        };

        // Act
        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Wallets.FindAsync(wallet.WalletId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Wallet"));
        Assert.That(retrieved.WalletType, Is.EqualTo("Hardware"));
    }

    /// <summary>
    /// Tests that CryptoHoldings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task CryptoHoldings_CanAddAndRetrieve()
    {
        // Arrange
        var wallet = new Wallet
        {
            WalletId = Guid.NewGuid(),
            Name = "Test Wallet",
            WalletType = "Software",
            IsActive = true,
        };

        var holding = new CryptoHolding
        {
            CryptoHoldingId = Guid.NewGuid(),
            WalletId = wallet.WalletId,
            Symbol = "BTC",
            Name = "Bitcoin",
            Quantity = 1.5m,
            AverageCost = 50000m,
            CurrentPrice = 60000m,
            LastPriceUpdate = DateTime.UtcNow,
        };

        // Act
        _context.Wallets.Add(wallet);
        _context.CryptoHoldings.Add(holding);
        await _context.SaveChangesAsync();

        var retrieved = await _context.CryptoHoldings.FindAsync(holding.CryptoHoldingId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Symbol, Is.EqualTo("BTC"));
        Assert.That(retrieved.Quantity, Is.EqualTo(1.5m));
    }

    /// <summary>
    /// Tests that Transactions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Transactions_CanAddAndRetrieve()
    {
        // Arrange
        var wallet = new Wallet
        {
            WalletId = Guid.NewGuid(),
            Name = "Test Wallet",
            WalletType = "Exchange",
            IsActive = true,
        };

        var transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            WalletId = wallet.WalletId,
            TransactionDate = DateTime.UtcNow,
            TransactionType = TransactionType.Buy,
            Symbol = "ETH",
            Quantity = 5m,
            PricePerUnit = 3000m,
            TotalAmount = 15000m,
            Fees = 30m,
            Notes = "Test transaction",
        };

        // Act
        _context.Wallets.Add(wallet);
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Transactions.FindAsync(transaction.TransactionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Symbol, Is.EqualTo("ETH"));
        Assert.That(retrieved.TransactionType, Is.EqualTo(TransactionType.Buy));
    }

    /// <summary>
    /// Tests that TaxLots can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TaxLots_CanAddAndRetrieve()
    {
        // Arrange
        var wallet = new Wallet
        {
            WalletId = Guid.NewGuid(),
            Name = "Test Wallet",
            WalletType = "Hardware",
            IsActive = true,
        };

        var holding = new CryptoHolding
        {
            CryptoHoldingId = Guid.NewGuid(),
            WalletId = wallet.WalletId,
            Symbol = "BTC",
            Name = "Bitcoin",
            Quantity = 1m,
            AverageCost = 55000m,
            CurrentPrice = 60000m,
            LastPriceUpdate = DateTime.UtcNow,
        };

        var taxLot = new TaxLot
        {
            TaxLotId = Guid.NewGuid(),
            CryptoHoldingId = holding.CryptoHoldingId,
            AcquisitionDate = DateTime.UtcNow.AddMonths(-6),
            Quantity = 1m,
            CostBasis = 55000m,
            IsDisposed = false,
        };

        // Act
        _context.Wallets.Add(wallet);
        _context.CryptoHoldings.Add(holding);
        _context.TaxLots.Add(taxLot);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TaxLots.FindAsync(taxLot.TaxLotId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.CostBasis, Is.EqualTo(55000m));
        Assert.That(retrieved.IsDisposed, Is.False);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var wallet = new Wallet
        {
            WalletId = Guid.NewGuid(),
            Name = "Test Wallet",
            WalletType = "Software",
            IsActive = true,
        };

        var holding = new CryptoHolding
        {
            CryptoHoldingId = Guid.NewGuid(),
            WalletId = wallet.WalletId,
            Symbol = "ADA",
            Name = "Cardano",
            Quantity = 100m,
            AverageCost = 1m,
            CurrentPrice = 1.5m,
            LastPriceUpdate = DateTime.UtcNow,
        };

        _context.Wallets.Add(wallet);
        _context.CryptoHoldings.Add(holding);
        await _context.SaveChangesAsync();

        // Act
        _context.Wallets.Remove(wallet);
        await _context.SaveChangesAsync();

        var retrievedHolding = await _context.CryptoHoldings.FindAsync(holding.CryptoHoldingId);

        // Assert
        Assert.That(retrievedHolding, Is.Null);
    }

    /// <summary>
    /// Tests that a wallet can have multiple holdings.
    /// </summary>
    [Test]
    public async Task Wallet_CanHaveMultipleHoldings()
    {
        // Arrange
        var wallet = new Wallet
        {
            WalletId = Guid.NewGuid(),
            Name = "Multi-Asset Wallet",
            WalletType = "Hardware",
            IsActive = true,
        };

        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();

        var holding1 = new CryptoHolding
        {
            CryptoHoldingId = Guid.NewGuid(),
            WalletId = wallet.WalletId,
            Symbol = "BTC",
            Name = "Bitcoin",
            Quantity = 0.5m,
            AverageCost = 50000m,
            CurrentPrice = 60000m,
            LastPriceUpdate = DateTime.UtcNow,
        };

        var holding2 = new CryptoHolding
        {
            CryptoHoldingId = Guid.NewGuid(),
            WalletId = wallet.WalletId,
            Symbol = "ETH",
            Name = "Ethereum",
            Quantity = 10m,
            AverageCost = 3000m,
            CurrentPrice = 3500m,
            LastPriceUpdate = DateTime.UtcNow,
        };

        // Act
        _context.CryptoHoldings.AddRange(holding1, holding2);
        await _context.SaveChangesAsync();

        var holdings = await _context.CryptoHoldings
            .Where(h => h.WalletId == wallet.WalletId)
            .ToListAsync();

        // Assert
        Assert.That(holdings, Has.Count.EqualTo(2));
        Assert.That(holdings.Any(h => h.Symbol == "BTC"), Is.True);
        Assert.That(holdings.Any(h => h.Symbol == "ETH"), Is.True);
    }
}
