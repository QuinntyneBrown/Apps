using NutritionLabelScanner.Api.Features.Product;
using NutritionLabelScanner.Api.Features.NutritionInfo;
using NutritionLabelScanner.Api.Features.Comparison;
using NutritionLabelScanner.Core;
using NutritionLabelScanner.Infrastructure;

namespace NutritionLabelScanner.Api.Tests;

public class DtoMappingTests
{
    [Test]
    public void ProductToDto_ShouldMapCorrectly()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Product",
            Brand = "Test Brand",
            Barcode = "1234567890",
            Category = "Snacks",
            ServingSize = "100g",
            ScannedAt = DateTime.UtcNow,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = product.ToDto();

        // Assert
        Assert.That(dto.ProductId, Is.EqualTo(product.ProductId));
        Assert.That(dto.UserId, Is.EqualTo(product.UserId));
        Assert.That(dto.Name, Is.EqualTo(product.Name));
        Assert.That(dto.Brand, Is.EqualTo(product.Brand));
        Assert.That(dto.Barcode, Is.EqualTo(product.Barcode));
        Assert.That(dto.Category, Is.EqualTo(product.Category));
        Assert.That(dto.ServingSize, Is.EqualTo(product.ServingSize));
        Assert.That(dto.ScannedAt, Is.EqualTo(product.ScannedAt));
        Assert.That(dto.Notes, Is.EqualTo(product.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(product.CreatedAt));
    }

    [Test]
    public void NutritionInfoToDto_ShouldMapCorrectly()
    {
        // Arrange
        var nutritionInfo = new Core.NutritionInfo
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Calories = 250,
            TotalFat = 10.5m,
            SaturatedFat = 3.5m,
            TransFat = 0.1m,
            Cholesterol = 20m,
            Sodium = 450m,
            TotalCarbohydrates = 35m,
            DietaryFiber = 5m,
            TotalSugars = 12m,
            Protein = 8m,
            AdditionalNutrients = "{\"VitaminD\": \"2mcg\"}",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = nutritionInfo.ToDto();

        // Assert
        Assert.That(dto.NutritionInfoId, Is.EqualTo(nutritionInfo.NutritionInfoId));
        Assert.That(dto.ProductId, Is.EqualTo(nutritionInfo.ProductId));
        Assert.That(dto.Calories, Is.EqualTo(nutritionInfo.Calories));
        Assert.That(dto.TotalFat, Is.EqualTo(nutritionInfo.TotalFat));
        Assert.That(dto.SaturatedFat, Is.EqualTo(nutritionInfo.SaturatedFat));
        Assert.That(dto.TransFat, Is.EqualTo(nutritionInfo.TransFat));
        Assert.That(dto.Cholesterol, Is.EqualTo(nutritionInfo.Cholesterol));
        Assert.That(dto.Sodium, Is.EqualTo(nutritionInfo.Sodium));
        Assert.That(dto.TotalCarbohydrates, Is.EqualTo(nutritionInfo.TotalCarbohydrates));
        Assert.That(dto.DietaryFiber, Is.EqualTo(nutritionInfo.DietaryFiber));
        Assert.That(dto.TotalSugars, Is.EqualTo(nutritionInfo.TotalSugars));
        Assert.That(dto.Protein, Is.EqualTo(nutritionInfo.Protein));
        Assert.That(dto.AdditionalNutrients, Is.EqualTo(nutritionInfo.AdditionalNutrients));
        Assert.That(dto.CreatedAt, Is.EqualTo(nutritionInfo.CreatedAt));
    }

    [Test]
    public void ComparisonToDto_ShouldMapCorrectly()
    {
        // Arrange
        var comparison = new Core.Comparison
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Comparison",
            ProductIds = "[\"id1\",\"id2\"]",
            Results = "Product 1 has less sugar",
            WinnerProductId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = comparison.ToDto();

        // Assert
        Assert.That(dto.ComparisonId, Is.EqualTo(comparison.ComparisonId));
        Assert.That(dto.UserId, Is.EqualTo(comparison.UserId));
        Assert.That(dto.Name, Is.EqualTo(comparison.Name));
        Assert.That(dto.ProductIds, Is.EqualTo(comparison.ProductIds));
        Assert.That(dto.Results, Is.EqualTo(comparison.Results));
        Assert.That(dto.WinnerProductId, Is.EqualTo(comparison.WinnerProductId));
        Assert.That(dto.CreatedAt, Is.EqualTo(comparison.CreatedAt));
    }
}

public class ProductFeatureTests
{
    private NutritionLabelScannerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<NutritionLabelScannerContext>(_testDatabaseId.ToString());
        _context = new NutritionLabelScannerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateProductCommand_ShouldCreateProduct()
    {
        // Arrange
        var handler = new CreateProductCommandHandler(_context);
        var command = new CreateProductCommand(
            Guid.NewGuid(),
            "Test Product",
            "Test Brand",
            "1234567890",
            "Snacks",
            "100g",
            DateTime.UtcNow,
            "Test notes");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Brand, Is.EqualTo(command.Brand));
        Assert.That(result.Barcode, Is.EqualTo(command.Barcode));

        var savedProduct = await _context.Products.FindAsync(result.ProductId);
        Assert.That(savedProduct, Is.Not.Null);
    }

    [Test]
    public async Task GetProductsQuery_ShouldReturnAllProducts()
    {
        // Arrange
        var product1 = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Product 1",
            ScannedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        var product2 = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Product 2",
            ScannedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.AddRange(product1, product2);
        await _context.SaveChangesAsync();

        var handler = new GetProductsQueryHandler(_context);
        var query = new GetProductsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetProductByIdQuery_ShouldReturnProduct()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Product",
            ScannedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var handler = new GetProductByIdQueryHandler(_context);
        var query = new GetProductByIdQuery(product.ProductId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ProductId, Is.EqualTo(product.ProductId));
    }

    [Test]
    public void GetProductByIdQuery_ShouldThrowWhenNotFound()
    {
        // Arrange
        var handler = new GetProductByIdQueryHandler(_context);
        var query = new GetProductByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public async Task UpdateProductCommand_ShouldUpdateProduct()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Original Name",
            ScannedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var handler = new UpdateProductCommandHandler(_context);
        var command = new UpdateProductCommand(
            product.ProductId,
            product.UserId,
            "Updated Name",
            "Updated Brand",
            "9876543210",
            "Beverages",
            "200ml",
            DateTime.UtcNow,
            "Updated notes");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Updated Name"));
        Assert.That(result.Brand, Is.EqualTo("Updated Brand"));
        Assert.That(result.Barcode, Is.EqualTo("9876543210"));
    }

    [Test]
    public async Task DeleteProductCommand_ShouldDeleteProduct()
    {
        // Arrange
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Product",
            ScannedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var handler = new DeleteProductCommandHandler(_context);
        var command = new DeleteProductCommand(product.ProductId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedProduct = await _context.Products.FindAsync(product.ProductId);
        Assert.That(deletedProduct, Is.Null);
    }
}

public class NutritionInfoFeatureTests
{
    private NutritionLabelScannerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<NutritionLabelScannerContext>(_testDatabaseId.ToString());
        _context = new NutritionLabelScannerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateNutritionInfoCommand_ShouldCreateNutritionInfo()
    {
        // Arrange
        var handler = new CreateNutritionInfoCommandHandler(_context);
        var command = new CreateNutritionInfoCommand(
            Guid.NewGuid(),
            250,
            10.5m,
            3.5m,
            0.1m,
            20m,
            450m,
            35m,
            5m,
            12m,
            8m,
            "{\"VitaminD\": \"2mcg\"}");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ProductId, Is.EqualTo(command.ProductId));
        Assert.That(result.Calories, Is.EqualTo(command.Calories));
        Assert.That(result.TotalFat, Is.EqualTo(command.TotalFat));

        var savedNutritionInfo = await _context.NutritionInfos.FindAsync(result.NutritionInfoId);
        Assert.That(savedNutritionInfo, Is.Not.Null);
    }

    [Test]
    public async Task GetNutritionInfosQuery_ShouldReturnAllNutritionInfos()
    {
        // Arrange
        var nutritionInfo1 = new Core.NutritionInfo
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Calories = 250,
            TotalFat = 10m,
            Sodium = 450m,
            TotalCarbohydrates = 35m,
            Protein = 8m,
            CreatedAt = DateTime.UtcNow
        };
        var nutritionInfo2 = new Core.NutritionInfo
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Calories = 150,
            TotalFat = 5m,
            Sodium = 300m,
            TotalCarbohydrates = 20m,
            Protein = 5m,
            CreatedAt = DateTime.UtcNow
        };

        _context.NutritionInfos.AddRange(nutritionInfo1, nutritionInfo2);
        await _context.SaveChangesAsync();

        var handler = new GetNutritionInfosQueryHandler(_context);
        var query = new GetNutritionInfosQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task UpdateNutritionInfoCommand_ShouldUpdateNutritionInfo()
    {
        // Arrange
        var nutritionInfo = new Core.NutritionInfo
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Calories = 250,
            TotalFat = 10m,
            Sodium = 450m,
            TotalCarbohydrates = 35m,
            Protein = 8m,
            CreatedAt = DateTime.UtcNow
        };

        _context.NutritionInfos.Add(nutritionInfo);
        await _context.SaveChangesAsync();

        var handler = new UpdateNutritionInfoCommandHandler(_context);
        var command = new UpdateNutritionInfoCommand(
            nutritionInfo.NutritionInfoId,
            nutritionInfo.ProductId,
            300,
            15m,
            5m,
            0.2m,
            30m,
            500m,
            40m,
            6m,
            15m,
            10m,
            null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Calories, Is.EqualTo(300));
        Assert.That(result.TotalFat, Is.EqualTo(15m));
        Assert.That(result.Protein, Is.EqualTo(10m));
    }

    [Test]
    public async Task DeleteNutritionInfoCommand_ShouldDeleteNutritionInfo()
    {
        // Arrange
        var nutritionInfo = new Core.NutritionInfo
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Calories = 250,
            TotalFat = 10m,
            Sodium = 450m,
            TotalCarbohydrates = 35m,
            Protein = 8m,
            CreatedAt = DateTime.UtcNow
        };

        _context.NutritionInfos.Add(nutritionInfo);
        await _context.SaveChangesAsync();

        var handler = new DeleteNutritionInfoCommandHandler(_context);
        var command = new DeleteNutritionInfoCommand(nutritionInfo.NutritionInfoId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedNutritionInfo = await _context.NutritionInfos.FindAsync(nutritionInfo.NutritionInfoId);
        Assert.That(deletedNutritionInfo, Is.Null);
    }
}

public class ComparisonFeatureTests
{
    private NutritionLabelScannerContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<NutritionLabelScannerContext>(_testDatabaseId.ToString());
        _context = new NutritionLabelScannerContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateComparisonCommand_ShouldCreateComparison()
    {
        // Arrange
        var handler = new CreateComparisonCommandHandler(_context);
        var command = new CreateComparisonCommand(
            Guid.NewGuid(),
            "Test Comparison",
            "[\"id1\",\"id2\"]",
            "Product 1 has less sugar",
            Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.ProductIds, Is.EqualTo(command.ProductIds));

        var savedComparison = await _context.Comparisons.FindAsync(result.ComparisonId);
        Assert.That(savedComparison, Is.Not.Null);
    }

    [Test]
    public async Task GetComparisonsQuery_ShouldReturnAllComparisons()
    {
        // Arrange
        var comparison1 = new Core.Comparison
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Comparison 1",
            ProductIds = "[\"id1\",\"id2\"]",
            CreatedAt = DateTime.UtcNow
        };
        var comparison2 = new Core.Comparison
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Comparison 2",
            ProductIds = "[\"id3\",\"id4\"]",
            CreatedAt = DateTime.UtcNow
        };

        _context.Comparisons.AddRange(comparison1, comparison2);
        await _context.SaveChangesAsync();

        var handler = new GetComparisonsQueryHandler(_context);
        var query = new GetComparisonsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task UpdateComparisonCommand_ShouldUpdateComparison()
    {
        // Arrange
        var comparison = new Core.Comparison
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Original Comparison",
            ProductIds = "[\"id1\",\"id2\"]",
            CreatedAt = DateTime.UtcNow
        };

        _context.Comparisons.Add(comparison);
        await _context.SaveChangesAsync();

        var handler = new UpdateComparisonCommandHandler(_context);
        var winnerId = Guid.NewGuid();
        var command = new UpdateComparisonCommand(
            comparison.ComparisonId,
            comparison.UserId,
            "Updated Comparison",
            "[\"id1\",\"id2\",\"id3\"]",
            "Product 2 wins",
            winnerId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Updated Comparison"));
        Assert.That(result.ProductIds, Is.EqualTo("[\"id1\",\"id2\",\"id3\"]"));
        Assert.That(result.Results, Is.EqualTo("Product 2 wins"));
        Assert.That(result.WinnerProductId, Is.EqualTo(winnerId));
    }

    [Test]
    public async Task DeleteComparisonCommand_ShouldDeleteComparison()
    {
        // Arrange
        var comparison = new Core.Comparison
        {
            ComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Comparison",
            ProductIds = "[\"id1\",\"id2\"]",
            CreatedAt = DateTime.UtcNow
        };

        _context.Comparisons.Add(comparison);
        await _context.SaveChangesAsync();

        var handler = new DeleteComparisonCommandHandler(_context);
        var command = new DeleteComparisonCommand(comparison.ComparisonId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedComparison = await _context.Comparisons.FindAsync(comparison.ComparisonId);
        Assert.That(deletedComparison, Is.Null);
    }
}
