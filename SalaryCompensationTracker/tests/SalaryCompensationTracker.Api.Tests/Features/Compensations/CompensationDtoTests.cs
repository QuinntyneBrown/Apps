using SalaryCompensationTracker.Api.Features.Compensations;
using SalaryCompensationTracker.Core;

namespace SalaryCompensationTracker.Api.Tests.Features.Compensations;

[TestFixture]
public class CompensationDtoTests
{
    [Test]
    public void ToDto_ValidCompensation_MapsAllProperties()
    {
        // Arrange
        var compensation = new Compensation
        {
            CompensationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompensationType = CompensationType.FullTime,
            Employer = "Tech Corp",
            JobTitle = "Software Engineer",
            BaseSalary = 100000m,
            Currency = "USD",
            Bonus = 10000m,
            StockValue = 5000m,
            OtherCompensation = 2000m,
            TotalCompensation = 117000m,
            EffectiveDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1),
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = compensation.ToDto();

        // Assert
        Assert.That(dto.CompensationId, Is.EqualTo(compensation.CompensationId));
        Assert.That(dto.UserId, Is.EqualTo(compensation.UserId));
        Assert.That(dto.CompensationType, Is.EqualTo(compensation.CompensationType));
        Assert.That(dto.Employer, Is.EqualTo(compensation.Employer));
        Assert.That(dto.JobTitle, Is.EqualTo(compensation.JobTitle));
        Assert.That(dto.BaseSalary, Is.EqualTo(compensation.BaseSalary));
        Assert.That(dto.Currency, Is.EqualTo(compensation.Currency));
        Assert.That(dto.Bonus, Is.EqualTo(compensation.Bonus));
        Assert.That(dto.StockValue, Is.EqualTo(compensation.StockValue));
        Assert.That(dto.OtherCompensation, Is.EqualTo(compensation.OtherCompensation));
        Assert.That(dto.TotalCompensation, Is.EqualTo(compensation.TotalCompensation));
        Assert.That(dto.Notes, Is.EqualTo(compensation.Notes));
    }
}
