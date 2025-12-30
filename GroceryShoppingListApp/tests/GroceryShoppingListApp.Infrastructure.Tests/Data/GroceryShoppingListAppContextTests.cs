// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Infrastructure.Tests;

/// <summary>
/// Unit tests for the GroceryShoppingListAppContext.
/// </summary>
[TestFixture]
public class GroceryShoppingListAppContextTests
{
    private GroceryShoppingListAppContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<GroceryShoppingListAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new GroceryShoppingListAppContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that GroceryLists can be added and retrieved.
    /// </summary>
    [Test]
    public async Task GroceryLists_CanAddAndRetrieve()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Weekly Shopping",
            CreatedDate = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.GroceryLists.Add(groceryList);
        await _context.SaveChangesAsync();

        var retrieved = await _context.GroceryLists.FindAsync(groceryList.GroceryListId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Weekly Shopping"));
        Assert.That(retrieved.IsCompleted, Is.False);
    }

    /// <summary>
    /// Tests that GroceryItems can be added and retrieved.
    /// </summary>
    [Test]
    public async Task GroceryItems_CanAddAndRetrieve()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            CreatedDate = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var groceryItem = new GroceryItem
        {
            GroceryItemId = Guid.NewGuid(),
            GroceryListId = groceryList.GroceryListId,
            Name = "Milk",
            Category = Category.Dairy,
            Quantity = 2,
            IsChecked = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.GroceryLists.Add(groceryList);
        _context.GroceryItems.Add(groceryItem);
        await _context.SaveChangesAsync();

        var retrieved = await _context.GroceryItems.FindAsync(groceryItem.GroceryItemId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Milk"));
        Assert.That(retrieved.Category, Is.EqualTo(Category.Dairy));
        Assert.That(retrieved.Quantity, Is.EqualTo(2));
    }

    /// <summary>
    /// Tests that Stores can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Stores_CanAddAndRetrieve()
    {
        // Arrange
        var store = new Store
        {
            StoreId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Whole Foods",
            Address = "123 Main St",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Stores.Add(store);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Stores.FindAsync(store.StoreId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Whole Foods"));
        Assert.That(retrieved.Address, Is.EqualTo("123 Main St"));
    }

    /// <summary>
    /// Tests that PriceHistories can be added and retrieved.
    /// </summary>
    [Test]
    public async Task PriceHistories_CanAddAndRetrieve()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            CreatedDate = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var groceryItem = new GroceryItem
        {
            GroceryItemId = Guid.NewGuid(),
            GroceryListId = groceryList.GroceryListId,
            Name = "Milk",
            Category = Category.Dairy,
            Quantity = 2,
            IsChecked = false,
            CreatedAt = DateTime.UtcNow,
        };

        var store = new Store
        {
            StoreId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Whole Foods",
            CreatedAt = DateTime.UtcNow,
        };

        var priceHistory = new PriceHistory
        {
            PriceHistoryId = Guid.NewGuid(),
            GroceryItemId = groceryItem.GroceryItemId,
            StoreId = store.StoreId,
            Price = 4.99m,
            Date = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.GroceryLists.Add(groceryList);
        _context.GroceryItems.Add(groceryItem);
        _context.Stores.Add(store);
        _context.PriceHistories.Add(priceHistory);
        await _context.SaveChangesAsync();

        var retrieved = await _context.PriceHistories.FindAsync(priceHistory.PriceHistoryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Price, Is.EqualTo(4.99m));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedItems()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            CreatedDate = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var groceryItem = new GroceryItem
        {
            GroceryItemId = Guid.NewGuid(),
            GroceryListId = groceryList.GroceryListId,
            Name = "Milk",
            Category = Category.Dairy,
            Quantity = 2,
            IsChecked = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.GroceryLists.Add(groceryList);
        _context.GroceryItems.Add(groceryItem);
        await _context.SaveChangesAsync();

        // Act
        _context.GroceryLists.Remove(groceryList);
        await _context.SaveChangesAsync();

        var retrievedItem = await _context.GroceryItems.FindAsync(groceryItem.GroceryItemId);

        // Assert
        Assert.That(retrievedItem, Is.Null);
    }
}
