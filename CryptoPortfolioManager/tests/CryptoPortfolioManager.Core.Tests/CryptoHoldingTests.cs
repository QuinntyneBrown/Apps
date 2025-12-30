namespace CryptoPortfolioManager.Core.Tests;

public class CryptoHoldingTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCryptoHolding()
    {
        // Arrange
        var holdingId = Guid.NewGuid();
        var walletId = Guid.NewGuid();
        var symbol = "BTC";
        var name = "Bitcoin";
        var quantity = 1.5m;
        var averageCost = 45000m;
        var currentPrice = 50000m;

        // Act
        var holding = new CryptoHolding
        {
            CryptoHoldingId = holdingId,
            WalletId = walletId,
            Symbol = symbol,
            Name = name,
            Quantity = quantity,
            AverageCost = averageCost,
            CurrentPrice = currentPrice
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holding.CryptoHoldingId, Is.EqualTo(holdingId));
            Assert.That(holding.WalletId, Is.EqualTo(walletId));
            Assert.That(holding.Symbol, Is.EqualTo(symbol));
            Assert.That(holding.Name, Is.EqualTo(name));
            Assert.That(holding.Quantity, Is.EqualTo(quantity));
            Assert.That(holding.AverageCost, Is.EqualTo(averageCost));
            Assert.That(holding.CurrentPrice, Is.EqualTo(currentPrice));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var holding = new CryptoHolding();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holding.Symbol, Is.EqualTo(string.Empty));
            Assert.That(holding.Name, Is.EqualTo(string.Empty));
            Assert.That(holding.Quantity, Is.EqualTo(0));
            Assert.That(holding.AverageCost, Is.EqualTo(0));
            Assert.That(holding.CurrentPrice, Is.EqualTo(0));
            Assert.That(holding.LastPriceUpdate, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void CalculateMarketValue_ReturnsCorrectValue()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            Quantity = 2.5m,
            CurrentPrice = 1000m
        };

        // Act
        var marketValue = holding.CalculateMarketValue();

        // Assert
        Assert.That(marketValue, Is.EqualTo(2500m));
    }

    [Test]
    public void CalculateMarketValue_WithZeroQuantity_ReturnsZero()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            Quantity = 0,
            CurrentPrice = 1000m
        };

        // Act
        var marketValue = holding.CalculateMarketValue();

        // Assert
        Assert.That(marketValue, Is.EqualTo(0));
    }

    [Test]
    public void CalculateMarketValue_WithZeroPrice_ReturnsZero()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            Quantity = 2.5m,
            CurrentPrice = 0
        };

        // Act
        var marketValue = holding.CalculateMarketValue();

        // Assert
        Assert.That(marketValue, Is.EqualTo(0));
    }

    [Test]
    public void CalculateUnrealizedGainLoss_PriceIncreased_ReturnsPositiveGain()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            Quantity = 2m,
            AverageCost = 1000m,
            CurrentPrice = 1500m
        };

        // Act
        var gainLoss = holding.CalculateUnrealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(1000m)); // (1500 - 1000) * 2 = 1000
    }

    [Test]
    public void CalculateUnrealizedGainLoss_PriceDecreased_ReturnsNegativeLoss()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            Quantity = 3m,
            AverageCost = 2000m,
            CurrentPrice = 1500m
        };

        // Act
        var gainLoss = holding.CalculateUnrealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(-1500m)); // (1500 - 2000) * 3 = -1500
    }

    [Test]
    public void CalculateUnrealizedGainLoss_PriceUnchanged_ReturnsZero()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            Quantity = 5m,
            AverageCost = 1000m,
            CurrentPrice = 1000m
        };

        // Act
        var gainLoss = holding.CalculateUnrealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(0));
    }

    [Test]
    public void UpdatePrice_UpdatesPriceAndTimestamp()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            CurrentPrice = 1000m
        };
        var newPrice = 1200m;
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        holding.UpdatePrice(newPrice);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holding.CurrentPrice, Is.EqualTo(newPrice));
            Assert.That(holding.LastPriceUpdate, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void UpdatePrice_WithZero_UpdatesPrice()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            CurrentPrice = 1000m
        };

        // Act
        holding.UpdatePrice(0);

        // Assert
        Assert.That(holding.CurrentPrice, Is.EqualTo(0));
    }

    [Test]
    public void UpdatePrice_CalledMultipleTimes_UpdatesTimestamp()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            CurrentPrice = 1000m
        };

        holding.UpdatePrice(1100m);
        var firstUpdate = holding.LastPriceUpdate;

        System.Threading.Thread.Sleep(10);

        // Act
        holding.UpdatePrice(1200m);

        // Assert
        Assert.That(holding.LastPriceUpdate, Is.GreaterThanOrEqualTo(firstUpdate));
    }

    [Test]
    public void Wallet_NavigationProperty_CanBeSet()
    {
        // Arrange
        var wallet = new Wallet { WalletId = Guid.NewGuid() };
        var holding = new CryptoHolding
        {
            WalletId = wallet.WalletId
        };

        // Act
        holding.Wallet = wallet;

        // Assert
        Assert.That(holding.Wallet, Is.EqualTo(wallet));
    }

    [Test]
    public void CalculateMarketValue_WithDecimalQuantity_ReturnsCorrectValue()
    {
        // Arrange
        var holding = new CryptoHolding
        {
            Quantity = 0.5m,
            CurrentPrice = 50000m
        };

        // Act
        var marketValue = holding.CalculateMarketValue();

        // Assert
        Assert.That(marketValue, Is.EqualTo(25000m));
    }
}
