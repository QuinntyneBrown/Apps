namespace CryptoPortfolioManager.Core.Tests;

public class EventTests
{
    [Test]
    public void HoldingAddedEvent_CanBeCreated()
    {
        // Arrange
        var holdingId = Guid.NewGuid();
        var walletId = Guid.NewGuid();
        var symbol = "BTC";
        var quantity = 1.5m;

        // Act
        var evt = new HoldingAddedEvent
        {
            CryptoHoldingId = holdingId,
            WalletId = walletId,
            Symbol = symbol,
            Quantity = quantity
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.CryptoHoldingId, Is.EqualTo(holdingId));
            Assert.That(evt.WalletId, Is.EqualTo(walletId));
            Assert.That(evt.Symbol, Is.EqualTo(symbol));
            Assert.That(evt.Quantity, Is.EqualTo(quantity));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TransactionRecordedEvent_CanBeCreated()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var walletId = Guid.NewGuid();
        var transactionType = TransactionType.Buy;
        var totalAmount = 50000m;

        // Act
        var evt = new TransactionRecordedEvent
        {
            TransactionId = transactionId,
            WalletId = walletId,
            TransactionType = transactionType,
            TotalAmount = totalAmount
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TransactionId, Is.EqualTo(transactionId));
            Assert.That(evt.WalletId, Is.EqualTo(walletId));
            Assert.That(evt.TransactionType, Is.EqualTo(transactionType));
            Assert.That(evt.TotalAmount, Is.EqualTo(totalAmount));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void WalletCreatedEvent_CanBeCreated()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var name = "My Wallet";

        // Act
        var evt = new WalletCreatedEvent
        {
            WalletId = walletId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.WalletId, Is.EqualTo(walletId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TaxLotCreatedEvent_CanBeCreated()
    {
        // Arrange
        var taxLotId = Guid.NewGuid();
        var holdingId = Guid.NewGuid();
        var quantity = 1.5m;
        var costBasis = 45000m;

        // Act
        var evt = new TaxLotCreatedEvent
        {
            TaxLotId = taxLotId,
            CryptoHoldingId = holdingId,
            Quantity = quantity,
            CostBasis = costBasis
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TaxLotId, Is.EqualTo(taxLotId));
            Assert.That(evt.CryptoHoldingId, Is.EqualTo(holdingId));
            Assert.That(evt.Quantity, Is.EqualTo(quantity));
            Assert.That(evt.CostBasis, Is.EqualTo(costBasis));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Events_AreRecords()
    {
        // This ensures events are immutable and have value-based equality
        var walletId = Guid.NewGuid();
        var name = "Test Wallet";

        var event1 = new WalletCreatedEvent
        {
            WalletId = walletId,
            Name = name,
            Timestamp = new DateTime(2024, 1, 1)
        };

        var event2 = new WalletCreatedEvent
        {
            WalletId = walletId,
            Name = name,
            Timestamp = new DateTime(2024, 1, 1)
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void TransactionRecordedEvent_AllTransactionTypes_CanBeUsed()
    {
        // Arrange & Act
        var buyEvent = new TransactionRecordedEvent { TransactionType = TransactionType.Buy };
        var sellEvent = new TransactionRecordedEvent { TransactionType = TransactionType.Sell };
        var transferEvent = new TransactionRecordedEvent { TransactionType = TransactionType.Transfer };
        var stakeEvent = new TransactionRecordedEvent { TransactionType = TransactionType.Stake };
        var rewardEvent = new TransactionRecordedEvent { TransactionType = TransactionType.Reward };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(buyEvent.TransactionType, Is.EqualTo(TransactionType.Buy));
            Assert.That(sellEvent.TransactionType, Is.EqualTo(TransactionType.Sell));
            Assert.That(transferEvent.TransactionType, Is.EqualTo(TransactionType.Transfer));
            Assert.That(stakeEvent.TransactionType, Is.EqualTo(TransactionType.Stake));
            Assert.That(rewardEvent.TransactionType, Is.EqualTo(TransactionType.Reward));
        });
    }
}
