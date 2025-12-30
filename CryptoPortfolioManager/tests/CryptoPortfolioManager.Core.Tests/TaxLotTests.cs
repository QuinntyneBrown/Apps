namespace CryptoPortfolioManager.Core.Tests;

public class TaxLotTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTaxLot()
    {
        // Arrange
        var taxLotId = Guid.NewGuid();
        var cryptoHoldingId = Guid.NewGuid();
        var acquisitionDate = DateTime.UtcNow.AddMonths(-6);
        var quantity = 1.5m;
        var costBasis = 45000m;

        // Act
        var taxLot = new TaxLot
        {
            TaxLotId = taxLotId,
            CryptoHoldingId = cryptoHoldingId,
            AcquisitionDate = acquisitionDate,
            Quantity = quantity,
            CostBasis = costBasis
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taxLot.TaxLotId, Is.EqualTo(taxLotId));
            Assert.That(taxLot.CryptoHoldingId, Is.EqualTo(cryptoHoldingId));
            Assert.That(taxLot.AcquisitionDate, Is.EqualTo(acquisitionDate));
            Assert.That(taxLot.Quantity, Is.EqualTo(quantity));
            Assert.That(taxLot.CostBasis, Is.EqualTo(costBasis));
            Assert.That(taxLot.IsDisposed, Is.False);
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var taxLot = new TaxLot();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taxLot.Quantity, Is.EqualTo(0));
            Assert.That(taxLot.CostBasis, Is.EqualTo(0));
            Assert.That(taxLot.IsDisposed, Is.False);
            Assert.That(taxLot.DisposalDate, Is.Null);
            Assert.That(taxLot.DisposalPrice, Is.Null);
        });
    }

    [Test]
    public void CalculateRealizedGainLoss_NotDisposed_ReturnsNull()
    {
        // Arrange
        var taxLot = new TaxLot
        {
            IsDisposed = false,
            Quantity = 1m,
            CostBasis = 10000m
        };

        // Act
        var gainLoss = taxLot.CalculateRealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.Null);
    }

    [Test]
    public void CalculateRealizedGainLoss_DisposedWithoutPrice_ReturnsNull()
    {
        // Arrange
        var taxLot = new TaxLot
        {
            IsDisposed = true,
            DisposalPrice = null,
            Quantity = 1m,
            CostBasis = 10000m
        };

        // Act
        var gainLoss = taxLot.CalculateRealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.Null);
    }

    [Test]
    public void CalculateRealizedGainLoss_DisposedWithGain_ReturnsPositiveValue()
    {
        // Arrange
        var taxLot = new TaxLot
        {
            IsDisposed = true,
            Quantity = 2m,
            CostBasis = 10000m,
            DisposalPrice = 6000m  // Sold at 6000 per unit
        };

        // Act
        var gainLoss = taxLot.CalculateRealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(2000m)); // (6000 * 2) - 10000 = 2000 gain
    }

    [Test]
    public void CalculateRealizedGainLoss_DisposedWithLoss_ReturnsNegativeValue()
    {
        // Arrange
        var taxLot = new TaxLot
        {
            IsDisposed = true,
            Quantity = 1m,
            CostBasis = 15000m,
            DisposalPrice = 12000m
        };

        // Act
        var gainLoss = taxLot.CalculateRealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(-3000m)); // (12000 * 1) - 15000 = -3000 loss
    }

    [Test]
    public void CalculateRealizedGainLoss_DisposedAtBreakEven_ReturnsZero()
    {
        // Arrange
        var taxLot = new TaxLot
        {
            IsDisposed = true,
            Quantity = 3m,
            CostBasis = 30000m,
            DisposalPrice = 10000m  // Sold at same price as cost basis per unit
        };

        // Act
        var gainLoss = taxLot.CalculateRealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(0m)); // (10000 * 3) - 30000 = 0
    }

    [Test]
    public void CalculateRealizedGainLoss_WithDecimalQuantity_ReturnsCorrectValue()
    {
        // Arrange
        var taxLot = new TaxLot
        {
            IsDisposed = true,
            Quantity = 0.5m,
            CostBasis = 5000m,
            DisposalPrice = 12000m
        };

        // Act
        var gainLoss = taxLot.CalculateRealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(1000m)); // (12000 * 0.5) - 5000 = 1000
    }

    [Test]
    public void IsDisposed_CanBeSetToTrue()
    {
        // Arrange & Act
        var taxLot = new TaxLot
        {
            IsDisposed = true,
            DisposalDate = DateTime.UtcNow,
            DisposalPrice = 50000m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(taxLot.IsDisposed, Is.True);
            Assert.That(taxLot.DisposalDate, Is.Not.Null);
            Assert.That(taxLot.DisposalPrice, Is.Not.Null);
        });
    }

    [Test]
    public void DisposalDate_CanBeSet()
    {
        // Arrange
        var disposalDate = DateTime.UtcNow;
        var taxLot = new TaxLot();

        // Act
        taxLot.DisposalDate = disposalDate;

        // Assert
        Assert.That(taxLot.DisposalDate, Is.EqualTo(disposalDate));
    }

    [Test]
    public void DisposalPrice_CanBeSet()
    {
        // Arrange
        var disposalPrice = 55000m;
        var taxLot = new TaxLot();

        // Act
        taxLot.DisposalPrice = disposalPrice;

        // Assert
        Assert.That(taxLot.DisposalPrice, Is.EqualTo(disposalPrice));
    }

    [Test]
    public void CryptoHolding_NavigationProperty_CanBeSet()
    {
        // Arrange
        var holding = new CryptoHolding { CryptoHoldingId = Guid.NewGuid() };
        var taxLot = new TaxLot
        {
            CryptoHoldingId = holding.CryptoHoldingId
        };

        // Act
        taxLot.CryptoHolding = holding;

        // Assert
        Assert.That(taxLot.CryptoHolding, Is.EqualTo(holding));
    }

    [Test]
    public void CalculateRealizedGainLoss_WithLargeNumbers_ReturnsAccurateValue()
    {
        // Arrange
        var taxLot = new TaxLot
        {
            IsDisposed = true,
            Quantity = 100m,
            CostBasis = 1000000m,
            DisposalPrice = 12000m
        };

        // Act
        var gainLoss = taxLot.CalculateRealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(200000m)); // (12000 * 100) - 1000000 = 200000
    }

    [Test]
    public void CalculateRealizedGainLoss_WithZeroDisposalPrice_ReturnsNegativeCostBasis()
    {
        // Arrange
        var taxLot = new TaxLot
        {
            IsDisposed = true,
            Quantity = 2m,
            CostBasis = 10000m,
            DisposalPrice = 0m
        };

        // Act
        var gainLoss = taxLot.CalculateRealizedGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(-10000m)); // (0 * 2) - 10000 = -10000
    }
}
