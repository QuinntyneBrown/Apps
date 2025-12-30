namespace PersonalNetWorthDashboard.Core.Tests;

public class AssetTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesAsset()
    {
        // Arrange
        var assetId = Guid.NewGuid();
        var purchaseDate = DateTime.UtcNow.AddYears(-2);

        // Act
        var asset = new Asset
        {
            AssetId = assetId,
            Name = "Primary Residence",
            AssetType = AssetType.RealEstate,
            CurrentValue = 500000m,
            PurchasePrice = 400000m,
            PurchaseDate = purchaseDate,
            Institution = "First National Bank",
            AccountNumber = "ACCT-12345",
            Notes = "Single family home",
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(asset.AssetId, Is.EqualTo(assetId));
            Assert.That(asset.Name, Is.EqualTo("Primary Residence"));
            Assert.That(asset.AssetType, Is.EqualTo(AssetType.RealEstate));
            Assert.That(asset.CurrentValue, Is.EqualTo(500000m));
            Assert.That(asset.PurchasePrice, Is.EqualTo(400000m));
            Assert.That(asset.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(asset.Institution, Is.EqualTo("First National Bank"));
            Assert.That(asset.AccountNumber, Is.EqualTo("ACCT-12345"));
            Assert.That(asset.Notes, Is.EqualTo("Single family home"));
            Assert.That(asset.IsActive, Is.True);
            Assert.That(asset.LastUpdated, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UpdateValue_ValidPositiveValue_UpdatesValueAndTimestamp()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 1000m
        };

        // Act
        asset.UpdateValue(1500m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(asset.CurrentValue, Is.EqualTo(1500m));
            Assert.That(asset.LastUpdated, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UpdateValue_ZeroValue_UpdatesCorrectly()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 1000m
        };

        // Act
        asset.UpdateValue(0m);

        // Assert
        Assert.That(asset.CurrentValue, Is.EqualTo(0m));
    }

    [Test]
    public void UpdateValue_NegativeValue_ThrowsArgumentException()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 1000m
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => asset.UpdateValue(-100m));
    }

    [Test]
    public void CalculateGainLoss_WithPurchasePrice_ReturnsGain()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Investment",
            AssetType = AssetType.Investment,
            CurrentValue = 15000m,
            PurchasePrice = 10000m
        };

        // Act
        var gainLoss = asset.CalculateGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(5000m));
    }

    [Test]
    public void CalculateGainLoss_WithPurchasePrice_ReturnsLoss()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Investment",
            AssetType = AssetType.Investment,
            CurrentValue = 8000m,
            PurchasePrice = 10000m
        };

        // Act
        var gainLoss = asset.CalculateGainLoss();

        // Assert
        Assert.That(gainLoss, Is.EqualTo(-2000m));
    }

    [Test]
    public void CalculateGainLoss_WithoutPurchasePrice_ReturnsNull()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Cash",
            AssetType = AssetType.Cash,
            CurrentValue = 5000m,
            PurchasePrice = null
        };

        // Act
        var gainLoss = asset.CalculateGainLoss();

        // Assert
        Assert.That(gainLoss, Is.Null);
    }

    [Test]
    public void Deactivate_SetsIsActiveToFalse()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 1000m,
            IsActive = true
        };

        // Act
        asset.Deactivate();

        // Assert
        Assert.That(asset.IsActive, Is.False);
    }

    [Test]
    public void Reactivate_SetsIsActiveToTrue()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 1000m,
            IsActive = false
        };

        // Act
        asset.Reactivate();

        // Assert
        Assert.That(asset.IsActive, Is.True);
    }

    [Test]
    public void Asset_DefaultIsActive_IsTrue()
    {
        // Arrange & Act
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash
        };

        // Assert
        Assert.That(asset.IsActive, Is.True);
    }

    [Test]
    public void Asset_AllAssetTypes_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Asset { AssetType = AssetType.Cash }, Throws.Nothing);
            Assert.That(() => new Asset { AssetType = AssetType.Investment }, Throws.Nothing);
            Assert.That(() => new Asset { AssetType = AssetType.Retirement }, Throws.Nothing);
            Assert.That(() => new Asset { AssetType = AssetType.RealEstate }, Throws.Nothing);
            Assert.That(() => new Asset { AssetType = AssetType.Vehicle }, Throws.Nothing);
            Assert.That(() => new Asset { AssetType = AssetType.PersonalProperty }, Throws.Nothing);
            Assert.That(() => new Asset { AssetType = AssetType.Business }, Throws.Nothing);
            Assert.That(() => new Asset { AssetType = AssetType.Other }, Throws.Nothing);
        });
    }
}
