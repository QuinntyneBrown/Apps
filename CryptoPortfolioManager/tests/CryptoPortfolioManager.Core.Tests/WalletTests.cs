namespace CryptoPortfolioManager.Core.Tests;

public class WalletTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWallet()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var name = "My Hardware Wallet";
        var address = "0x1234567890abcdef";
        var walletType = "Hardware";
        var isActive = true;
        var notes = "Cold storage wallet";

        // Act
        var wallet = new Wallet
        {
            WalletId = walletId,
            Name = name,
            Address = address,
            WalletType = walletType,
            IsActive = isActive,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wallet.WalletId, Is.EqualTo(walletId));
            Assert.That(wallet.Name, Is.EqualTo(name));
            Assert.That(wallet.Address, Is.EqualTo(address));
            Assert.That(wallet.WalletType, Is.EqualTo(walletType));
            Assert.That(wallet.IsActive, Is.EqualTo(isActive));
            Assert.That(wallet.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var wallet = new Wallet();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wallet.Name, Is.EqualTo(string.Empty));
            Assert.That(wallet.WalletType, Is.EqualTo(string.Empty));
            Assert.That(wallet.IsActive, Is.True);
            Assert.That(wallet.Address, Is.Null);
            Assert.That(wallet.Notes, Is.Null);
            Assert.That(wallet.Holdings, Is.Not.Null.And.Empty);
        });
    }

    [Test]
    public void CalculateTotalValue_NoHoldings_ReturnsZero()
    {
        // Arrange
        var wallet = new Wallet
        {
            Holdings = new List<CryptoHolding>()
        };

        // Act
        var totalValue = wallet.CalculateTotalValue();

        // Assert
        Assert.That(totalValue, Is.EqualTo(0));
    }

    [Test]
    public void CalculateTotalValue_WithHoldings_ReturnsSumOfMarketValues()
    {
        // Arrange
        var wallet = new Wallet
        {
            Holdings = new List<CryptoHolding>
            {
                new CryptoHolding { Quantity = 1m, CurrentPrice = 50000m },  // 50000
                new CryptoHolding { Quantity = 10m, CurrentPrice = 2000m },  // 20000
                new CryptoHolding { Quantity = 100m, CurrentPrice = 100m }   // 10000
            }
        };

        // Act
        var totalValue = wallet.CalculateTotalValue();

        // Assert
        Assert.That(totalValue, Is.EqualTo(80000m));
    }

    [Test]
    public void CalculateTotalValue_WithOneHolding_ReturnsItsMarketValue()
    {
        // Arrange
        var wallet = new Wallet
        {
            Holdings = new List<CryptoHolding>
            {
                new CryptoHolding { Quantity = 2.5m, CurrentPrice = 1000m }
            }
        };

        // Act
        var totalValue = wallet.CalculateTotalValue();

        // Assert
        Assert.That(totalValue, Is.EqualTo(2500m));
    }

    [Test]
    public void CalculateTotalValue_WithZeroValueHoldings_ReturnsZero()
    {
        // Arrange
        var wallet = new Wallet
        {
            Holdings = new List<CryptoHolding>
            {
                new CryptoHolding { Quantity = 0m, CurrentPrice = 1000m },
                new CryptoHolding { Quantity = 10m, CurrentPrice = 0m }
            }
        };

        // Act
        var totalValue = wallet.CalculateTotalValue();

        // Assert
        Assert.That(totalValue, Is.EqualTo(0));
    }

    [Test]
    public void CalculateTotalValue_WithMixedHoldings_ReturnsCorrectSum()
    {
        // Arrange
        var wallet = new Wallet
        {
            Holdings = new List<CryptoHolding>
            {
                new CryptoHolding { Quantity = 0.5m, CurrentPrice = 50000m },   // 25000
                new CryptoHolding { Quantity = 5.25m, CurrentPrice = 2000m },   // 10500
                new CryptoHolding { Quantity = 1000m, CurrentPrice = 0.50m }    // 500
            }
        };

        // Act
        var totalValue = wallet.CalculateTotalValue();

        // Assert
        Assert.That(totalValue, Is.EqualTo(36000m));
    }

    [Test]
    public void IsActive_CanBeSetToFalse()
    {
        // Arrange & Act
        var wallet = new Wallet
        {
            IsActive = false
        };

        // Assert
        Assert.That(wallet.IsActive, Is.False);
    }

    [Test]
    public void Address_CanBeSet()
    {
        // Arrange
        var address = "0xabcdef1234567890";
        var wallet = new Wallet();

        // Act
        wallet.Address = address;

        // Assert
        Assert.That(wallet.Address, Is.EqualTo(address));
    }

    [Test]
    public void Notes_CanBeSet()
    {
        // Arrange
        var notes = "Primary trading wallet";
        var wallet = new Wallet();

        // Act
        wallet.Notes = notes;

        // Assert
        Assert.That(wallet.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Holdings_CanBePopulated()
    {
        // Arrange
        var wallet = new Wallet();
        var holding1 = new CryptoHolding { CryptoHoldingId = Guid.NewGuid() };
        var holding2 = new CryptoHolding { CryptoHoldingId = Guid.NewGuid() };

        // Act
        wallet.Holdings.Add(holding1);
        wallet.Holdings.Add(holding2);

        // Assert
        Assert.That(wallet.Holdings, Has.Count.EqualTo(2));
    }

    [Test]
    public void CalculateTotalValue_WithDecimalPrecision_ReturnsAccurateValue()
    {
        // Arrange
        var wallet = new Wallet
        {
            Holdings = new List<CryptoHolding>
            {
                new CryptoHolding { Quantity = 1.23456789m, CurrentPrice = 987.654321m }
            }
        };

        // Act
        var totalValue = wallet.CalculateTotalValue();

        // Assert
        Assert.That(totalValue, Is.EqualTo(1219.326320255269m));
    }
}
