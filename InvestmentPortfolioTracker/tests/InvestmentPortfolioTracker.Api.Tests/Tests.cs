namespace InvestmentPortfolioTracker.Api.Tests;

/// <summary>
/// Tests for DTO mapping extensions.
/// </summary>
[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void AccountToDto_MapsAllProperties_Correctly()
    {
        // Arrange
        var account = TestHelpers.CreateSampleAccount();

        // Act
        var dto = account.ToDto();

        // Assert
        Assert.That(dto.AccountId, Is.EqualTo(account.AccountId));
        Assert.That(dto.Name, Is.EqualTo(account.Name));
        Assert.That(dto.AccountType, Is.EqualTo(account.AccountType));
        Assert.That(dto.Institution, Is.EqualTo(account.Institution));
        Assert.That(dto.AccountNumber, Is.EqualTo(account.AccountNumber));
        Assert.That(dto.CurrentBalance, Is.EqualTo(account.CurrentBalance));
        Assert.That(dto.IsActive, Is.EqualTo(account.IsActive));
        Assert.That(dto.OpenedDate, Is.EqualTo(account.OpenedDate));
        Assert.That(dto.Notes, Is.EqualTo(account.Notes));
    }

    [Test]
    public void HoldingToDto_MapsAllProperties_Correctly()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var holding = TestHelpers.CreateSampleHolding(accountId);

        // Act
        var dto = holding.ToDto();

        // Assert
        Assert.That(dto.HoldingId, Is.EqualTo(holding.HoldingId));
        Assert.That(dto.AccountId, Is.EqualTo(holding.AccountId));
        Assert.That(dto.Symbol, Is.EqualTo(holding.Symbol));
        Assert.That(dto.Name, Is.EqualTo(holding.Name));
        Assert.That(dto.Shares, Is.EqualTo(holding.Shares));
        Assert.That(dto.AverageCost, Is.EqualTo(holding.AverageCost));
        Assert.That(dto.CurrentPrice, Is.EqualTo(holding.CurrentPrice));
        Assert.That(dto.LastPriceUpdate, Is.EqualTo(holding.LastPriceUpdate));
        Assert.That(dto.MarketValue, Is.EqualTo(holding.CalculateMarketValue()));
        Assert.That(dto.CostBasis, Is.EqualTo(holding.CalculateCostBasis()));
        Assert.That(dto.UnrealizedGainLoss, Is.EqualTo(holding.CalculateUnrealizedGainLoss()));
    }

    [Test]
    public void TransactionToDto_MapsAllProperties_Correctly()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var transaction = TestHelpers.CreateSampleTransaction(accountId);

        // Act
        var dto = transaction.ToDto();

        // Assert
        Assert.That(dto.TransactionId, Is.EqualTo(transaction.TransactionId));
        Assert.That(dto.AccountId, Is.EqualTo(transaction.AccountId));
        Assert.That(dto.HoldingId, Is.EqualTo(transaction.HoldingId));
        Assert.That(dto.TransactionDate, Is.EqualTo(transaction.TransactionDate));
        Assert.That(dto.TransactionType, Is.EqualTo(transaction.TransactionType));
        Assert.That(dto.Symbol, Is.EqualTo(transaction.Symbol));
        Assert.That(dto.Shares, Is.EqualTo(transaction.Shares));
        Assert.That(dto.PricePerShare, Is.EqualTo(transaction.PricePerShare));
        Assert.That(dto.Amount, Is.EqualTo(transaction.Amount));
        Assert.That(dto.Fees, Is.EqualTo(transaction.Fees));
        Assert.That(dto.Notes, Is.EqualTo(transaction.Notes));
        Assert.That(dto.TotalCost, Is.EqualTo(transaction.CalculateTotalCost()));
    }

    [Test]
    public void DividendToDto_MapsAllProperties_Correctly()
    {
        // Arrange
        var holdingId = Guid.NewGuid();
        var dividend = TestHelpers.CreateSampleDividend(holdingId);

        // Act
        var dto = dividend.ToDto();

        // Assert
        Assert.That(dto.DividendId, Is.EqualTo(dividend.DividendId));
        Assert.That(dto.HoldingId, Is.EqualTo(dividend.HoldingId));
        Assert.That(dto.PaymentDate, Is.EqualTo(dividend.PaymentDate));
        Assert.That(dto.ExDividendDate, Is.EqualTo(dividend.ExDividendDate));
        Assert.That(dto.AmountPerShare, Is.EqualTo(dividend.AmountPerShare));
        Assert.That(dto.TotalAmount, Is.EqualTo(dividend.TotalAmount));
        Assert.That(dto.IsReinvested, Is.EqualTo(dividend.IsReinvested));
        Assert.That(dto.Notes, Is.EqualTo(dividend.Notes));
    }
}

/// <summary>
/// Tests for Account queries and commands.
/// </summary>
[TestFixture]
public class AccountFeatureTests
{
    private Mock<IInvestmentPortfolioTrackerContext> _mockContext = null!;
    private Mock<ILogger<GetAccountsQueryHandler>> _mockGetAccountsLogger = null!;
    private Mock<ILogger<GetAccountByIdQueryHandler>> _mockGetAccountByIdLogger = null!;
    private Mock<ILogger<CreateAccountCommandHandler>> _mockCreateAccountLogger = null!;
    private Mock<ILogger<UpdateAccountCommandHandler>> _mockUpdateAccountLogger = null!;
    private Mock<ILogger<DeleteAccountCommandHandler>> _mockDeleteAccountLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IInvestmentPortfolioTrackerContext>();
        _mockGetAccountsLogger = TestHelpers.CreateMockLogger<GetAccountsQueryHandler>();
        _mockGetAccountByIdLogger = TestHelpers.CreateMockLogger<GetAccountByIdQueryHandler>();
        _mockCreateAccountLogger = TestHelpers.CreateMockLogger<CreateAccountCommandHandler>();
        _mockUpdateAccountLogger = TestHelpers.CreateMockLogger<UpdateAccountCommandHandler>();
        _mockDeleteAccountLogger = TestHelpers.CreateMockLogger<DeleteAccountCommandHandler>();
    }

    [Test]
    public async Task GetAccountsQuery_ReturnsAllAccounts()
    {
        // Arrange
        var accounts = new List<Core.Account>
        {
            TestHelpers.CreateSampleAccount(),
            TestHelpers.CreateSampleAccount()
        };
        var mockDbSet = TestHelpers.CreateMockDbSet(accounts);
        _mockContext.Setup(c => c.Accounts).Returns(mockDbSet.Object);

        var handler = new GetAccountsQueryHandler(_mockContext.Object, _mockGetAccountsLogger.Object);
        var query = new GetAccountsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetAccountByIdQuery_ReturnsAccount_WhenExists()
    {
        // Arrange
        var account = TestHelpers.CreateSampleAccount();
        var accounts = new List<Core.Account> { account };
        var mockDbSet = TestHelpers.CreateMockDbSet(accounts);
        _mockContext.Setup(c => c.Accounts).Returns(mockDbSet.Object);

        var handler = new GetAccountByIdQueryHandler(_mockContext.Object, _mockGetAccountByIdLogger.Object);
        var query = new GetAccountByIdQuery(account.AccountId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.AccountId, Is.EqualTo(account.AccountId));
    }

    [Test]
    public async Task CreateAccountCommand_CreatesAccount_Successfully()
    {
        // Arrange
        var accounts = new List<Core.Account>();
        var mockDbSet = TestHelpers.CreateMockDbSet(accounts);
        _mockContext.Setup(c => c.Accounts).Returns(mockDbSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateAccountCommandHandler(_mockContext.Object, _mockCreateAccountLogger.Object);
        var command = new CreateAccountCommand
        {
            Name = "New Account",
            AccountType = AccountType.Taxable,
            Institution = "Test Bank",
            CurrentBalance = 5000m,
            OpenedDate = DateTime.UtcNow
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(accounts, Has.Count.EqualTo(1));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

/// <summary>
/// Tests for Holding queries and commands.
/// </summary>
[TestFixture]
public class HoldingFeatureTests
{
    private Mock<IInvestmentPortfolioTrackerContext> _mockContext = null!;
    private Mock<ILogger<GetHoldingsQueryHandler>> _mockGetHoldingsLogger = null!;
    private Mock<ILogger<CreateHoldingCommandHandler>> _mockCreateHoldingLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IInvestmentPortfolioTrackerContext>();
        _mockGetHoldingsLogger = TestHelpers.CreateMockLogger<GetHoldingsQueryHandler>();
        _mockCreateHoldingLogger = TestHelpers.CreateMockLogger<CreateHoldingCommandHandler>();
    }

    [Test]
    public async Task GetHoldingsQuery_ReturnsAllHoldings()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var holdings = new List<Core.Holding>
        {
            TestHelpers.CreateSampleHolding(accountId),
            TestHelpers.CreateSampleHolding(accountId)
        };
        var mockDbSet = TestHelpers.CreateMockDbSet(holdings);
        _mockContext.Setup(c => c.Holdings).Returns(mockDbSet.Object);

        var handler = new GetHoldingsQueryHandler(_mockContext.Object, _mockGetHoldingsLogger.Object);
        var query = new GetHoldingsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task CreateHoldingCommand_CreatesHolding_Successfully()
    {
        // Arrange
        var holdings = new List<Core.Holding>();
        var mockDbSet = TestHelpers.CreateMockDbSet(holdings);
        _mockContext.Setup(c => c.Holdings).Returns(mockDbSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateHoldingCommandHandler(_mockContext.Object, _mockCreateHoldingLogger.Object);
        var command = new CreateHoldingCommand
        {
            AccountId = Guid.NewGuid(),
            Symbol = "MSFT",
            Name = "Microsoft Corporation",
            Shares = 50m,
            AverageCost = 300m,
            CurrentPrice = 350m
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Symbol, Is.EqualTo(command.Symbol));
        Assert.That(holdings, Has.Count.EqualTo(1));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

/// <summary>
/// Tests for Transaction queries and commands.
/// </summary>
[TestFixture]
public class TransactionFeatureTests
{
    private Mock<IInvestmentPortfolioTrackerContext> _mockContext = null!;
    private Mock<ILogger<GetTransactionsQueryHandler>> _mockGetTransactionsLogger = null!;
    private Mock<ILogger<CreateTransactionCommandHandler>> _mockCreateTransactionLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IInvestmentPortfolioTrackerContext>();
        _mockGetTransactionsLogger = TestHelpers.CreateMockLogger<GetTransactionsQueryHandler>();
        _mockCreateTransactionLogger = TestHelpers.CreateMockLogger<CreateTransactionCommandHandler>();
    }

    [Test]
    public async Task GetTransactionsQuery_ReturnsAllTransactions()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var transactions = new List<Core.Transaction>
        {
            TestHelpers.CreateSampleTransaction(accountId),
            TestHelpers.CreateSampleTransaction(accountId)
        };
        var mockDbSet = TestHelpers.CreateMockDbSet(transactions);
        _mockContext.Setup(c => c.Transactions).Returns(mockDbSet.Object);

        var handler = new GetTransactionsQueryHandler(_mockContext.Object, _mockGetTransactionsLogger.Object);
        var query = new GetTransactionsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task CreateTransactionCommand_CreatesTransaction_Successfully()
    {
        // Arrange
        var transactions = new List<Core.Transaction>();
        var mockDbSet = TestHelpers.CreateMockDbSet(transactions);
        _mockContext.Setup(c => c.Transactions).Returns(mockDbSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateTransactionCommandHandler(_mockContext.Object, _mockCreateTransactionLogger.Object);
        var command = new CreateTransactionCommand
        {
            AccountId = Guid.NewGuid(),
            TransactionDate = DateTime.UtcNow,
            TransactionType = TransactionType.Buy,
            Symbol = "GOOGL",
            Shares = 10m,
            PricePerShare = 140m,
            Amount = 1400m,
            Fees = 7m
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Symbol, Is.EqualTo(command.Symbol));
        Assert.That(transactions, Has.Count.EqualTo(1));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

/// <summary>
/// Tests for Dividend queries and commands.
/// </summary>
[TestFixture]
public class DividendFeatureTests
{
    private Mock<IInvestmentPortfolioTrackerContext> _mockContext = null!;
    private Mock<ILogger<GetDividendsQueryHandler>> _mockGetDividendsLogger = null!;
    private Mock<ILogger<CreateDividendCommandHandler>> _mockCreateDividendLogger = null!;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IInvestmentPortfolioTrackerContext>();
        _mockGetDividendsLogger = TestHelpers.CreateMockLogger<GetDividendsQueryHandler>();
        _mockCreateDividendLogger = TestHelpers.CreateMockLogger<CreateDividendCommandHandler>();
    }

    [Test]
    public async Task GetDividendsQuery_ReturnsAllDividends()
    {
        // Arrange
        var holdingId = Guid.NewGuid();
        var dividends = new List<Core.Dividend>
        {
            TestHelpers.CreateSampleDividend(holdingId),
            TestHelpers.CreateSampleDividend(holdingId)
        };
        var mockDbSet = TestHelpers.CreateMockDbSet(dividends);
        _mockContext.Setup(c => c.Dividends).Returns(mockDbSet.Object);

        var handler = new GetDividendsQueryHandler(_mockContext.Object, _mockGetDividendsLogger.Object);
        var query = new GetDividendsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task CreateDividendCommand_CreatesDividend_Successfully()
    {
        // Arrange
        var dividends = new List<Core.Dividend>();
        var mockDbSet = TestHelpers.CreateMockDbSet(dividends);
        _mockContext.Setup(c => c.Dividends).Returns(mockDbSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateDividendCommandHandler(_mockContext.Object, _mockCreateDividendLogger.Object);
        var command = new CreateDividendCommand
        {
            HoldingId = Guid.NewGuid(),
            PaymentDate = DateTime.UtcNow,
            ExDividendDate = DateTime.UtcNow.AddDays(-7),
            AmountPerShare = 0.5m,
            TotalAmount = 50m,
            IsReinvested = true
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.AmountPerShare, Is.EqualTo(command.AmountPerShare));
        Assert.That(dividends, Has.Count.EqualTo(1));
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
