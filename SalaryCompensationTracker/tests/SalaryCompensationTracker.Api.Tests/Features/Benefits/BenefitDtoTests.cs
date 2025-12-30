using SalaryCompensationTracker.Api.Features.Benefits;
using SalaryCompensationTracker.Core;

namespace SalaryCompensationTracker.Api.Tests.Features.Benefits;

[TestFixture]
public class BenefitDtoTests
{
    [Test]
    public void ToDto_ValidBenefit_MapsAllProperties()
    {
        // Arrange
        var benefit = new Benefit
        {
            BenefitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompensationId = Guid.NewGuid(),
            Name = "Health Insurance",
            Category = "Health",
            Description = "Premium health coverage",
            EstimatedValue = 12000m,
            EmployerContribution = 10000m,
            EmployeeContribution = 2000m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = benefit.ToDto();

        // Assert
        Assert.That(dto.BenefitId, Is.EqualTo(benefit.BenefitId));
        Assert.That(dto.UserId, Is.EqualTo(benefit.UserId));
        Assert.That(dto.CompensationId, Is.EqualTo(benefit.CompensationId));
        Assert.That(dto.Name, Is.EqualTo(benefit.Name));
        Assert.That(dto.Category, Is.EqualTo(benefit.Category));
        Assert.That(dto.Description, Is.EqualTo(benefit.Description));
        Assert.That(dto.EstimatedValue, Is.EqualTo(benefit.EstimatedValue));
        Assert.That(dto.EmployerContribution, Is.EqualTo(benefit.EmployerContribution));
        Assert.That(dto.EmployeeContribution, Is.EqualTo(benefit.EmployeeContribution));
    }
}
