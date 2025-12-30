// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Infrastructure.Tests;

/// <summary>
/// Unit tests for the TaxDeductionOrganizerContext.
/// </summary>
[TestFixture]
public class TaxDeductionOrganizerContextTests
{
    private TaxDeductionOrganizerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TaxDeductionOrganizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TaxDeductionOrganizerContext(options);
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
    /// Tests that TaxYears can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TaxYears_CanAddAndRetrieve()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false,
            TotalDeductions = 0,
            Notes = "Current year",
        };

        // Act
        _context.TaxYears.Add(taxYear);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TaxYears.FindAsync(taxYear.TaxYearId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Year, Is.EqualTo(2024));
        Assert.That(retrieved.IsFiled, Is.False);
    }

    /// <summary>
    /// Tests that Deductions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Deductions_CanAddAndRetrieve()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false,
            TotalDeductions = 0,
        };

        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Home Office Equipment",
            Amount = 1500.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.HomeOffice,
            HasReceipt = true,
        };

        // Act
        _context.TaxYears.Add(taxYear);
        _context.Deductions.Add(deduction);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Deductions.FindAsync(deduction.DeductionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Home Office Equipment"));
        Assert.That(retrieved.Amount, Is.EqualTo(1500.00m));
        Assert.That(retrieved.Category, Is.EqualTo(DeductionCategory.HomeOffice));
    }

    /// <summary>
    /// Tests that Receipts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Receipts_CanAddAndRetrieve()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false,
            TotalDeductions = 0,
        };

        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Business Expense",
            Amount = 500.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Business,
            HasReceipt = true,
        };

        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = deduction.DeductionId,
            FileName = "receipt.pdf",
            FileUrl = "/receipts/receipt.pdf",
            UploadDate = DateTime.UtcNow,
        };

        // Act
        _context.TaxYears.Add(taxYear);
        _context.Deductions.Add(deduction);
        _context.Receipts.Add(receipt);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Receipts.FindAsync(receipt.ReceiptId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.FileName, Is.EqualTo("receipt.pdf"));
        Assert.That(retrieved.FileUrl, Is.EqualTo("/receipts/receipt.pdf"));
    }

    /// <summary>
    /// Tests that Deductions can be associated with TaxYear.
    /// </summary>
    [Test]
    public async Task Deductions_CanAssociateWithTaxYear()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false,
            TotalDeductions = 0,
        };

        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Medical Expense",
            Amount = 800.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Medical,
            HasReceipt = false,
        };

        // Act
        _context.TaxYears.Add(taxYear);
        _context.Deductions.Add(deduction);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Deductions
            .Include(d => d.TaxYear)
            .FirstOrDefaultAsync(d => d.DeductionId == deduction.DeductionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TaxYear, Is.Not.Null);
        Assert.That(retrieved.TaxYear!.Year, Is.EqualTo(2024));
    }

    /// <summary>
    /// Tests that cascade delete works for TaxYear and Deductions.
    /// </summary>
    [Test]
    public async Task TaxYears_CascadeDeleteDeductions()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false,
            TotalDeductions = 0,
        };

        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Test Deduction",
            Amount = 100.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Business,
            HasReceipt = false,
        };

        _context.TaxYears.Add(taxYear);
        _context.Deductions.Add(deduction);
        await _context.SaveChangesAsync();

        // Act
        _context.TaxYears.Remove(taxYear);
        await _context.SaveChangesAsync();

        var retrievedDeduction = await _context.Deductions.FindAsync(deduction.DeductionId);

        // Assert
        Assert.That(retrievedDeduction, Is.Null);
    }

    /// <summary>
    /// Tests that cascade delete works for Deduction and Receipts.
    /// </summary>
    [Test]
    public async Task Deductions_CascadeDeleteReceipts()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false,
            TotalDeductions = 0,
        };

        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Test Deduction",
            Amount = 100.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Business,
            HasReceipt = true,
        };

        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            DeductionId = deduction.DeductionId,
            FileName = "test_receipt.pdf",
            FileUrl = "/receipts/test_receipt.pdf",
            UploadDate = DateTime.UtcNow,
        };

        _context.TaxYears.Add(taxYear);
        _context.Deductions.Add(deduction);
        _context.Receipts.Add(receipt);
        await _context.SaveChangesAsync();

        // Act
        _context.Deductions.Remove(deduction);
        await _context.SaveChangesAsync();

        var retrievedReceipt = await _context.Receipts.FindAsync(receipt.ReceiptId);

        // Assert
        Assert.That(retrievedReceipt, Is.Null);
    }

    /// <summary>
    /// Tests that Deductions can be updated.
    /// </summary>
    [Test]
    public async Task Deductions_CanUpdate()
    {
        // Arrange
        var taxYear = new TaxYear
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024,
            IsFiled = false,
            TotalDeductions = 0,
        };

        var deduction = new Deduction
        {
            DeductionId = Guid.NewGuid(),
            TaxYearId = taxYear.TaxYearId,
            Description = "Original Description",
            Amount = 100.00m,
            Date = DateTime.UtcNow,
            Category = DeductionCategory.Business,
            HasReceipt = false,
        };

        _context.TaxYears.Add(taxYear);
        _context.Deductions.Add(deduction);
        await _context.SaveChangesAsync();

        // Act
        deduction.Description = "Updated Description";
        deduction.Amount = 200.00m;
        deduction.AttachReceipt();
        await _context.SaveChangesAsync();

        var retrieved = await _context.Deductions.FindAsync(deduction.DeductionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Updated Description"));
        Assert.That(retrieved.Amount, Is.EqualTo(200.00m));
        Assert.That(retrieved.HasReceipt, Is.True);
    }
}
