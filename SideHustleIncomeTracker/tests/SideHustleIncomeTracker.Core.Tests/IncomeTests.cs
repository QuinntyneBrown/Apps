namespace SideHustleIncomeTracker.Core.Tests;

public class IncomeTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesIncome()
    {
        // Arrange & Act
        var income = new Income
        {
            IncomeId = Guid.NewGuid(),
            BusinessId = Guid.NewGuid(),
            Description = "Website design for ABC Corp",
            Amount = 5000m,
            IncomeDate = new DateTime(2024, 3, 15),
            Client = "ABC Corporation",
            InvoiceNumber = "INV-2024-001",
            IsPaid = true,
            Notes = "Payment received via wire transfer"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(income.IncomeId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(income.BusinessId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(income.Description, Is.EqualTo("Website design for ABC Corp"));
            Assert.That(income.Amount, Is.EqualTo(5000m));
            Assert.That(income.IncomeDate, Is.EqualTo(new DateTime(2024, 3, 15)));
            Assert.That(income.Client, Is.EqualTo("ABC Corporation"));
            Assert.That(income.InvoiceNumber, Is.EqualTo("INV-2024-001"));
            Assert.That(income.IsPaid, Is.True);
        });
    }

    [Test]
    public void MarkAsPaid_UnpaidIncome_SetsIsPaidToTrue()
    {
        // Arrange
        var income = new Income
        {
            IsPaid = false
        };

        // Act
        income.MarkAsPaid();

        // Assert
        Assert.That(income.IsPaid, Is.True);
    }

    [Test]
    public void MarkAsPaid_AlreadyPaid_RemainsTrue()
    {
        // Arrange
        var income = new Income
        {
            IsPaid = true
        };

        // Act
        income.MarkAsPaid();

        // Assert
        Assert.That(income.IsPaid, Is.True);
    }

    [Test]
    public void Description_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var income = new Income();

        // Assert
        Assert.That(income.Description, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsPaid_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var income = new Income();

        // Assert
        Assert.That(income.IsPaid, Is.False);
    }

    [Test]
    public void Client_CanBeNull()
    {
        // Arrange & Act
        var income = new Income
        {
            Client = null
        };

        // Assert
        Assert.That(income.Client, Is.Null);
    }

    [Test]
    public void InvoiceNumber_CanBeNull()
    {
        // Arrange & Act
        var income = new Income
        {
            InvoiceNumber = null
        };

        // Assert
        Assert.That(income.InvoiceNumber, Is.Null);
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var income = new Income
        {
            Notes = null
        };

        // Assert
        Assert.That(income.Notes, Is.Null);
    }

    [Test]
    public void Amount_CanBeSetToZero()
    {
        // Arrange & Act
        var income = new Income
        {
            Amount = 0m
        };

        // Assert
        Assert.That(income.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void Amount_LargeValue_CanBeSet()
    {
        // Arrange & Act
        var income = new Income
        {
            Amount = 999999.99m
        };

        // Assert
        Assert.That(income.Amount, Is.EqualTo(999999.99m));
    }

    [Test]
    public void IncomeDate_CanBeSetToAnyDate()
    {
        // Arrange
        var specificDate = new DateTime(2024, 1, 15);
        var income = new Income
        {
            IncomeDate = specificDate
        };

        // Assert
        Assert.That(income.IncomeDate, Is.EqualTo(specificDate));
    }

    [Test]
    public void Business_NavigationProperty_CanBeSet()
    {
        // Arrange
        var income = new Income();
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "My Business"
        };

        // Act
        income.Business = business;

        // Assert
        Assert.That(income.Business, Is.Not.Null);
        Assert.That(income.Business.Name, Is.EqualTo("My Business"));
    }

    [Test]
    public void InvoiceNumber_DifferentFormats_CanBeSet()
    {
        // Arrange & Act
        var income1 = new Income { InvoiceNumber = "INV-001" };
        var income2 = new Income { InvoiceNumber = "2024-001" };
        var income3 = new Income { InvoiceNumber = "ABC123" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(income1.InvoiceNumber, Is.EqualTo("INV-001"));
            Assert.That(income2.InvoiceNumber, Is.EqualTo("2024-001"));
            Assert.That(income3.InvoiceNumber, Is.EqualTo("ABC123"));
        });
    }

    [Test]
    public void Client_DifferentNames_CanBeSet()
    {
        // Arrange & Act
        var income = new Income
        {
            Client = "John Doe & Associates LLC"
        };

        // Assert
        Assert.That(income.Client, Is.EqualTo("John Doe & Associates LLC"));
    }
}
