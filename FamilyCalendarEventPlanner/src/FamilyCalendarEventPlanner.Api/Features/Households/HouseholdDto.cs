using FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate;
using FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate.Enums;

namespace FamilyCalendarEventPlanner.Api.Features.Households;

public record HouseholdDto
{
    public Guid HouseholdId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public CanadianProvince Province { get; init; }
    public string PostalCode { get; init; } = string.Empty;
    public string FormattedPostalCode { get; init; } = string.Empty;
    public string FullAddress { get; init; } = string.Empty;
}

public static class HouseholdExtensions
{
    public static HouseholdDto ToDto(this Household household)
    {
        return new HouseholdDto
        {
            HouseholdId = household.HouseholdId,
            Name = household.Name,
            Street = household.Street,
            City = household.City,
            Province = household.Province,
            PostalCode = household.PostalCode,
            FormattedPostalCode = household.GetFormattedPostalCode(),
            FullAddress = household.GetFullAddress()
        };
    }
}
