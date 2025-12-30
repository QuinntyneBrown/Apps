namespace SideHustleIncomeTracker.Core.Tests;

public class ExpenseTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesExpense()
    {
        // Arrange & Act
        var expense = new Expense
        {
            ExpenseId = Guid.NewGuid(),
            BusinessId = Guid.NewGuid(),
            Description = "Adobe Creative Cloud Subscription",
            Amount = 54.99m,
            ExpenseDate = new DateTime(2024, 3, 1),
            Category = "Software",
            Vendor = "Adobe Inc.",
            IsTaxDeductible = true,
            Notes = "Monthly subscription"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(expense.ExpenseId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(expense.BusinessId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(expense.Description, Is.EqualTo("Adobe Creative Cloud Subscription"));
            Assert.That(expense.Amount, Is.EqualTo(54.99m));
            Assert.That(expense.ExpenseDate, Is.EqualTo(new DateTime(2024, 3, 1)));
            Assert.That(expense.Category, Is.EqualTo("Software"));
            Assert.That(expense.Vendor, Is.EqualTo("Adobe Inc."));
            Assert.That(expense.IsTaxDeductible, Is.True);
        });
    }

    [Test]
    public void Description_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var expense = new Expense();

        // Assert
        Assert.That(expense.Description, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsTaxDeductible_DefaultValue_IsTrue()
    {
        // Arrange & Act
        var expense = new Expense();

        // Assert
        Assert.That(expense.IsTaxDeductible, Is.True);
    }

    [Test]
    public void Category_CanBeNull()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Category = null
        };

        // Assert
        Assert.That(expense.Category, Is.Null);
    }

    [Test]
    public void Vendor_CanBeNull()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Vendor = null
        };

        // Assert
        Assert.That(expense.Vendor, Is.Null);
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Notes = null
        };

        // Assert
        Assert.That(expense.Notes, Is.Null);
    }

    [Test]
    public void Amount_CanBeSetToZero()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Amount = 0m
        };

        // Assert
        Assert.That(expense.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void Amount_LargeValue_CanBeSet()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Amount = 50000m
        };

        // Assert
        Assert.That(expense.Amount, Is.EqualTo(50000m));
    }

    [Test]
    public void Amount_SmallDecimalValue_CanBeSet()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Amount = 12.99m
        };

        // Assert
        Assert.That(expense.Amount, Is.EqualTo(12.99m));
    }

    [Test]
    public void ExpenseDate_CanBeSetToAnyDate()
    {
        // Arrange
        var specificDate = new DateTime(2024, 2, 20);
        var expense = new Expense
        {
            ExpenseDate = specificDate
        };

        // Assert
        Assert.That(expense.ExpenseDate, Is.EqualTo(specificDate));
    }

    [Test]
    public void IsTaxDeductible_CanBeSetToFalse()
    {
        // Arrange & Act
        var expense = new Expense
        {
            IsTaxDeductible = false
        };

        // Assert
        Assert.That(expense.IsTaxDeductible, Is.False);
    }

    [Test]
    public void Business_NavigationProperty_CanBeSet()
    {
        // Arrange
        var expense = new Expense();
        var business = new Business
        {
            BusinessId = Guid.NewGuid(),
            Name = "My Business"
        };

        // Act
        expense.Business = business;

        // Assert
        Assert.That(expense.Business, Is.Not.Null);
        Assert.That(expense.Business.Name, Is.EqualTo("My Business"));
    }

    [Test]
    public void Category_SoftwareCategory_CanBeSet()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Category = "Software"
        };

        // Assert
        Assert.That(expense.Category, Is.EqualTo("Software"));
    }

    [Test]
    public void Category_OfficeSuppliesCategory_CanBeSet()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Category = "Office Supplies"
        };

        // Assert
        Assert.That(expense.Category, Is.EqualTo("Office Supplies"));
    }

    [Test]
    public void Category_TravelCategory_CanBeSet()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Category = "Travel"
        };

        // Assert
        Assert.That(expense.Category, Is.EqualTo("Travel"));
    }

    [Test]
    public void Vendor_DifferentVendorNames_CanBeSet()
    {
        // Arrange & Act
        var expense = new Expense
        {
            Vendor = "Amazon Business"
        };

        // Assert
        Assert.That(expense.Vendor, Is.EqualTo("Amazon Business"));
    }
}
