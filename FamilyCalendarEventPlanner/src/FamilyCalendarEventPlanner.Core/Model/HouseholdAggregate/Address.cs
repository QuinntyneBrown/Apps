using FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate;

public class Address
{
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public CanadianProvince Province { get; private set; }
    public string PostalCode { get; private set; } = string.Empty;

    private Address()
    {
    }

    public Address(string street, string city, CanadianProvince province, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(street))
        {
            throw new ArgumentException("Street cannot be empty.", nameof(street));
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City cannot be empty.", nameof(city));
        }

        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new ArgumentException("Postal code cannot be empty.", nameof(postalCode));
        }

        if (!IsValidCanadianPostalCode(postalCode))
        {
            throw new ArgumentException("Invalid Canadian postal code format.", nameof(postalCode));
        }

        Street = street;
        City = city;
        Province = province;
        PostalCode = postalCode.ToUpperInvariant().Replace(" ", "");
    }

    private static bool IsValidCanadianPostalCode(string postalCode)
    {
        var normalized = postalCode.ToUpperInvariant().Replace(" ", "");
        if (normalized.Length != 6)
        {
            return false;
        }

        return char.IsLetter(normalized[0]) &&
               char.IsDigit(normalized[1]) &&
               char.IsLetter(normalized[2]) &&
               char.IsDigit(normalized[3]) &&
               char.IsLetter(normalized[4]) &&
               char.IsDigit(normalized[5]);
    }

    public string GetFormattedPostalCode()
    {
        if (PostalCode.Length == 6)
        {
            return $"{PostalCode.Substring(0, 3)} {PostalCode.Substring(3, 3)}";
        }
        return PostalCode;
    }
}
