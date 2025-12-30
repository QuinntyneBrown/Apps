namespace DateNightIdeaGenerator.Core.Tests;

public class CategoryTests
{
    [Test]
    public void Category_Outdoor_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Outdoor;

        // Assert
        Assert.That((int)category, Is.EqualTo(0));
    }

    [Test]
    public void Category_Indoor_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Indoor;

        // Assert
        Assert.That((int)category, Is.EqualTo(1));
    }

    [Test]
    public void Category_Adventure_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Adventure;

        // Assert
        Assert.That((int)category, Is.EqualTo(2));
    }

    [Test]
    public void Category_Romantic_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Romantic;

        // Assert
        Assert.That((int)category, Is.EqualTo(3));
    }

    [Test]
    public void Category_Cultural_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Cultural;

        // Assert
        Assert.That((int)category, Is.EqualTo(4));
    }

    [Test]
    public void Category_FoodAndDining_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.FoodAndDining;

        // Assert
        Assert.That((int)category, Is.EqualTo(5));
    }

    [Test]
    public void Category_Entertainment_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Entertainment;

        // Assert
        Assert.That((int)category, Is.EqualTo(6));
    }

    [Test]
    public void Category_SportsAndFitness_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.SportsAndFitness;

        // Assert
        Assert.That((int)category, Is.EqualTo(7));
    }

    [Test]
    public void Category_Relaxation_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Relaxation;

        // Assert
        Assert.That((int)category, Is.EqualTo(8));
    }

    [Test]
    public void Category_Creative_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Creative;

        // Assert
        Assert.That((int)category, Is.EqualTo(9));
    }

    [Test]
    public void Category_CanBeAssignedToProperty()
    {
        // Arrange
        var dateIdea = new DateIdea();

        // Act
        dateIdea.Category = Category.Romantic;

        // Assert
        Assert.That(dateIdea.Category, Is.EqualTo(Category.Romantic));
    }

    [Test]
    public void Category_AllValuesAreUnique()
    {
        // Arrange
        var values = Enum.GetValues<Category>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }
}
