namespace CryptoPortfolioManager.Core.Tests;

public class TransactionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTransaction()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var walletId = Guid.NewGuid();
        var transactionDate = DateTime.UtcNow;
        var transactionType = TransactionType.Buy;
        var symbol = "ETH";
        var quantity = 10m;
        var pricePerUnit = 2000m;
        var totalAmount = 20000m;
        var fees = 50m;

        // Act
        var transaction = new Transaction
        {
            TransactionId = transactionId,
            WalletId = walletId,
            TransactionDate = transactionDate,
            TransactionType = transactionType,
            Symbol = symbol,
            Quantity = quantity,
            PricePerUnit = pricePerUnit,
            TotalAmount = totalAmount,
            Fees = fees
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(transaction.TransactionId, Is.EqualTo(transactionId));
            Assert.That(transaction.WalletId, Is.EqualTo(walletId));
            Assert.That(transaction.TransactionDate, Is.EqualTo(transactionDate));
            Assert.That(transaction.TransactionType, Is.EqualTo(transactionType));
            Assert.That(transaction.Symbol, Is.EqualTo(symbol));
            Assert.That(transaction.Quantity, Is.EqualTo(quantity));
            Assert.That(transaction.PricePerUnit, Is.EqualTo(pricePerUnit));
            Assert.That(transaction.TotalAmount, Is.EqualTo(totalAmount));
            Assert.That(transaction.Fees, Is.EqualTo(fees));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var transaction = new Transaction();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(transaction.Symbol, Is.EqualTo(string.Empty));
            Assert.That(transaction.Quantity, Is.EqualTo(0));
            Assert.That(transaction.PricePerUnit, Is.EqualTo(0));
            Assert.That(transaction.TotalAmount, Is.EqualTo(0));
            Assert.That(transaction.Fees, Is.Null);
            Assert.That(transaction.Notes, Is.Null);
        });
    }

    [Test]
    public void CalculateTotalCost_WithFees_ReturnsSumOfAmountAndFees()
    {
        // Arrange
        var transaction = new Transaction
        {
            TotalAmount = 10000m,
            Fees = 100m
        };

        // Act
        var totalCost = transaction.CalculateTotalCost();

        // Assert
        Assert.That(totalCost, Is.EqualTo(10100m));
    }

    [Test]
    public void CalculateTotalCost_WithoutFees_ReturnsTotalAmount()
    {
        // Arrange
        var transaction = new Transaction
        {
            TotalAmount = 10000m,
            Fees = null
        };

        // Act
        var totalCost = transaction.CalculateTotalCost();

        // Assert
        Assert.That(totalCost, Is.EqualTo(10000m));
    }

    [Test]
    public void CalculateTotalCost_WithZeroFees_ReturnsTotalAmount()
    {
        // Arrange
        var transaction = new Transaction
        {
            TotalAmount = 10000m,
            Fees = 0m
        };

        // Act
        var totalCost = transaction.CalculateTotalCost();

        // Assert
        Assert.That(totalCost, Is.EqualTo(10000m));
    }

    [Test]
    public void CalculateTotalCost_WithZeroAmount_ReturnsOnlyFees()
    {
        // Arrange
        var transaction = new Transaction
        {
            TotalAmount = 0m,
            Fees = 50m
        };

        // Act
        var totalCost = transaction.CalculateTotalCost();

        // Assert
        Assert.That(totalCost, Is.EqualTo(50m));
    }

    [Test]
    public void TransactionType_Buy_CanBeSet()
    {
        // Arrange & Act
        var transaction = new Transaction
        {
            TransactionType = TransactionType.Buy
        };

        // Assert
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Buy));
    }

    [Test]
    public void TransactionType_Sell_CanBeSet()
    {
        // Arrange & Act
        var transaction = new Transaction
        {
            TransactionType = TransactionType.Sell
        };

        // Assert
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Sell));
    }

    [Test]
    public void TransactionType_Transfer_CanBeSet()
    {
        // Arrange & Act
        var transaction = new Transaction
        {
            TransactionType = TransactionType.Transfer
        };

        // Assert
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Transfer));
    }

    [Test]
    public void TransactionType_Stake_CanBeSet()
    {
        // Arrange & Act
        var transaction = new Transaction
        {
            TransactionType = TransactionType.Stake
        };

        // Assert
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Stake));
    }

    [Test]
    public void TransactionType_Reward_CanBeSet()
    {
        // Arrange & Act
        var transaction = new Transaction
        {
            TransactionType = TransactionType.Reward
        };

        // Assert
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Reward));
    }

    [Test]
    public void Notes_CanBeSetAndRetrieved()
    {
        // Arrange
        var notes = "Bought during market dip";
        var transaction = new Transaction();

        // Act
        transaction.Notes = notes;

        // Assert
        Assert.That(transaction.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Wallet_NavigationProperty_CanBeSet()
    {
        // Arrange
        var wallet = new Wallet { WalletId = Guid.NewGuid() };
        var transaction = new Transaction
        {
            WalletId = wallet.WalletId
        };

        // Act
        transaction.Wallet = wallet;

        // Assert
        Assert.That(transaction.Wallet, Is.EqualTo(wallet));
    }

    [Test]
    public void CalculateTotalCost_WithDecimalValues_ReturnsCorrectSum()
    {
        // Arrange
        var transaction = new Transaction
        {
            TotalAmount = 1234.56m,
            Fees = 12.34m
        };

        // Act
        var totalCost = transaction.CalculateTotalCost();

        // Assert
        Assert.That(totalCost, Is.EqualTo(1246.90m));
    }
}
