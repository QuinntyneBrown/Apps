using LifeAdminDashboard.Api.Features.Renewals;

namespace LifeAdminDashboard.Api.Tests.Features.Renewals;

[TestFixture]
public class RenewalDtoMappingTests
{
    [Test]
    public void ToDto_MapsAllProperties()
    {
        // Arrange
        var renewal = new Renewal
        {
            RenewalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Renewal",
            RenewalType = "Subscription",
            Provider = "Test Provider",
            RenewalDate = DateTime.UtcNow.AddMonths(1),
            Cost = 99.99m,
            Frequency = "Monthly",
            IsAutoRenewal = true,
            IsActive = true,
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = renewal.ToDto();

        // Assert
        Assert.That(dto.RenewalId, Is.EqualTo(renewal.RenewalId));
        Assert.That(dto.UserId, Is.EqualTo(renewal.UserId));
        Assert.That(dto.Name, Is.EqualTo(renewal.Name));
        Assert.That(dto.RenewalType, Is.EqualTo(renewal.RenewalType));
        Assert.That(dto.Provider, Is.EqualTo(renewal.Provider));
        Assert.That(dto.RenewalDate, Is.EqualTo(renewal.RenewalDate));
        Assert.That(dto.Cost, Is.EqualTo(renewal.Cost));
        Assert.That(dto.Frequency, Is.EqualTo(renewal.Frequency));
        Assert.That(dto.IsAutoRenewal, Is.EqualTo(renewal.IsAutoRenewal));
        Assert.That(dto.IsActive, Is.EqualTo(renewal.IsActive));
        Assert.That(dto.Notes, Is.EqualTo(renewal.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(renewal.CreatedAt));
    }
}
