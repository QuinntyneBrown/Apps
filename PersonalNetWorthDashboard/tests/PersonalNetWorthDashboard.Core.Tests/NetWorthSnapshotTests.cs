namespace PersonalNetWorthDashboard.Core.Tests;

public class NetWorthSnapshotTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesNetWorthSnapshot()
    {
        // Arrange
        var snapshotId = Guid.NewGuid();
        var snapshotDate = DateTime.UtcNow;

        // Act
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = snapshotId,
            SnapshotDate = snapshotDate,
            TotalAssets = 1000000m,
            TotalLiabilities = 300000m,
            NetWorth = 700000m,
            Notes = "End of year snapshot"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(snapshot.NetWorthSnapshotId, Is.EqualTo(snapshotId));
            Assert.That(snapshot.SnapshotDate, Is.EqualTo(snapshotDate));
            Assert.That(snapshot.TotalAssets, Is.EqualTo(1000000m));
            Assert.That(snapshot.TotalLiabilities, Is.EqualTo(300000m));
            Assert.That(snapshot.NetWorth, Is.EqualTo(700000m));
            Assert.That(snapshot.Notes, Is.EqualTo("End of year snapshot"));
            Assert.That(snapshot.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void CalculateNetWorth_ValidValues_CalculatesCorrectly()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 500000m,
            TotalLiabilities = 200000m,
            NetWorth = 0m
        };

        // Act
        snapshot.CalculateNetWorth();

        // Assert
        Assert.That(snapshot.NetWorth, Is.EqualTo(300000m));
    }

    [Test]
    public void CalculateNetWorth_NoLiabilities_EqualsAssets()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 500000m,
            TotalLiabilities = 0m,
            NetWorth = 0m
        };

        // Act
        snapshot.CalculateNetWorth();

        // Assert
        Assert.That(snapshot.NetWorth, Is.EqualTo(500000m));
    }

    [Test]
    public void CalculateNetWorth_LiabilitiesExceedAssets_NegativeNetWorth()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 100000m,
            TotalLiabilities = 150000m,
            NetWorth = 0m
        };

        // Act
        snapshot.CalculateNetWorth();

        // Assert
        Assert.That(snapshot.NetWorth, Is.EqualTo(-50000m));
    }

    [Test]
    public void UpdateValues_ValidValues_UpdatesAndCalculates()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 100000m,
            TotalLiabilities = 50000m,
            NetWorth = 50000m
        };

        // Act
        snapshot.UpdateValues(200000m, 75000m);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(snapshot.TotalAssets, Is.EqualTo(200000m));
            Assert.That(snapshot.TotalLiabilities, Is.EqualTo(75000m));
            Assert.That(snapshot.NetWorth, Is.EqualTo(125000m));
        });
    }

    [Test]
    public void UpdateValues_NegativeAssets_ThrowsArgumentException()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 100000m,
            TotalLiabilities = 50000m
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => snapshot.UpdateValues(-10000m, 50000m));
    }

    [Test]
    public void UpdateValues_NegativeLiabilities_ThrowsArgumentException()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 100000m,
            TotalLiabilities = 50000m
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => snapshot.UpdateValues(100000m, -50000m));
    }

    [Test]
    public void CalculateNetWorthChange_PositiveChange_ReturnsCorrectValue()
    {
        // Arrange
        var previousSnapshot = new NetWorthSnapshot
        {
            NetWorth = 100000m
        };

        var currentSnapshot = new NetWorthSnapshot
        {
            NetWorth = 150000m
        };

        // Act
        var change = currentSnapshot.CalculateNetWorthChange(previousSnapshot);

        // Assert
        Assert.That(change, Is.EqualTo(50000m));
    }

    [Test]
    public void CalculateNetWorthChange_NegativeChange_ReturnsCorrectValue()
    {
        // Arrange
        var previousSnapshot = new NetWorthSnapshot
        {
            NetWorth = 150000m
        };

        var currentSnapshot = new NetWorthSnapshot
        {
            NetWorth = 100000m
        };

        // Act
        var change = currentSnapshot.CalculateNetWorthChange(previousSnapshot);

        // Assert
        Assert.That(change, Is.EqualTo(-50000m));
    }

    [Test]
    public void CalculateNetWorthChange_NoChange_ReturnsZero()
    {
        // Arrange
        var previousSnapshot = new NetWorthSnapshot
        {
            NetWorth = 100000m
        };

        var currentSnapshot = new NetWorthSnapshot
        {
            NetWorth = 100000m
        };

        // Act
        var change = currentSnapshot.CalculateNetWorthChange(previousSnapshot);

        // Assert
        Assert.That(change, Is.EqualTo(0m));
    }

    [Test]
    public void CalculatePercentageChange_PositiveChange_ReturnsCorrectPercentage()
    {
        // Arrange
        var previousSnapshot = new NetWorthSnapshot
        {
            NetWorth = 100000m
        };

        var currentSnapshot = new NetWorthSnapshot
        {
            NetWorth = 150000m
        };

        // Act
        var percentage = currentSnapshot.CalculatePercentageChange(previousSnapshot);

        // Assert
        Assert.That(percentage, Is.EqualTo(50m));
    }

    [Test]
    public void CalculatePercentageChange_NegativeChange_ReturnsCorrectPercentage()
    {
        // Arrange
        var previousSnapshot = new NetWorthSnapshot
        {
            NetWorth = 100000m
        };

        var currentSnapshot = new NetWorthSnapshot
        {
            NetWorth = 75000m
        };

        // Act
        var percentage = currentSnapshot.CalculatePercentageChange(previousSnapshot);

        // Assert
        Assert.That(percentage, Is.EqualTo(-25m));
    }

    [Test]
    public void CalculatePercentageChange_PreviousNetWorthZero_ReturnsNull()
    {
        // Arrange
        var previousSnapshot = new NetWorthSnapshot
        {
            NetWorth = 0m
        };

        var currentSnapshot = new NetWorthSnapshot
        {
            NetWorth = 100000m
        };

        // Act
        var percentage = currentSnapshot.CalculatePercentageChange(previousSnapshot);

        // Assert
        Assert.That(percentage, Is.Null);
    }

    [Test]
    public void CalculatePercentageChange_FromNegativeToPositive_CalculatesCorrectly()
    {
        // Arrange
        var previousSnapshot = new NetWorthSnapshot
        {
            NetWorth = -50000m
        };

        var currentSnapshot = new NetWorthSnapshot
        {
            NetWorth = 50000m
        };

        // Act
        var percentage = currentSnapshot.CalculatePercentageChange(previousSnapshot);

        // Assert
        Assert.That(percentage, Is.EqualTo(200m));
    }
}
