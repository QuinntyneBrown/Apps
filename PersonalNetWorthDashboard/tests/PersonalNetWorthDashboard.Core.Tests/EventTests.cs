namespace PersonalNetWorthDashboard.Core.Tests;

public class EventTests
{
    [Test]
    public void AssetAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var assetId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AssetAddedEvent
        {
            AssetId = assetId,
            Name = "Primary Residence",
            AssetType = AssetType.RealEstate,
            CurrentValue = 500000m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AssetId, Is.EqualTo(assetId));
            Assert.That(evt.Name, Is.EqualTo("Primary Residence"));
            Assert.That(evt.AssetType, Is.EqualTo(AssetType.RealEstate));
            Assert.That(evt.CurrentValue, Is.EqualTo(500000m));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void AssetValueUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var assetId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AssetValueUpdatedEvent
        {
            AssetId = assetId,
            PreviousValue = 500000m,
            NewValue = 550000m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AssetId, Is.EqualTo(assetId));
            Assert.That(evt.PreviousValue, Is.EqualTo(500000m));
            Assert.That(evt.NewValue, Is.EqualTo(550000m));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void LiabilityAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var liabilityId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new LiabilityAddedEvent
        {
            LiabilityId = liabilityId,
            Name = "Home Mortgage",
            LiabilityType = LiabilityType.Mortgage,
            CurrentBalance = 250000m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.LiabilityId, Is.EqualTo(liabilityId));
            Assert.That(evt.Name, Is.EqualTo("Home Mortgage"));
            Assert.That(evt.LiabilityType, Is.EqualTo(LiabilityType.Mortgage));
            Assert.That(evt.CurrentBalance, Is.EqualTo(250000m));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void LiabilityBalanceUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var liabilityId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new LiabilityBalanceUpdatedEvent
        {
            LiabilityId = liabilityId,
            PreviousBalance = 250000m,
            NewBalance = 245000m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.LiabilityId, Is.EqualTo(liabilityId));
            Assert.That(evt.PreviousBalance, Is.EqualTo(250000m));
            Assert.That(evt.NewBalance, Is.EqualTo(245000m));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void SnapshotCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var snapshotId = Guid.NewGuid();
        var snapshotDate = DateTime.UtcNow.Date;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new SnapshotCreatedEvent
        {
            NetWorthSnapshotId = snapshotId,
            SnapshotDate = snapshotDate,
            TotalAssets = 1000000m,
            TotalLiabilities = 300000m,
            NetWorth = 700000m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NetWorthSnapshotId, Is.EqualTo(snapshotId));
            Assert.That(evt.SnapshotDate, Is.EqualTo(snapshotDate));
            Assert.That(evt.TotalAssets, Is.EqualTo(1000000m));
            Assert.That(evt.TotalLiabilities, Is.EqualTo(300000m));
            Assert.That(evt.NetWorth, Is.EqualTo(700000m));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void AssetAddedEvent_AllAssetTypes_CanBeUsed()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new AssetAddedEvent { AssetType = AssetType.Cash }, Throws.Nothing);
            Assert.That(() => new AssetAddedEvent { AssetType = AssetType.Investment }, Throws.Nothing);
            Assert.That(() => new AssetAddedEvent { AssetType = AssetType.Retirement }, Throws.Nothing);
            Assert.That(() => new AssetAddedEvent { AssetType = AssetType.RealEstate }, Throws.Nothing);
            Assert.That(() => new AssetAddedEvent { AssetType = AssetType.Vehicle }, Throws.Nothing);
            Assert.That(() => new AssetAddedEvent { AssetType = AssetType.PersonalProperty }, Throws.Nothing);
            Assert.That(() => new AssetAddedEvent { AssetType = AssetType.Business }, Throws.Nothing);
            Assert.That(() => new AssetAddedEvent { AssetType = AssetType.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void LiabilityAddedEvent_AllLiabilityTypes_CanBeUsed()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new LiabilityAddedEvent { LiabilityType = LiabilityType.Mortgage }, Throws.Nothing);
            Assert.That(() => new LiabilityAddedEvent { LiabilityType = LiabilityType.AutoLoan }, Throws.Nothing);
            Assert.That(() => new LiabilityAddedEvent { LiabilityType = LiabilityType.StudentLoan }, Throws.Nothing);
            Assert.That(() => new LiabilityAddedEvent { LiabilityType = LiabilityType.CreditCard }, Throws.Nothing);
            Assert.That(() => new LiabilityAddedEvent { LiabilityType = LiabilityType.PersonalLoan }, Throws.Nothing);
            Assert.That(() => new LiabilityAddedEvent { LiabilityType = LiabilityType.MedicalDebt }, Throws.Nothing);
            Assert.That(() => new LiabilityAddedEvent { LiabilityType = LiabilityType.BusinessLoan }, Throws.Nothing);
            Assert.That(() => new LiabilityAddedEvent { LiabilityType = LiabilityType.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void AssetValueUpdatedEvent_ValueIncrease_CanBeRecorded()
    {
        // Arrange & Act
        var evt = new AssetValueUpdatedEvent
        {
            AssetId = Guid.NewGuid(),
            PreviousValue = 100000m,
            NewValue = 150000m
        };

        // Assert
        Assert.That(evt.NewValue, Is.GreaterThan(evt.PreviousValue));
    }

    [Test]
    public void AssetValueUpdatedEvent_ValueDecrease_CanBeRecorded()
    {
        // Arrange & Act
        var evt = new AssetValueUpdatedEvent
        {
            AssetId = Guid.NewGuid(),
            PreviousValue = 150000m,
            NewValue = 100000m
        };

        // Assert
        Assert.That(evt.NewValue, Is.LessThan(evt.PreviousValue));
    }

    [Test]
    public void LiabilityBalanceUpdatedEvent_BalanceDecrease_CanBeRecorded()
    {
        // Arrange & Act
        var evt = new LiabilityBalanceUpdatedEvent
        {
            LiabilityId = Guid.NewGuid(),
            PreviousBalance = 50000m,
            NewBalance = 45000m
        };

        // Assert
        Assert.That(evt.NewBalance, Is.LessThan(evt.PreviousBalance));
    }

    [Test]
    public void SnapshotCreatedEvent_NegativeNetWorth_CanBeRecorded()
    {
        // Arrange & Act
        var evt = new SnapshotCreatedEvent
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 100000m,
            TotalLiabilities = 150000m,
            NetWorth = -50000m
        };

        // Assert
        Assert.That(evt.NetWorth, Is.LessThan(0m));
    }

    [Test]
    public void Events_AreRecords_AreImmutable()
    {
        // Arrange & Act & Assert - Record types are immutable
        Assert.Multiple(() =>
        {
            var evt1 = new AssetAddedEvent { AssetId = Guid.NewGuid() };
            Assert.That(evt1, Is.Not.Null);

            var evt2 = new AssetValueUpdatedEvent { AssetId = Guid.NewGuid() };
            Assert.That(evt2, Is.Not.Null);

            var evt3 = new LiabilityAddedEvent { LiabilityId = Guid.NewGuid() };
            Assert.That(evt3, Is.Not.Null);

            var evt4 = new LiabilityBalanceUpdatedEvent { LiabilityId = Guid.NewGuid() };
            Assert.That(evt4, Is.Not.Null);

            var evt5 = new SnapshotCreatedEvent { NetWorthSnapshotId = Guid.NewGuid() };
            Assert.That(evt5, Is.Not.Null);
        });
    }
}
