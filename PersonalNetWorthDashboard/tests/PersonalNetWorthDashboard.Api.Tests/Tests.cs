using PersonalNetWorthDashboard.Api.Features.Asset;
using PersonalNetWorthDashboard.Api.Features.Liability;
using PersonalNetWorthDashboard.Api.Features.NetWorthSnapshot;
using PersonalNetWorthDashboard.Core;
using PersonalNetWorthDashboard.Infrastructure;

namespace PersonalNetWorthDashboard.Api.Tests;

public class DtoMappingTests
{
    [Test]
    public void AssetToDto_ShouldMapCorrectly()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 10000m,
            PurchasePrice = 8000m,
            PurchaseDate = DateTime.UtcNow.AddYears(-1),
            Institution = "Test Bank",
            AccountNumber = "123456",
            Notes = "Test notes",
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        // Act
        var dto = asset.ToDto();

        // Assert
        Assert.That(dto.AssetId, Is.EqualTo(asset.AssetId));
        Assert.That(dto.Name, Is.EqualTo(asset.Name));
        Assert.That(dto.AssetType, Is.EqualTo(asset.AssetType));
        Assert.That(dto.CurrentValue, Is.EqualTo(asset.CurrentValue));
        Assert.That(dto.PurchasePrice, Is.EqualTo(asset.PurchasePrice));
        Assert.That(dto.PurchaseDate, Is.EqualTo(asset.PurchaseDate));
        Assert.That(dto.Institution, Is.EqualTo(asset.Institution));
        Assert.That(dto.AccountNumber, Is.EqualTo(asset.AccountNumber));
        Assert.That(dto.Notes, Is.EqualTo(asset.Notes));
        Assert.That(dto.LastUpdated, Is.EqualTo(asset.LastUpdated));
        Assert.That(dto.IsActive, Is.EqualTo(asset.IsActive));
    }

    [Test]
    public void LiabilityToDto_ShouldMapCorrectly()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Test Liability",
            LiabilityType = LiabilityType.Mortgage,
            CurrentBalance = 200000m,
            OriginalAmount = 250000m,
            InterestRate = 3.5m,
            MonthlyPayment = 1500m,
            Creditor = "Test Bank",
            AccountNumber = "987654",
            DueDate = DateTime.UtcNow.AddDays(15),
            Notes = "Test notes",
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        // Act
        var dto = liability.ToDto();

        // Assert
        Assert.That(dto.LiabilityId, Is.EqualTo(liability.LiabilityId));
        Assert.That(dto.Name, Is.EqualTo(liability.Name));
        Assert.That(dto.LiabilityType, Is.EqualTo(liability.LiabilityType));
        Assert.That(dto.CurrentBalance, Is.EqualTo(liability.CurrentBalance));
        Assert.That(dto.OriginalAmount, Is.EqualTo(liability.OriginalAmount));
        Assert.That(dto.InterestRate, Is.EqualTo(liability.InterestRate));
        Assert.That(dto.MonthlyPayment, Is.EqualTo(liability.MonthlyPayment));
        Assert.That(dto.Creditor, Is.EqualTo(liability.Creditor));
        Assert.That(dto.AccountNumber, Is.EqualTo(liability.AccountNumber));
        Assert.That(dto.DueDate, Is.EqualTo(liability.DueDate));
        Assert.That(dto.Notes, Is.EqualTo(liability.Notes));
        Assert.That(dto.LastUpdated, Is.EqualTo(liability.LastUpdated));
        Assert.That(dto.IsActive, Is.EqualTo(liability.IsActive));
    }

    [Test]
    public void NetWorthSnapshotToDto_ShouldMapCorrectly()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 500000m,
            TotalLiabilities = 200000m,
            NetWorth = 300000m,
            Notes = "Test snapshot",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = snapshot.ToDto();

        // Assert
        Assert.That(dto.NetWorthSnapshotId, Is.EqualTo(snapshot.NetWorthSnapshotId));
        Assert.That(dto.SnapshotDate, Is.EqualTo(snapshot.SnapshotDate));
        Assert.That(dto.TotalAssets, Is.EqualTo(snapshot.TotalAssets));
        Assert.That(dto.TotalLiabilities, Is.EqualTo(snapshot.TotalLiabilities));
        Assert.That(dto.NetWorth, Is.EqualTo(snapshot.NetWorth));
        Assert.That(dto.Notes, Is.EqualTo(snapshot.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(snapshot.CreatedAt));
    }
}

public class AssetFeatureTests
{
    private PersonalNetWorthDashboardContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<PersonalNetWorthDashboardContext>(_testDatabaseId.ToString());
        _context = new PersonalNetWorthDashboardContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateAssetCommand_ShouldCreateAsset()
    {
        // Arrange
        var handler = new CreateAssetCommandHandler(_context);
        var command = new CreateAssetCommand(
            "Savings Account",
            AssetType.Cash,
            10000m,
            null,
            null,
            "Test Bank",
            "123456",
            "Test notes");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.AssetType, Is.EqualTo(command.AssetType));
        Assert.That(result.CurrentValue, Is.EqualTo(command.CurrentValue));
        Assert.That(result.Institution, Is.EqualTo(command.Institution));
        Assert.That(result.IsActive, Is.True);

        var savedAsset = await _context.Assets.FindAsync(result.AssetId);
        Assert.That(savedAsset, Is.Not.Null);
    }

    [Test]
    public async Task GetAssetsQuery_ShouldReturnAllAssets()
    {
        // Arrange
        var asset1 = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Asset 1",
            AssetType = AssetType.Cash,
            CurrentValue = 10000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };
        var asset2 = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Asset 2",
            AssetType = AssetType.Investment,
            CurrentValue = 50000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Assets.AddRange(asset1, asset2);
        await _context.SaveChangesAsync();

        var handler = new GetAssetsQueryHandler(_context);
        var query = new GetAssetsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetAssetByIdQuery_ShouldReturnAsset()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.RealEstate,
            CurrentValue = 300000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        var handler = new GetAssetByIdQueryHandler(_context);
        var query = new GetAssetByIdQuery(asset.AssetId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AssetId, Is.EqualTo(asset.AssetId));
    }

    [Test]
    public void GetAssetByIdQuery_ShouldThrowWhenNotFound()
    {
        // Arrange
        var handler = new GetAssetByIdQueryHandler(_context);
        var query = new GetAssetByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public async Task UpdateAssetCommand_ShouldUpdateAsset()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Original Name",
            AssetType = AssetType.Cash,
            CurrentValue = 10000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        var handler = new UpdateAssetCommandHandler(_context);
        var command = new UpdateAssetCommand(
            asset.AssetId,
            "Updated Name",
            AssetType.Investment,
            15000m,
            10000m,
            DateTime.UtcNow.AddYears(-1),
            "New Bank",
            "654321",
            "Updated notes");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Updated Name"));
        Assert.That(result.AssetType, Is.EqualTo(AssetType.Investment));
        Assert.That(result.CurrentValue, Is.EqualTo(15000m));
        Assert.That(result.Institution, Is.EqualTo("New Bank"));
    }

    [Test]
    public async Task DeleteAssetCommand_ShouldDeleteAsset()
    {
        // Arrange
        var asset = new Asset
        {
            AssetId = Guid.NewGuid(),
            Name = "Test Asset",
            AssetType = AssetType.Cash,
            CurrentValue = 10000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();

        var handler = new DeleteAssetCommandHandler(_context);
        var command = new DeleteAssetCommand(asset.AssetId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedAsset = await _context.Assets.FindAsync(asset.AssetId);
        Assert.That(deletedAsset, Is.Null);
    }
}

public class LiabilityFeatureTests
{
    private PersonalNetWorthDashboardContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<PersonalNetWorthDashboardContext>(_testDatabaseId.ToString());
        _context = new PersonalNetWorthDashboardContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateLiabilityCommand_ShouldCreateLiability()
    {
        // Arrange
        var handler = new CreateLiabilityCommandHandler(_context);
        var command = new CreateLiabilityCommand(
            "Home Mortgage",
            LiabilityType.Mortgage,
            200000m,
            250000m,
            3.5m,
            1500m,
            "Test Bank",
            "987654",
            DateTime.UtcNow.AddDays(15),
            "Test notes");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.LiabilityType, Is.EqualTo(command.LiabilityType));
        Assert.That(result.CurrentBalance, Is.EqualTo(command.CurrentBalance));
        Assert.That(result.OriginalAmount, Is.EqualTo(command.OriginalAmount));
        Assert.That(result.IsActive, Is.True);

        var savedLiability = await _context.Liabilities.FindAsync(result.LiabilityId);
        Assert.That(savedLiability, Is.Not.Null);
    }

    [Test]
    public async Task GetLiabilitiesQuery_ShouldReturnAllLiabilities()
    {
        // Arrange
        var liability1 = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Mortgage",
            LiabilityType = LiabilityType.Mortgage,
            CurrentBalance = 200000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };
        var liability2 = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Car Loan",
            LiabilityType = LiabilityType.AutoLoan,
            CurrentBalance = 15000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Liabilities.AddRange(liability1, liability2);
        await _context.SaveChangesAsync();

        var handler = new GetLiabilitiesQueryHandler(_context);
        var query = new GetLiabilitiesQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetLiabilityByIdQuery_ShouldReturnLiability()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Test Liability",
            LiabilityType = LiabilityType.CreditCard,
            CurrentBalance = 5000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Liabilities.Add(liability);
        await _context.SaveChangesAsync();

        var handler = new GetLiabilityByIdQueryHandler(_context);
        var query = new GetLiabilityByIdQuery(liability.LiabilityId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.LiabilityId, Is.EqualTo(liability.LiabilityId));
    }

    [Test]
    public void GetLiabilityByIdQuery_ShouldThrowWhenNotFound()
    {
        // Arrange
        var handler = new GetLiabilityByIdQueryHandler(_context);
        var query = new GetLiabilityByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public async Task UpdateLiabilityCommand_ShouldUpdateLiability()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Original Liability",
            LiabilityType = LiabilityType.Mortgage,
            CurrentBalance = 200000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Liabilities.Add(liability);
        await _context.SaveChangesAsync();

        var handler = new UpdateLiabilityCommandHandler(_context);
        var command = new UpdateLiabilityCommand(
            liability.LiabilityId,
            "Updated Liability",
            LiabilityType.Mortgage,
            180000m,
            250000m,
            3.0m,
            1400m,
            "Updated Bank",
            "111111",
            DateTime.UtcNow.AddDays(20),
            "Updated notes");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Updated Liability"));
        Assert.That(result.CurrentBalance, Is.EqualTo(180000m));
        Assert.That(result.MonthlyPayment, Is.EqualTo(1400m));
    }

    [Test]
    public async Task DeleteLiabilityCommand_ShouldDeleteLiability()
    {
        // Arrange
        var liability = new Liability
        {
            LiabilityId = Guid.NewGuid(),
            Name = "Test Liability",
            LiabilityType = LiabilityType.PersonalLoan,
            CurrentBalance = 10000m,
            LastUpdated = DateTime.UtcNow,
            IsActive = true
        };

        _context.Liabilities.Add(liability);
        await _context.SaveChangesAsync();

        var handler = new DeleteLiabilityCommandHandler(_context);
        var command = new DeleteLiabilityCommand(liability.LiabilityId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedLiability = await _context.Liabilities.FindAsync(liability.LiabilityId);
        Assert.That(deletedLiability, Is.Null);
    }
}

public class NetWorthSnapshotFeatureTests
{
    private PersonalNetWorthDashboardContext _context = null!;
    private Guid _testDatabaseId;

    [SetUp]
    public void Setup()
    {
        _testDatabaseId = Guid.NewGuid();
        var options = TestHelpers.CreateInMemoryDbContextOptions<PersonalNetWorthDashboardContext>(_testDatabaseId.ToString());
        _context = new PersonalNetWorthDashboardContext(options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateNetWorthSnapshotCommand_ShouldCreateSnapshot()
    {
        // Arrange
        var handler = new CreateNetWorthSnapshotCommandHandler(_context);
        var command = new CreateNetWorthSnapshotCommand(
            DateTime.UtcNow,
            500000m,
            200000m,
            "Monthly snapshot");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TotalAssets, Is.EqualTo(command.TotalAssets));
        Assert.That(result.TotalLiabilities, Is.EqualTo(command.TotalLiabilities));
        Assert.That(result.NetWorth, Is.EqualTo(300000m));
        Assert.That(result.Notes, Is.EqualTo(command.Notes));

        var savedSnapshot = await _context.NetWorthSnapshots.FindAsync(result.NetWorthSnapshotId);
        Assert.That(savedSnapshot, Is.Not.Null);
    }

    [Test]
    public async Task GetNetWorthSnapshotsQuery_ShouldReturnAllSnapshots()
    {
        // Arrange
        var snapshot1 = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow.AddMonths(-1),
            TotalAssets = 450000m,
            TotalLiabilities = 210000m,
            NetWorth = 240000m,
            CreatedAt = DateTime.UtcNow.AddMonths(-1)
        };
        var snapshot2 = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 500000m,
            TotalLiabilities = 200000m,
            NetWorth = 300000m,
            CreatedAt = DateTime.UtcNow
        };

        _context.NetWorthSnapshots.AddRange(snapshot1, snapshot2);
        await _context.SaveChangesAsync();

        var handler = new GetNetWorthSnapshotsQueryHandler(_context);
        var query = new GetNetWorthSnapshotsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].SnapshotDate, Is.GreaterThanOrEqualTo(result[1].SnapshotDate));
    }

    [Test]
    public async Task GetNetWorthSnapshotByIdQuery_ShouldReturnSnapshot()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 500000m,
            TotalLiabilities = 200000m,
            NetWorth = 300000m,
            CreatedAt = DateTime.UtcNow
        };

        _context.NetWorthSnapshots.Add(snapshot);
        await _context.SaveChangesAsync();

        var handler = new GetNetWorthSnapshotByIdQueryHandler(_context);
        var query = new GetNetWorthSnapshotByIdQuery(snapshot.NetWorthSnapshotId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.NetWorthSnapshotId, Is.EqualTo(snapshot.NetWorthSnapshotId));
    }

    [Test]
    public void GetNetWorthSnapshotByIdQuery_ShouldThrowWhenNotFound()
    {
        // Arrange
        var handler = new GetNetWorthSnapshotByIdQueryHandler(_context);
        var query = new GetNetWorthSnapshotByIdQuery(Guid.NewGuid());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(query, CancellationToken.None));
    }

    [Test]
    public async Task UpdateNetWorthSnapshotCommand_ShouldUpdateSnapshot()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 500000m,
            TotalLiabilities = 200000m,
            NetWorth = 300000m,
            CreatedAt = DateTime.UtcNow
        };

        _context.NetWorthSnapshots.Add(snapshot);
        await _context.SaveChangesAsync();

        var handler = new UpdateNetWorthSnapshotCommandHandler(_context);
        var command = new UpdateNetWorthSnapshotCommand(
            snapshot.NetWorthSnapshotId,
            DateTime.UtcNow.AddDays(1),
            550000m,
            190000m,
            "Updated snapshot");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.TotalAssets, Is.EqualTo(550000m));
        Assert.That(result.TotalLiabilities, Is.EqualTo(190000m));
        Assert.That(result.NetWorth, Is.EqualTo(360000m));
        Assert.That(result.Notes, Is.EqualTo("Updated snapshot"));
    }

    [Test]
    public async Task DeleteNetWorthSnapshotCommand_ShouldDeleteSnapshot()
    {
        // Arrange
        var snapshot = new NetWorthSnapshot
        {
            NetWorthSnapshotId = Guid.NewGuid(),
            SnapshotDate = DateTime.UtcNow,
            TotalAssets = 500000m,
            TotalLiabilities = 200000m,
            NetWorth = 300000m,
            CreatedAt = DateTime.UtcNow
        };

        _context.NetWorthSnapshots.Add(snapshot);
        await _context.SaveChangesAsync();

        var handler = new DeleteNetWorthSnapshotCommandHandler(_context);
        var command = new DeleteNetWorthSnapshotCommand(snapshot.NetWorthSnapshotId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedSnapshot = await _context.NetWorthSnapshots.FindAsync(snapshot.NetWorthSnapshotId);
        Assert.That(deletedSnapshot, Is.Null);
    }
}
