namespace CryptoPortfolioManager.Core.Tests;

public class TransactionTypeTests
{
    [Test]
    public void TransactionType_Buy_HasCorrectValue()
    {
        // Arrange & Act
        var type = TransactionType.Buy;

        // Assert
        Assert.That((int)type, Is.EqualTo(0));
    }

    [Test]
    public void TransactionType_Sell_HasCorrectValue()
    {
        // Arrange & Act
        var type = TransactionType.Sell;

        // Assert
        Assert.That((int)type, Is.EqualTo(1));
    }

    [Test]
    public void TransactionType_Transfer_HasCorrectValue()
    {
        // Arrange & Act
        var type = TransactionType.Transfer;

        // Assert
        Assert.That((int)type, Is.EqualTo(2));
    }

    [Test]
    public void TransactionType_Stake_HasCorrectValue()
    {
        // Arrange & Act
        var type = TransactionType.Stake;

        // Assert
        Assert.That((int)type, Is.EqualTo(3));
    }

    [Test]
    public void TransactionType_Reward_HasCorrectValue()
    {
        // Arrange & Act
        var type = TransactionType.Reward;

        // Assert
        Assert.That((int)type, Is.EqualTo(4));
    }

    [Test]
    public void TransactionType_CanBeAssignedToProperty()
    {
        // Arrange
        var transaction = new Transaction();

        // Act
        transaction.TransactionType = TransactionType.Buy;

        // Assert
        Assert.That(transaction.TransactionType, Is.EqualTo(TransactionType.Buy));
    }

    [Test]
    public void TransactionType_AllValuesAreUnique()
    {
        // Arrange
        var values = Enum.GetValues<TransactionType>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }
}
